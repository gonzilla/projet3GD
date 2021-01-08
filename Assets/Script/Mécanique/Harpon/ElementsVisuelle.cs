using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsVisuelle : PersonnalMethod
{
    //Public variable
   
    //Local variable
    GestionDataNonJoueur myGDNJ;
    GestionDesDatasPlayer myGDDP;
    GameObject ImpactPoint;
    GameObject Player;
    bool Moving;
    Vector3 oldPosition;
    LineRenderer LR;
    void Start()
    {
        GoFindDataPlayer(out myGDDP);
        GoFindDataNonJoueur(out myGDNJ);
        Player = transform.parent.gameObject;
    }

    public void LanceGrappin(Vector3 PointImact) 
    {
        ImpactPoint = GameObject.CreatePrimitive(PrimitiveType.Cube);//crée un cube
        ImpactPoint.transform.localScale = Vector3.one * myGDNJ.F_tailleDuCube; // lui donne la bonne taille
        ImpactPoint.transform.rotation = Player.transform.rotation;//une rotation
        ImpactPoint.GetComponent<MeshRenderer>().material=myGDNJ.Mat_materialImpact;// lui met un material
        ImpactPoint.GetComponent<Collider>().isTrigger = true;//le met en trigger
        oldPosition = ImpactPoint.transform.position;//set la position
        Rigidbody RB= ImpactPoint.AddComponent<Rigidbody>();//ajoute de la physique
        RB.useGravity = false;//pas de gravité
        Vector3 DirectionProjectile=PointImact-transform.position;//calcul la direction
        RB.AddForce(DirectionProjectile.normalized * myGDNJ.F_VitesseElement, ForceMode.Impulse);//lance le projectille
        Moving = true;//lui dit qu'il bouge mtn
       
    }
    public void DestroyElementVisuelle() 
    {
        if (ImpactPoint!=null)
        {
            Destroy(ImpactPoint);
        }
        
    }
     void Update()
    {
        if (myGDNJ.B_Coller)
        {
            actualiseLine();
        }
    }

    void LateUpdate()
    {

        if (ImpactPoint!=null)
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
        LR = this.gameObject.AddComponent<LineRenderer>();
        LR.SetPosition(0, transform.position);//set le point 0
        LR.SetPosition(1, ImpactPoint.transform.position);//set le point 1
        LR.startWidth = 0.10f;//la "grosseur" de corde
    }

    void actualiseLine() 
    {
        LR.SetPosition(0, transform.position);//set le point 0
        LR.SetPosition(1, ImpactPoint.transform.position);//set le point 1

    }

}
