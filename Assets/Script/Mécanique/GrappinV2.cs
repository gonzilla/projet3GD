using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappinV2 : MonoBehaviour
{
    //Script du grappin (nouveau)
    //Public variable
    public float SpeedHook;
    public bool Activate;
    public Material Mat_Corde;
    //Local variable
    Vector3 CordonnateToRush;
    Vector3 Direction;
    GameObject ObjetToGo;
    Rigidbody Rb;
    ConstantForce CF;
    LineRenderer LR;
    //Vector3 ExPosition;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        //ExPosition = transform.position;
    }

    
    void Update()
    {
       
        if (Activate)
        {
            Direction = CordonnateToRush - transform.position;

            Rb.velocity = Direction.normalized * SpeedHook;
            
            UpdateLineRenderer();// update le line renderer
            

        }

      
    }

    public void RushTo(RaycastHit Info) 
    {
        Destroy(LR);//detruit le line render
        Activate = true;//active
        //ObjetToGo = Info.transform.gameObject;
        CordonnateToRush = Info.point;//recupére la position
        Direction = CordonnateToRush - transform.position;//calcul de la direction
       if (CF==null)//si il n y'a  pas une constante force
        {
            CF = this.gameObject.AddComponent<ConstantForce>();//ajoute une constante force
        }
        setLineRenderer();//set le line render
        CF.force = Vector3.up * 9.81f;// eneleve la gravité
        
    }
    public void DeleteConstantforce() 
    {
        Destroy(CF);// Destroy le component constant force

    }

    public void Cancel() // annule le grappin
    {
        if (Activate)
        {
            Destroy(LR);//detruit le line render
            Activate = false;// desactive
            DeleteConstantforce(); // delete force constant force
        }
        
    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Mur")|| collision.transform.CompareTag("Inmovable"))
        {
            Activate = false;
            DeleteConstantforce();
            Destroy(LR);
        }
    }

    void setLineRenderer() //set le linerender
    {
        LR = gameObject.AddComponent<LineRenderer>();//ajoute le component
        if (LR != null)
        {
            LR.SetPosition(0, transform.position);//set le point 0
            LR.SetPosition(1, CordonnateToRush);//set le point 1
            LR.startWidth = 0.25f;//la "grosseur" de corde
            LR.material = Mat_Corde;//met le material
        }
        
    }
    void UpdateLineRenderer()// update le linerender
    {
        if (LR!=null)
        {
            LR.SetPosition(0, transform.position);
        }
        
    }

}
/*if (transform.position==ExPosition)
      {
          Activate = false;
          DeleteConstantforce();
          Rb.velocity = Vector3.zero;
      }
      ExPosition = transform.position;*/
