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
    public List<Transform> corners;
    public int cornerCounter;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bottomRight = corners[1];
        bottomLeft = corners[0];
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if(horizontal > 0)
        {
            //cornercounter needs to be updated and link/recursive the list!!!!
            cornerCounter = 1;
            transform.RotateAround(bottomRight.position,-Vector3.forward,45);
            bottomRight = corners[cornerCounter++];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Bounce Pad"))
        {
            rb.AddForce(Vector2.up * pushForce, ForceMode2D.Impulse);
        }
    }
}
