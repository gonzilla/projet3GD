using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Script de la caméra
    //public Camera main;
    public float sensX = 1f;
    public float sensY = 1f;

    float curTilt = 0;
    Vector2 currentLook;

    //Public variable
    public float RotationSpeed = 1;
    public Transform Target, Player;

    float mouseX, mouseY;

    void Start()
    {

        curTilt = transform.localEulerAngles.z;//la rotation de l'objet sur z
        Cursor.lockState = CursorLockMode.Locked;//verrouille le curseur au centre
        Cursor.visible = false;//rend le curseur invisible
    }

    void Update()
    {
        newMethode();
        //OLDmethode();
    }

    void newMethode() 
    {
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY += Input.GetAxis("Mouse Y") * RotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -60, 60);

        transform.LookAt(Target);

        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        Player.rotation = Quaternion.Euler(0, mouseX, 0);
    }

    void OLDmethode() 
    {
        /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//récupere la poistion de la sourris et tire un raycast vers depuis la cam
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.blue);//visualisation du raycast*/
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));//vas chercher axis de déplacement de la souris
        mouseInput.x *= sensX;//regle la sensibilité X
        mouseInput.y *= sensY;//regle la sensibilité Y

        currentLook.x += mouseInput.x;//ajoute a l'axis x à current Look
        currentLook.y = Mathf.Clamp(currentLook.y += mouseInput.y, -90, 90);//force le joueur à ne regarder que devans lui


        //transform.rotation = Quaternion.AngleAxis(currentLook.y, Vector3.right);//tourne l'objet autour de l'axe x
        //transform.rotation = Quaternion.AngleAxis(currentLook.x, Vector3.up);//tourne l'objet autour de l'axe x
        //transform.eulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, curTilt);//tourne la camera de 15degre
        transform.root.transform.localRotation = Quaternion.Euler(0, currentLook.x, 0);// tourne la capsule collider sur y
        transform.localRotation = Quaternion.Euler(currentLook.y, 0, 0);
    }
}
