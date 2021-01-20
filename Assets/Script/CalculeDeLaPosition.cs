using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculeDeLaPosition : MonoBehaviour
{
    //Public variable

    //Local variable
    [SerializeField] Vector3 OffsetPos;
    [SerializeField] Transform Joueur;

    
    void FixedUpdate()
    {
        transform.position = Joueur.position + OffsetPos;
    }
}
