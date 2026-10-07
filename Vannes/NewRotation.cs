using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NewRotation : MonoBehaviour
{
    [Header("Refined Physics Settings")]
    [Tooltip("How hard the square pushes off the ground to flip.")]
    [SerializeField] private float flipForce = 5f;

    [Tooltip("How fast the square rotates mid-air.")]
    [SerializeField] private float torqueForce = 10f;

    [Header("Ground Check")]
    [Tooltip("Layer mask containing your ground/platforms.")]
    [SerializeField] private LayerMask groundLayer;
    [Tooltip("Distance from center to check for ground. Match half your square size.")]
    [SerializeField] private float groundCheckDistance = 0.05f;
    [Tooltip("The size of the detection box. Make this slightly larger than your square (e.g., 1.05 x 1.05) to detect walls/floors touching any side.")]
    [SerializeField] private Vector2 overlapBoxSize = new Vector2(1.05f, 1.05f);

    [Header("Water Check")]
    [Tooltip("Layer mask containing your ground/platforms.")]
    [SerializeField] private LayerMask waterLayer;

    public Rigidbody2D rb;
    float moveCooldown;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Ensure the Rigidbody doesn't infinitely roll like a ball after landing
        rb.angularDrag = 5f;
        rb.drag = 1f;
    }

    void Update()
    {
        // Add a small hop option to help get out of pools of water
        if (Input.GetKeyDown(KeyCode.Space)){
            rb.AddForce(Vector3.up * 10);
        }

        // Simple cooldown makes all movement feel like the box requires great effort to move
        moveCooldown += Time.deltaTime;
        if (moveCooldown < .5f) return;

        // Cast a ray from each side of our square and see if any touches the floor
        if (Physics2D.Raycast(transform.position, transform.up, groundCheckDistance, groundLayer) ||
            Physics2D.Raycast(transform.position, -transform.up, groundCheckDistance, groundLayer) ||
            Physics2D.Raycast(transform.position, -transform.right, groundCheckDistance, groundLayer) ||
            Physics2D.Raycast(transform.position, transform.right, groundCheckDistance, groundLayer))
        {
            Roll();
        }
        else
        {
            // In case we get caught in an area where only the corners are touching something, this checks around the entire box
            if (Physics2D.OverlapBox(transform.position, overlapBoxSize, transform.eulerAngles.z, groundLayer))
            {
                Roll();
            }
            else
            {
                // When we are not on land, we expect to move perfectly horizontal (i.e. in water)
                float horizontal = Input.GetAxis("Horizontal");
                rb.AddForce(Vector2.right * horizontal * 50);
                moveCooldown = 0;
                Debug.Log("No ground to roll off");
                return;
            }
        }
    }

    private void Roll()
    {
        // Moving via WASD or Arrow Keys
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveCooldown = 0;
            ApplyFlop(Vector2.right, -torqueForce);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveCooldown = 0;
            ApplyFlop(Vector2.left, torqueForce);
        }
    }

    private void ApplyFlop(Vector2 direction, float torque)
    {
        // Reset existing velocity so flips feel consistent
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Combine upward lift and direction
        Vector2 jumpDirection = (Vector2.up + direction).normalized;

        // 4. Apply forces using Impulse (instant velocity change)
        rb.AddForce(jumpDirection * flipForce, ForceMode2D.Impulse);
        rb.AddTorque(torque, ForceMode2D.Impulse);
    }
}