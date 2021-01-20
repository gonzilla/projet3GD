using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //Public variable
    public string vertical;
    public float vitesseMax;
    public float speedX;
    //Local variable
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
{
        if (rb.velocity.magnitude>vitesseMax)
        {
            rb.velocity = rb.velocity.normalized*vitesseMax ;
        }
        
        if (Input.GetAxis(vertical)!=0)
        {
            //rb.AddForce(Vector3.forward*speedX* Input.GetAxis(vertical)*Time.deltaTime,ForceMode.VelocityChange);
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -speedX * Input.GetAxis(vertical));
        }
    }
}
