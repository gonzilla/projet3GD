using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNiveau : MonoBehaviour
{
    //Public variable
    public GameObject objectToSpawn;
    public Transform coordinate;
    //Local variable

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    void create() 
    {
        Instantiate(objectToSpawn, coordinate.position, transform.rotation);
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            create();
        }
    }


}
