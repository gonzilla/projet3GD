using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
    //Public variable
    public float speed;
    //Local variable

    void Start()
    {
        
    }

    
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement() 
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 plyerMovement = new Vector3(hor, 0, ver)*Time.deltaTime*speed;
        transform.Translate(plyerMovement, Space.Self);
    }
}
