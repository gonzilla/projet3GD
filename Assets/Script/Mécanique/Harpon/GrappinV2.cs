using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappinV2 : PersonnalMethod
{
    //Script du grappin (nouveau)
    //Public variable
    public float SpeedHook;
    public bool Activate;
    public Material Mat_Corde;
    public float ForceApresGrappin;
    
    //Local variable
    Vector3 CordonnateToRush;
    Vector3 Direction;
    Rigidbody Rb;
    ConstantForce CF;
    LineRenderer LR;
    GestionDataNonJoueur GDNJ;
    GestionDesDatasPlayer GDDP;
    

    void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        GoFindDataNonJoueur(out GDNJ);
        GoFindDataPlayer(out GDDP);
       
       
    }


    void Update()
    {

       
        if (Activate)
        {
            Direction = CordonnateToRush - transform.position;
            
            
            Rb.velocity = Direction.normalized * SpeedHook;

           


        }


    }

    private void FixedUpdate()
    {
        float distanceParcourue = Vector3.Distance(transform.position, CordonnateToRush);
        if (distanceParcourue <= GDNJ.DistanceCassure && Activate)
        {
            print("Cancel");
            Rb.AddForce(Vector3.down * Rb.velocity.magnitude/ForceApresGrappin, ForceMode.VelocityChange);
            
            Cancel();
        }

    }

    
    public void RushTo(Vector3 Point)
    {
        
        Activate = true;//active
        CordonnateToRush = Point;
       Direction = Point - transform.position;
        if (CF == null)//si il n y'a  pas une constante force
        {
            CF = this.gameObject.AddComponent<ConstantForce>();//ajoute une constante force
        }
        CF.force = Vector3.up * 9.81f * 2;
    }
    public void DeleteConstantforce()
    {
        Destroy(CF);// Destroy le component constant force

    }

    public void Cancel() // annule le grappin
    {
        if (Activate)
        {

            
            Activate = false;// desactive
            DeleteConstantforce(); // delete force constant force
            GDNJ.EV.DestroyElementVisuelle();
        }


    }
    
    /*private void OnCollisionEnter(Collision collision)
    {
        /*
        Activate = false;
        DeleteConstantforce();
        Destroy(LR);

    }*/

  

    /*public bool EstIlGrappinable (string LeTag)   
    {
        bool renvoie = false;
        foreach (string item in GDNJ.ListeDeTagObjetAccrochable)
        {
            if (LeTag==item)
            {
                renvoie = true;
                break;
                
            }
        }
        return renvoie;

    }*/
}
/*if (transform.position==ExPosition)
      {
          Activate = false;
          DeleteConstantforce();
          Rb.velocity = Vector3.zero;
      }
      ExPosition = transform.position;*/
/*RaycastHit hit;
      Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, ~(1 << 9));
      Dj.ComeBackSol(Vector3.Distance(hit.point,transform.position));*/
/*public void RushTo(RaycastHit Info)
{

  //Destroy(LR);//detruit le line render
  Activate = true;//active
  //ObjetToGo = Info.transform.gameObject;
  CordonnateToRush = Info.point;//recupére la position
  Direction = CordonnateToRush - transform.position;//calcul de la direction
  if (CF == null)//si il n y'a  pas une constante force
  {
      CF = this.gameObject.AddComponent<ConstantForce>();//ajoute une constante force
  }
  //setLineRenderer();//set le line render
  CF.force = Vector3.up * 9.81f*2;// eneleve la gravité

}*/
