using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RotateingPlayer : MonoBehaviour
{



    [Header("Movement Settings")]
    [SerializeField] private float torqueForce = 15f;
    [SerializeField] private float targetAngleStep = 90f;
    [SerializeField] private float angleTolerance = 1.5f;

    private Rigidbody2D rb;
    private bool isFlopping = false;
    private float startAngle;
    private float targetAngle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Triggers the flop sequence when holding "A" or "D" (or Arrow keys)
        // and ensures it only starts if the square is currently still
        if (Input.GetAxisRaw("Horizontal") != 0 && !isFlopping && rb.angularVelocity == 0)
        {
            StartFlop(Input.GetAxisRaw("Horizontal"));
        }
    }

    void FixedUpdate()
    {
        if (isFlopping)
        {
            ExecuteFlop();
        }
    }

    private void StartFlop(float direction)
    {
        isFlopping = true;

        // Capture the current angle and calculate the next 90-degree snap target
        startAngle = rb.rotation;

        // Negative direction for right, positive for left due to Unity's rotation rules
        float angleDirection = direction > 0 ? -targetAngleStep : targetAngleStep;
        targetAngle = startAngle + angleDirection;

        // Give the physics engine a small initial push to get it off the flat edge
        rb.AddTorque(angleDirection * torqueForce * 0.5f, ForceMode2D.Impulse);
    }

    private void ExecuteFlop()
    {
        // Calculate how much rotation is left to reach the target angle
        float angleDiff = Mathf.Abs(rb.rotation - targetAngle);

        if (angleDiff > angleTolerance)
        {
            // Apply continuous torque toward the target direction
            float directionSign = Mathf.Sign(targetAngle - startAngle);
            rb.AddTorque(directionSign * torqueForce, ForceMode2D.Force);
        }
        else
        {
            // Target reached: Snap to exact angle, stop physics forces, and reset
            StopFlop();
        }
    }

    private void StopFlop()
    {
        isFlopping = false;
        rb.rotation = targetAngle;
        rb.angularVelocity = 0f;
        rb.velocity = Vector2.zero; // Note: Use rb.velocity if using Unity 2022 or older
    }
}