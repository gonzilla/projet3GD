using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilHarpon : PersonnalMethod
{
    //Public variable
    

    //Local variable
    GestionDataNonJoueur myGDNJ;
    GestionDesDatasPlayer myGDDP;
    Vector3 RaycastPoint;
    Vector3 PositionOriginille;
    Vector3 OldPosition;
    CubeDivision CD;
    Rigidbody RbSelf;
    RaycastHit Hitted;
    Ray thisRay;
    float maxDistance;
    float DistanceParcourue;

    void Awake()
    {
        GoFindDataPlayer(out myGDDP);
        GoFindDataNonJoueur(out myGDNJ);
    }
    

    void FixedUpdate()
    {
        DistanceParcourue=Vector3.Distance(transform.position,PositionOriginille);
        if (DistanceParcourue>maxDistance)
        {
            Destroy(this.gameObject);
        }
        Debug.DrawRay(PositionOriginille, RaycastPoint, Color.cyan);
        //float LongueurRaycast = Vector3.Distance(transform.position, OldPosition);
        //RaycastHit Hit;
        /*if (Physics.Raycast(OldPosition,transform.position,out Hit,LongueurRaycast))
        {
            RaycastPoint = Hit.point;
            myGDNJ.B_Coller = true;
            DetermineFonction(Hit.transform.gameObject);
        }*/
    }

    void LateUpdate()
    {
        OldPosition = transform.position;
    }

    public void Enclenche(Rigidbody Self,RaycastHit Info) 
    {
        
        RbSelf = Self;
        Hitted = Info;
        PositionOriginille = transform.position;
        Vector3 DirectionProjectile = Vector3.zero;
        maxDistance = CalculDistanceGrappin();

        if (Info.transform!=null)
        {
           DirectionProjectile = Info.point - transform.position;//calcul la direction
            
        }
        if(Info.transform == null)
        {
            thisRay = myGDNJ.Cam.ScreenPointToRay(Input.mousePosition);// le ray
            DirectionProjectile = thisRay.GetPoint(maxDistance) -transform.position;//calcul la direction thisRay.direction;//
        }
       
        //Vector3 DirectionProjectile = thisRay.direction-transform.position;//calcul la direction
        RbSelf.AddForce(DirectionProjectile.normalized * myGDNJ.F_VitesseHarpon, ForceMode.Impulse);
        
    //Prendre la position d'origine
    //
    }

    void DetermineFonction(GameObject ElementToucher) 
    {
       
        switch(FaitUneFonction(ElementToucher)) 
        {
            case 0:
                Destroy(this.gameObject);
                break;
            case 1:
                myGDNJ.EV.DestroyElementVisuelle();
                if (ElementToucher.GetComponent<CubeDivision>())//si l'objet n'a pas le script
                {
                    CD = ElementToucher.AddComponent<CubeDivision>();//ajoute le script

                }
                else //sinon
                {
                    CD = ElementToucher.GetComponent<CubeDivision>();//recupére le script
                }
               
                CD.ActivateDivision();//dans le script CubeDivisions
                break;

            case 2:
                
                myGDNJ.Grap.RushTo(RaycastPoint);// déplace le joueur vers l'endroit visé
                break;
        
        }   
    }


    

    void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
        {
            myGDNJ.B_Coller = true;
            RbSelf.velocity = Vector3.zero;
            RaycastPoint = transform.position;
            DetermineFonction(other.transform.gameObject);
        }
       
    }

    float CalculDistanceGrappin() 
    {
        float pourcentage = myGDDP.tauxMaintien;
        float ecart = (myGDNJ.DistanceMaxGrappin - myGDNJ.DistanceMinGrappin);
        float application = myGDNJ.DistanceMinGrappin + ecart * pourcentage;
        myGDNJ.ValueCalculate = application;
        return application;

    }

    int FaitUneFonction (GameObject element) 
    {

        int DoitFaireUnTruc = 0;
        string TagObjet = element.tag;
        foreach (string Attire in myGDNJ.ObjetDivisable)
        {
            if (TagObjet==Attire)
            {
                
                DoitFaireUnTruc = 1;
                break;
            }
        }
        foreach (string ObjetAccrochable in myGDNJ.ListeDeTagObjetAccrochable)
        {
            if (TagObjet == ObjetAccrochable)
            {
                
                DoitFaireUnTruc = 2;
                break;
            }
        }

        
        return DoitFaireUnTruc;
    }
   
    float CalculDistancePourPoint() 
    {
        //calculate 
        float distancePlAYERGUN = Vector3.Distance(GameObject.Find("Player").transform.position,PositionOriginille);

        float distanceCalculer =  Mathf.Sqrt(Mathf.Pow(distancePlAYERGUN, 2)+ Mathf.Pow(maxDistance, 2));

        return distanceCalculer;
    }
}
