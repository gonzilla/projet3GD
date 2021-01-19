using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDirection : MonoBehaviour
{
    //Public variable
    public float speed;
    //Local variable
    Rigidbody RbPlayer;

    private void Start()
    {
        RbPlayer = GetComponent<Rigidbody>();
    }




    void Update()
    {
       
        RaycastHit hit;
        if (Physics.Raycast(transform.position,Vector3.down,out hit,100, ~(1 << 9)))
        {
           
        }
        Vector3 directionEncote = Vector3.Cross(Vector3.back, hit.normal).normalized *10;
        Debug.DrawRay(transform.position, directionEncote, Color.red);
        Vector3 Vitesse = new Vector3(directionEncote.x, RbPlayer.velocity.y - 9.81f, directionEncote.z);
        Debug.DrawRay(transform.position, Vitesse,Color.magenta);
        if (RbPlayer.velocity.magnitude<speed)
        {
            RbPlayer.AddForce(directionEncote.normalized * speed, ForceMode.VelocityChange);
        }
        else if (RbPlayer.velocity.magnitude>speed)
        {
            RbPlayer.velocity = directionEncote.normalized*speed;
        }
        
    }
}
