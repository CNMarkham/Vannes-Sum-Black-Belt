using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : MonoBehaviour
{
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        rb.AddForce((Vector2.down) *5000* horizontal * Time.deltaTime);
    }
}
