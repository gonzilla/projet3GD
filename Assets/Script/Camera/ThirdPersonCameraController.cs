using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    //Public variable
    public float RotationSpeed = 1;
    public Transform Target, Player, Obstruction;
    
    
    //Local variable
    float mouseX, mouseY;
    float zoomSpeed = 2f;


    void Start()
    {
        Obstruction = Target;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void LateUpdate()
    {
        CamControl();
        //viewObstruction();
    }

    void CamControl() 
    {
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY += Input.GetAxis("Mouse Y") * RotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(Target);

        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        Player.rotation = Quaternion.Euler(0, mouseX, 0);
    }

    void viewObstruction() 
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, 4.5f))
        {
            if (hit.collider.gameObject.tag!="Player")
            {
                Obstruction.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                if (Vector3.Distance(Obstruction.position,transform.position)>=3f && Vector3.Distance(transform.position, Target.position)>=1.5f)
                {
                    transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                }
            }
            else 
            {
                Obstruction.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                if ( Vector3.Distance(transform.position, Target.position) <4.5f)
                {
                    transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                }
            }
        }
    }
}
