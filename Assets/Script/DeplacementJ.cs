using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementJ : PersonnalMethod
{
    //Public variable
    public float F_SpeedDeplacementClassic;// valeur de la vitesse
    public float F_JumpForce;//valeur de la force de jump
                             //public float F_SortieDeGrappin;

    public float F_SpeedDeplacementRoller;
    
    public bool CanMoveBasic = true;
    public Camera maCamera;
    //public float MagnitudeMax;
    //Local variable
    Rigidbody RbPlayer;// le component rigidbody
    GrappinV2 Grap;
    GestionDesDatasPlayer GDDP;

    int nombreDeJump;
    void Start()
    {
        GoFindDataPlayer(out GDDP);
        RbPlayer = GetComponent<Rigidbody>();//Récupére le component rigidbody de l'objet
        Grap = GetComponent<GrappinV2>();
    }

    
    

    public void DeplacementClassic(float X,float Z)//Lance le déplacement
    {
        //print(CanMove);
        if (!Grap.Activate && CanMoveBasic)
        {


            if (!GDDP.Vise)
            {
                float targetAngle = Mathf.Atan2(X, Z) * Mathf.Rad2Deg + maCamera.transform.eulerAngles.y;
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            }
            
            //Vector3 depla =new Vector3 (0 , RbPlayer.velocity.y, transform.TransformDirection(Vector3.forward).z);
            RbPlayer.velocity = transform.TransformDirection(Vector3.forward) * F_SpeedDeplacementClassic;
        }
       

    }
    
    public void DeplacementRoller(float X, float Z) 
    {
        float targetAngle = Mathf.Atan2(X, Z) * Mathf.Rad2Deg + maCamera.transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        if (RbPlayer.velocity.magnitude<F_SpeedDeplacementRoller)
        {
            RbPlayer.AddForce(transform.TransformDirection(Vector3.forward) * F_SpeedDeplacementRoller, ForceMode.Force);
        }
        
    }
    /*
    public void DeplacementInAir(float X, float Z) //pour le déplacement dans les airs
    {
    
    }
    */


    public void Jump() 
    {
        if (nombreDeJump<2)
        {
            RbPlayer.AddForce(Vector3.up * F_JumpForce, ForceMode.Impulse);// applique une force impulse vers le haut

            nombreDeJump++;
        }          
        
    
       

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal==Vector3.up)
        {
            nombreDeJump = 0;
            CanMoveBasic = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
       /* RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit,3f, ~(1<<9)))
        {
            if (hit.normal==Vector3.up)
            {
                print("sores");
                OnGround = false;
            }
        }*/
    }
}
/*if (RbPlayer.velocity.magnitude< VitesseMax)
           {
               Vector3 DirectionJoueur = transform.TransformDirection(new Vector3(X, 0, Z) * F_SpeedDeplacementClassic);
               RbPlayer.AddForce(DirectionJoueur, ForceMode.Force);

           }*/






/*Vector2 ClampVelocitySpeed = new Vector2(X, Z);// à changer RbPlayer pour le jump //créer un vecteur pour la velocité
float calculatedMagnitude = Mathf.Sqrt(X * X + Z * Z);//calcul de la magnitude
if (calculatedMagnitude >= 1)// si la magnitude est supérrieur a 1
{
    ClampVelocitySpeed = ClampVelocitySpeed.normalized; //la remet a 1
}
//transform.Rotate(Vector3.up,Space.World);
ClampVelocitySpeed = ClampVelocitySpeed * F_SpeedDeplacementClassic;// multiplie celle par la valeur souhaiter
Vector3 FutureVelocity = new Vector3(ClampVelocitySpeed.x, RbPlayer.velocity.y, ClampVelocitySpeed.y);
RbPlayer.velocity = transform.TransformDirection(FutureVelocity);//fais en sorte qu'il le fasse de maniére local*/
/* transform.rotation = Quaternion.Euler(0, Mathf.Atan2(X, Z) * Mathf.Rad2Deg * maCamera.transform.eulerAngles.y, 0);//pas encore ça
 RbPlayer.velocity = transform.TransformDirection((Vector3.forward) *F_SpeedDeplacementClassic);*/
