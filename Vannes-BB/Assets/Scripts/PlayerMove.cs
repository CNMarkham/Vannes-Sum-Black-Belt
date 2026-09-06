using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movespeed;
    public float pushForce;
    public Transform bottomRight;
    public Transform bottomLeft;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if(horizontal > 0)
        transform.RotateAround(bottomRight.position, Vector3.right,45f);
        rb.AddForce(Vector2.right * horizontal * movespeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Bounce Pad"))
        {
            rb.AddForce (Vector2.up * pushForce, ForceMode2D.Impulse);
        }
    }
}
