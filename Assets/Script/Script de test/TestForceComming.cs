using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForceComming : MonoBehaviour
{
    //Public variable
    public Transform cible;
    public float speed;
    public float DistanceMin;
    //Local variable
    Rigidbody rb;
    public enum TypeDeforce 
    {
    Velocity,
    AddForce
    
    };
    public TypeDeforce Typeof;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (TypeDeforce.AddForce == Typeof)
        {
            ConstantForce CF= this.gameObject.AddComponent<ConstantForce>();
            CF.force= new Vector3(0, 9.81f, 0);
        }
    }

    
    void Update()
    {
        if (TypeDeforce.Velocity==Typeof)
        {
            if (transform.position != cible.transform.position)
            {
                Vector3 direction = cible.transform.position - transform.position;
                rb.velocity = direction.normalized * speed;
            }

            if (Vector3.Distance(cible.transform.position, transform.position) < DistanceMin)
            {
                transform.position = cible.transform.position;
            }
        }
        if (TypeDeforce.AddForce == Typeof)
        {
            Vector3 direction = cible.transform.position - transform.position;
            float MaMagnitude=rb.velocity.magnitude;
            Vector3 test = direction.normalized*speed + (-1 * rb.velocity);
            rb.AddForce(test, ForceMode.Force);

        }
       
       
    }
}
