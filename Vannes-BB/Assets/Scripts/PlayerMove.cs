using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movespeed;
    public float pushForce;
    public float filpForce;
    public Transform bottomRight;
    public Transform bottomLeft;
    public List<Transform> corners;
    public int cornerCounter;
    public bool rotating;
    public Animator roll;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        roll = GetComponent<Animator>();
        bottomRight = corners[1];
        bottomLeft = corners[0];
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.AddForce(Vector2.right * horizontal * movespeed * Time.deltaTime);
        if (horizontal > 0)
        {
            roll.SetBool("Right", true);
            roll.SetBool("Left", false);
            //if (rotating == false)
            //{
            //    rotating = true;
            //    //cornercounter needs to be updated and link/recursive the list!!!!
                
            //    cornerCounter++;
            //    transform.RotateAround(bottomRight.position,-Vector3.forward,60);
            //    if (cornerCounter == 4)
            //        cornerCounter = 0;
            //    bottomRight = corners[cornerCounter];
            //    Debug.Log(bottomRight.name);
            //}
        }
        if(horizontal < 0)
        {
            roll.SetBool("Left", true);
            roll.SetBool("Right", false);
        }
        if (horizontal == 0)
        {
            roll.SetBool("Right", false);
            roll.SetBool("Left", false);
            rotating = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Bounce Pad"))
        {
            rb.AddForce(Vector2.up * pushForce, ForceMode2D.Impulse);
        }

        //if (collision.gameObject.tag == ("Gravity Pad"))
        //{
        //    rb.AddForce(Vector2.up * filpForce, ForceMode2D.Impuls;
        //}
    }

    
}
