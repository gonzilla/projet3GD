using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementJ : PersonnalMethod
{
    //Public variable
    //public float F_SpeedDeplacementClassic;// valeur de la vitesse
    //public Camera maCamera;
    //public float MagnitudeMax;
    //Local variable
    //public float F_SortieDeGrappin;
    //public float F_SpeedDeplacementRoller;
    public float F_JumpForce;//valeur de la force de jump
    public float F_SpeedBobSleigueCote;
    public float F_SpeedBobSleigue;
    public float F_SpeedDash;
    public float vitesseDecrementation;
    //public bool CanMoveBasic = true;
    [HideInInspector] public bool afterGrap = false;
    
    
    Rigidbody RbPlayer;// le component rigidbody
    GestionDesDatasPlayer GDDP;
    GestionDataNonJoueur GDNJ;
    Vector3 directionEncote;
    bool surLeSol;
    int nombreDeJump;
    /*float solDistOri;
    float solDistanceMtn;
    float pourcentage;*/

   void Start()
    {
        GoFindDataPlayer(out GDDP);
        GoFindDataNonJoueur(out GDNJ);
        RbPlayer = GetComponent<Rigidbody>();//Récupére le component rigidbody de l'objet
        
    }

    public void BobsleigDeplacement(float X, float Z)
    {

        RbPlayer.velocity = new Vector3(RbPlayer.velocity.x, RbPlayer.velocity.y, F_SpeedBobSleigue * -X);
        //RbPlayer.AddForce(Vector3.left * Z * 1000*Time.deltaTime);

    }

    public void DeplacementCote()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100, ~(1 << 9)))
        {
            directionEncote = Vector3.Cross(Vector3.back, hit.normal).normalized * F_SpeedBobSleigueCote;
           
            Debug.DrawRay(transform.position, directionEncote);
        }
        //RbPlayer.AddForce(directionEncote.normalized * F_SpeedBobSleigue, ForceMode.VelocityChange);
        if (surLeSol)//&&!GDDP.LanceUnAutreDepl
        {
            if (RbPlayer.velocity.magnitude<F_SpeedBobSleigueCote)
            {
                RbPlayer.AddForce(directionEncote, ForceMode.Force);
            }
            if (RbPlayer.velocity.magnitude>F_SpeedBobSleigueCote)
            {
                Vector3 Incroyable = RbPlayer.velocity;
                float oldMagnitude = Incroyable.magnitude;
                float newMagnitude =oldMagnitude - vitesseDecrementation*Time.deltaTime;
                RbPlayer.velocity = RbPlayer.velocity.normalized * newMagnitude;
            }
            //Vector3 Vitesse = new Vector3(directionEncote.x, RbPlayer.velocity.y, directionEncote.z);
            
        }


    }

    public void Jump()
    {
        if (nombreDeJump < 2)
        {
            RbPlayer.AddForce(Vector3.up * F_JumpForce, ForceMode.Impulse);// applique une force impulse vers le haut
            //GDDP.LanceUnAutreDepl = false;
            nombreDeJump++;
        }




    }

    public void Dash(Ray raycam)
    {
        //Enleve Du Poids
        
        int NombreAEnlever = (int)Mathf.Pow(GDNJ.NombreDecube, 3)*(int)GDDP.NombreDeGrosCubeParDash;
        
        Vector3 DirectionCam = raycam.direction; //point de visé
        //Vector3 DirectionDash = DirectionCam - transform.position;
        GDDP.GDF.LancerDeCertainCube(-DirectionCam, NombreAEnlever);
        RbPlayer.AddForce(DirectionCam.normalized * F_SpeedDash*RbPlayer.mass, ForceMode.Impulse);
        //GDDP.LanceUnAutreDepl = false;

        /*Vector3 DirectionCam = raycam.GetPoint(GDDP.DistanceCameraPoint); //point de visé
        Vector3 DirectionDash = DirectionCam - transform.position;

        RbPlayer.AddForce(DirectionDash.normalized * GDDP.ForceDash * RbPlayer.mass, ForceMode.Impulse);
        GDDP.GDF.LancerDeBoule(-DirectionDash);


        Debug.DrawRay(transform.position, DirectionDash * GDDP.DistanceCameraPoint, Color.green);
        Debug.DrawRay(transform.position, -DirectionDash * GDDP.DistanceCameraPoint, Color.yellow);*/

        // récupére direction de la caméra
        // calculer la direction du dash
        // lancer les cubes en arriére
        // lancer la force



    }

    /*public void ComeBackSol (float DistanceSolOri)
    {
        solDistOri = DistanceSolOri;
        afterGrap = true;
    }*/

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Sol"))
        {
            nombreDeJump = 0;
            surLeSol = true;
            afterGrap = false;
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

/*public void DeplacementRoller(float X, float Z)
    {
        float targetAngle = Mathf.Atan2(X, Z) * Mathf.Rad2Deg + maCamera.transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        if (RbPlayer.velocity.magnitude < F_SpeedDeplacementRoller)
        {
            RbPlayer.AddForce(transform.TransformDirection(Vector3.forward) * F_SpeedDeplacementRoller, ForceMode.Force);
        }

    }*/











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
/*public void DeplacementClassic(float X, float Z)//Lance le déplacement //marche un peu par magie noir en vrais

   {
       //print(CanMove);
       if (!GDNJ.Grap.Activate && CanMoveBasic)
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
           RbPlayer.velocity = transform.TransformDirection(FutureVelocity);//fais en sorte qu'il le fasse de maniére local






       }


   }*/
/*private void FixedUpdate()
{
    RaycastHit hit;
    Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, ~(1 << 9));
    solDistanceMtn = Vector3.Distance(hit.point, transform.position);
    if (afterGrap)
    {
        pourcentage=solDistOri / solDistanceMtn;
    }
    float newY = RbPlayer.velocity.y - 9.81f*(1+1*pourcentage);//+ *pourcentage
    RbPlayer.velocity = new Vector3(RbPlayer.velocity.x, newY, RbPlayer.velocity.z);

}*/
