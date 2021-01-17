using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsVisuelle : PersonnalMethod
{
    //Public variable
    
    //Local variable
    GestionDataNonJoueur myGDNJ;
    GestionDesDatasPlayer myGDDP;
    GrappinV2 Grap;
    GameObject ImpactPoint;
    bool Once =false;
   
    Vector3 oldPosition;
    RaycastHit Info;
    LineRenderer LR;
    CubeDivision CD;
    void Start()
    {
        GoFindDataPlayer(out myGDDP);
        GoFindDataNonJoueur(out myGDNJ);
        Grap = GetComponent<GrappinV2>();
        CD = GetComponent<CubeDivision>();

    }

    public void LanceGrappin(Vector3 PointImact, RaycastHit InfoFrom) 
    {
        
        
        DestroyElementVisuelle();
        Info = InfoFrom;
        
        ImpactPoint = GameObject.CreatePrimitive(PrimitiveType.Cube);//crée un cube
        ImpactPoint.transform.localScale = Vector3.one * myGDNJ.F_tailleDuCube; // lui donne la bonne taille
        ImpactPoint.transform.position = myGDNJ.Trans_PositionSpawn.position;
        ImpactPoint.transform.rotation = myGDNJ.Player.transform.rotation;//une rotation
        ImpactPoint.GetComponent<MeshRenderer>().material = myGDNJ.Mat_materialImpact;// lui met un material
        ImpactPoint.GetComponent<Collider>().isTrigger = true;//le met en trigger
        Rigidbody RB = ImpactPoint.AddComponent<Rigidbody>();//ajoute de la physique
        RB.useGravity = false;//pas de gravité
        Vector3 DirectionProjectile = PointImact - transform.position;//calcul la direction
        RB.AddForce(DirectionProjectile.normalized * myGDNJ.F_VitesseElement, ForceMode.Impulse);
        
        oldPosition = ImpactPoint.transform.position;

    }
    public void DestroyElementVisuelle() 
    {
        if (ImpactPoint!=null)
        {
            myGDNJ.B_Coller = false;
            Destroy(ImpactPoint);
        }
        
    }
     void Update()
    {
        if (myGDNJ.B_Coller)
        {
            actualiseLine();
            if (!Once)
            {
                Once = true;
                //Grap.Cancel();// annule le grappin (si y'en a déjà un)
                if (myGDNJ.Gauche)
                {
                    
                        
                        if (!Info.transform.GetComponent<CubeDivision>())//si l'objet n'a pas le script
                        {
                            CD = Info.transform.gameObject.AddComponent<CubeDivision>();//ajoute le script

                        }
                        else //sinon
                        {
                            CD = Info.transform.GetComponent<CubeDivision>();//recupére le script
                        }

                        CD.ActivateDivision();//dans le script CubeDivisions

                        DestroyElementVisuelle();
                    //Info = new RaycastHit();

                }
                else
                {
                    Grap.RushTo(Info);// déplace le joueur vers l'endroit visé
                    //Info = new RaycastHit();
                }
               
            }
        }
        
    }

    void LateUpdate()
    {
        
        if (ImpactPoint!=null )
        {
           
            float distance = Vector3.Distance(oldPosition, ImpactPoint.transform.position);//calcul la distance
            Vector3 Direction = ImpactPoint.transform.position - oldPosition;//la direction
            RaycastHit hit;//stock les variables

            if (Physics.Raycast(oldPosition, Direction, out hit, distance))//test physique
            {
                if (hit.transform.gameObject != ImpactPoint)//condition
                {
                    ImpactPoint.transform.position = hit.point;//set la position
                    myGDNJ.B_Coller = true;//informe que c'est collé
                   
                    setLine();
                }
            }
            if (oldPosition != ImpactPoint.transform.position && !myGDNJ.B_Coller)
            {
                oldPosition = ImpactPoint.transform.position;
            }
        }
        
        

        
    }

    public void setLine() 
    {
        if (LR==null)
        {
            LR = ImpactPoint.AddComponent<LineRenderer>();
            LR.material = myGDNJ.Mat_Corde;
            LR.SetPosition(0, myGDNJ.Trans_PositionSpawn.position);//set le point 0
            LR.SetPosition(1, ImpactPoint.transform.position);//set le point 1
            LR.startWidth = 0.10f;//la "grosseur" de corde
            //Once = false;
        }
        
    }

    void actualiseLine() 
    {
        LR.SetPosition(0, myGDNJ.Trans_PositionSpawn.position);//set le point 0
        LR.SetPosition(1, ImpactPoint.transform.position);//set le point 1

    }

}
