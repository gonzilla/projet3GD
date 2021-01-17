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

    public float F_SpeedBobSleigueCote;
    public float F_SpeedBobSleigue;
    public bool CanMoveBasic = true;
    public Camera maCamera;
    //public float MagnitudeMax;
    //Local variable
    Rigidbody RbPlayer;// le component rigidbody
    GrappinV2 Grap;
    GestionDesDatasPlayer GDDP;
    GestionDesFormes GDF;
    Vector3 directionEncote;
    bool surLeSol;
    int nombreDeJump;
    void Start()
    {
        GoFindDataPlayer(out GDDP);
        RbPlayer = GetComponent<Rigidbody>();//Récupére le component rigidbody de l'objet
        Grap = GetComponent<GrappinV2>();
        GDF = GetComponent<GestionDesFormes>();
    }

    
    

    public void DeplacementClassic(float X,float Z)//Lance le déplacement //marche un peu par magie noir en vrais

    {
        //print(CanMove);
        if (!Grap.Activate && CanMoveBasic)
        {


            if (!GDDP.Vise)//si je ne vise pas
            {
                float targetAngle = Mathf.Atan2(X, Z) * Mathf.Rad2Deg + maCamera.transform.eulerAngles.y; // calcul de la rotation
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f); // set la rotation
            }
                 
            Vector2 ClampVelocitySpeed = new Vector2(X, Z);// à changer RbPlayer pour le jump //créer un vecteur pour la velocité
            float calculatedMagnitude = Mathf.Sqrt(X * X + Z * Z);//calcul de la magnitude
            if (calculatedMagnitude >= 1)// si la magnitude est supérrieur a 1
            {
                ClampVelocitySpeed = ClampVelocitySpeed.normalized; //la remet a 1
            }
            float signe = Z / Mathf.Abs(Z);// permet d'avoir le signe 
            ClampVelocitySpeed = ClampVelocitySpeed * F_SpeedDeplacementClassic;// multiplie celle par la valeur souhaiter
            //Vector3 FutureVelocity = new Vector3(0, RbPlayer.velocity.y, ClampVelocitySpeed.y*signe);
            Vector3 FutureVelocity = new Vector3(0, RbPlayer.velocity.y, 1 * F_SpeedDeplacementClassic);
            RbPlayer.velocity = transform.TransformDirection(FutureVelocity);//fais en sorte qu'il le fasse de maniére local*/

          




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


    public void BobsleigDeplacement(float X, float Z) 
    {

        RbPlayer.velocity = new Vector3(RbPlayer.velocity.x, RbPlayer.velocity.y, F_SpeedBobSleigueCote * -X);
        //RbPlayer.AddForce(Vector3.left * Z * 1000*Time.deltaTime);

    }

    public void DeplacementCote() 
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,Vector3.down,out hit,100, ~(1<<9)))
        {
            directionEncote = Vector3.Cross(Vector3.back, hit.normal).normalized * F_SpeedBobSleigue;
           
        }
        //RbPlayer.AddForce(directionEncote.normalized * F_SpeedBobSleigue, ForceMode.VelocityChange);
        if (surLeSol) 
        {
            Vector3 Vitesse = new Vector3(directionEncote.x, RbPlayer.velocity.y, directionEncote.z);
            RbPlayer.velocity = Vitesse;
        }
       

    }

    public void Jump() 
    {
        if (nombreDeJump<2)
        {
            RbPlayer.AddForce(Vector3.up * F_JumpForce, ForceMode.Impulse);// applique une force impulse vers le haut

            nombreDeJump++;
        }          
        
    
       

    }

    public void Dash(Ray raycam) 
    {
        Vector3 DirectionCam= raycam.GetPoint(GDDP.DistanceCaméraPoint); //point de visé
        Vector3 DirectionDash = DirectionCam - transform.position;

        RbPlayer.AddForce( DirectionDash.normalized*GDDP.ForceDash*RbPlayer.mass ,ForceMode.Impulse);
        GDF.LancerDeBoule(-DirectionDash);


        Debug.DrawRay(transform.position, DirectionDash * GDDP.DistanceCaméraPoint,Color.green);
        Debug.DrawRay(transform.position, -DirectionDash * GDDP.DistanceCaméraPoint, Color.yellow);
        
        // récupére direction de la caméra
        // calculer la direction du dash
        // lancer les cubes en arriére
        // lancer la force



    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Sol"))
        {
            nombreDeJump = 0;
            surLeSol = true;
            //CanMoveBasic = true;
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
        if (collision.transform.CompareTag("Sol"))
        {
            surLeSol = false;
        }
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
