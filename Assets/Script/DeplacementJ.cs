using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementJ : PersonnalMethod
{
    //Public variable
    public float F_SpeedDeplacementClassic;// valeur de la vitesse
    public float F_JumpForce;//valeur de la force de jump
    public float F_SortieDeGrappin;
    public bool CanMoveBasic = true;
    //public float MagnitudeMax;
    //Local variable
    Rigidbody RbPlayer;// le component rigidbody
    GrappinV2 Grap;
    GestionDesDatasPlayer GDDP;

    int nombreDeJump;
    void Start()
    {
        RbPlayer = GetComponent<Rigidbody>();//Récupére le component rigidbody de l'objet
        Grap = GetComponent<GrappinV2>();
    }

    
    

    public void DeplacementClassic(float X,float Z)//Lance le déplacement
    {
        //print(CanMove);
        if (!Grap.Activate && CanMoveBasic)
        {
           
            Vector2 ClampVelocitySpeed = new Vector2(X, Z);// à changer RbPlayer pour le jump //créer un vecteur pour la velocité
            float calculatedMagnitude = Mathf.Sqrt(X * X + Z * Z);//calcul de la magnitude
            if (calculatedMagnitude >= 1)// si la magnitude est supérrieur a 1
            {
                ClampVelocitySpeed = ClampVelocitySpeed.normalized; //la remet a 1
            }
            ClampVelocitySpeed = ClampVelocitySpeed * F_SpeedDeplacementClassic;// multiplie celle par la valeur souhaiter
            Vector3 FutureVelocity = new Vector3(ClampVelocitySpeed.x, RbPlayer.velocity.y, ClampVelocitySpeed.y);
            RbPlayer.velocity = transform.TransformDirection(FutureVelocity);//fais en sorte qu'il le fasse de maniére local

        }
       

    }

    public void DeplacementInAir(float X, float Z) //pour le déplacement dans les airs
    {
    
    }



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
