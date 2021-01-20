using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCursor : MonoBehaviour
{
    //Public variable
    
    //Local variable

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//verrouille le curseur au centre
        Cursor.visible = false;
    }

    
    
}
