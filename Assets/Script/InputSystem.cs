﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : PersonnalMethod
{
    //Script de gestion des Inputs
    //Public variable
    //Pour compresser un maximum je créer des tableau //cela oblige a faire un devoir de mémorisation de l'ordre// permet d'utiliser le même int pour plz info
    //hide in inspecteur= pas visible dans l'inspecteur
    //header fais une démarquation dans l'inspecteur
    public Camera MaCam;
    public CinemachineChangement CC;
    public int LayerPetitCube;
    public GestionDesDatasPlayer GDDP;
    public KeyCode[] Touches; //tableau des touches                                      
    [HideInInspector]public float[] TimeMaintenueTouche; // le temps que le joueur maintien cette touche 
    [HideInInspector]public float[] TimePressionTouche; // le temps dans le jeu où le joueur a appuyé sur cette touche 
    //[Header("Axes")]
    public string[] Axes; //Tableau des Inputs qui ont besoins d'une valeur de pression
    [HideInInspector] public float[] TimeMaintenueAxes;//le temps que le joueur maintien cette input
    [HideInInspector] public float[] TimePressionAxes;// le temps dans le jeu où le joueur a appuyé sur cette input
    //[Header("Autre")]
    public float timeMinMaintien;// temps à partir duquel on décide qu'une touche est maintenue volontairement 
    //Local variable
    //compenent pour lancements des effets des Inputs
    [SerializeField] bool BobSleig;
    GestionDataNonJoueur MyGDNJ;
    GestionDesDatasPlayer MyGDDP;
    GestionDesFormes GDF;
    DeplacementJ DJ; 
    GrappinV2 Grap;
    CubeDivision CD;
    TempRoller TR;
    ElementsVisuelle EV;
    RaycastHit hittedCam;//stock la valeur du raycast
    int maskToIgnore;//le mask a ignoré pour le raycast
     void Awake()
    {
        GoFindDataNonJoueur(out MyGDNJ);
        GoFindDataPlayer(out MyGDDP);
    }

    void Start()
    {
        DJ = GetComponent<DeplacementJ>();// vas chercher le compenent pour accerder/ modifier à des choses dedans
        Grap = GetComponent<GrappinV2>();
        CD = GetComponent<CubeDivision>();
        GDF = GetComponent<GestionDesFormes>();
        TR = GetComponent<TempRoller>();
        EV = GetComponent<ElementsVisuelle>(); 
        MAJ_Tab();//met à jour la longueur des tableau 
        maskToIgnore= ~(1<< LayerPetitCube);//set la valeur du layer physique
    }

    void Update()
    {
        
        Ray thisRay = MaCam.ScreenPointToRay(Input.mousePosition);// le ray
        Debug.DrawRay(MaCam.transform.position, MaCam.transform.forward * 100, Color.red);

        #region Input 0
        if (Input.GetKeyDown(Touches[0]))//si la touche 0 dans le tableau a été pressé
        {
            TimePressionTouche[0] = Time.time + timeMinMaintien;
        }
        if (Input.GetKey(Touches[0]))
        {
            if (TimePressionTouche[0] < Time.time)
            {
                GDDP.Vise = true;
                CC.Viser();
            }

        }
        if (Input.GetKeyUp(Touches[0]))
        {
           
            TimePressionTouche[0] = 0;
            if (GDDP.Vise)
            {
               
                if (Physics.Raycast(Camera.main.transform.position, thisRay.direction, out hittedCam, GDDP.DistanceMaxGrappin, maskToIgnore) && hittedCam.transform.CompareTag("Destructible"))
                {
                    if (hittedCam.transform!=null)
                    {
                        
                        MyGDNJ.Gauche = true;
                        EV.LanceGrappin(hittedCam.point, hittedCam);
                    }
                    
                }
                            
            }
            GDDP.Vise = false;
            CC.StopViser();
            hittedCam = new RaycastHit();
            

        }
        #endregion
        if (Input.GetKeyDown(Touches[1]))//si la touche 1 dans le tableau a été pressé
        {
            DJ.Jump();//lance le saut
            //Grap.Cancel();//cancel le grappin
        }
        #region Input2
        if (Input.GetKeyDown(Touches[2]))// si la touche 2 dans le tableau a été pressé
        {
            TimePressionTouche[2] = Time.time + timeMinMaintien;
            
           
        }
        if (Input.GetKey(Touches[2]))
        {
            if (TimePressionTouche[2]<Time.time)
            {
                GDDP.Vise = true;
                CC.Viser();
            }
            
        }
        if (Input.GetKeyUp(Touches[2]))
        {
            
            TimePressionTouche[2] = 0;
            if (GDDP.Vise)
            {
                if (Physics.Raycast(Camera.main.transform.position, thisRay.direction, out hittedCam, GDDP.DistanceMaxGrappin, maskToIgnore) && Grap.EstIlGrappinable(hittedCam.transform.tag))
                {
                    MyGDNJ.Gauche = false;
                    EV.LanceGrappin(hittedCam.point,hittedCam);
                }
                //Physics.Raycast(Camera.main.transform.position, thisRay.direction, out hittedCam, GDDP.DistanceMaxGrappin, maskToIgnore);              
            }
            GDDP.Vise = false;
            CC.StopViser();
            hittedCam = new RaycastHit();
            //DJ.CanMoveBasic = false;//empeche le joueur de bouger
            //je créer un lien si je fais click gauche je divise l'objet si possible
            //si je fais click droit je m'attire

        }

        #endregion

        if (Input.GetKeyDown(Touches[3]))// si la touche 3 dans le tableau a été pressé
        {
            
            if (MyGDNJ.MesPetitsCube.Count>0 && !MyGDDP.Using_Tele)//si dans mes data j'ai des cubes
            {
                foreach (Telekynesys item in MyGDNJ.telekynesysScript)//à changer lorsque vrais controle
                {
                    item.changeLayerBack();
                }
                GDF.LancerDeBoule(thisRay.direction);//lance la boule
                TR.GestionRoller();//desactive roller
                
            }
        }

        if (Input.GetKeyDown(Touches[4]))
        {
            if (MyGDNJ.MesPetitsCube.Count>0)// si j'ai des cubes sur moi
            {
                
                DJ.Dash(thisRay);//lance le dash
                TR.GestionRoller();//desactive roller
            }
            
        }
        
      
    }


    private void FixedUpdate()
    {

        if (Input.GetAxis(Axes[0]) != 0 || Input.GetAxis(Axes[1]) != 0)//si l'un de ces deux inputs est différent de 0
        {

            float X = Input.GetAxis(Axes[0]);//valeur de pression de l'input
            float Z = Input.GetAxis(Axes[1]);//valeur de pression de l'input
            if (BobSleig)
            {
                DJ.BobsleigDeplacement(X, Z);
            }
            else 
            {
                if (MyGDNJ.telekynesysScript.Count == 0)
                {
                    DJ.DeplacementClassic(X, Z);// demande d'utiliser le systeme de depalcment calssic et lui transmet les valeurs necessaire
                }
                else if (MyGDNJ.telekynesysScript.Count > 0)
                {
                    DJ.DeplacementRoller(X, Z);
                }
            }
            
            


        }
        else if (Input.GetAxis(Axes[0]) == 0 || Input.GetAxis(Axes[1]) == 0)
        {
            DJ.DeplacementCote();
        }

       
    }


    void MAJ_Tab() 
    {
        TimeMaintenueAxes = new float[Touches.Length];//set la longueur sur celle de Touche[]
        TimePressionTouche = new float[Touches.Length];//set la longueur sur celle de Touche[]
        //
        TimePressionAxes = new float[Axes.Length];//set la longueur sur celle de Axes[]
        TimeMaintenueAxes = new float[Axes.Length];//set la longueur sur celle de Axes[]
    }







}
/*if (Input.GetAxis(Axes[2]) != 0)
      {
          Grap.ChangementLongueurGrappin(Input.GetAxis(Axes[2]));
      }*/
/*if (Physics.Raycast(Camera.main.transform.position, thisRay.direction, out hittedCam, GDDP.DistanceMaxGrappin, maskToIgnore))
      {

          Grap.Cancel();// annule le grappin (si y'en a déjà un)
          Grap.RushTo(hittedCam);// déplace le joueur vers l'endroit visé
          DJ.CanMoveBasic=false;//empeche le joueur de bouger

      }*/

/*if (Physics.Raycast(Camera.main.transform.position, thisRay.direction, out hittedCam, GDDP.DistanceMaxGrappin, maskToIgnore)&& !Input.GetKey(Touches[2]))//Condition de visé
        {
            if (hittedCam.transform.CompareTag("Destructible"))//condition objet visé
            {

                if (!hittedCam.transform.GetComponent<CubeDivision>())//si l'objet n'a pas le script
                {
                    CD = hittedCam.transform.gameObject.AddComponent<CubeDivision>();//ajoute le script
                    //CD.NombreDecube = 3;
                }
                else //sinon
                {
                    CD = hittedCam.transform.GetComponent<CubeDivision>();//recupére le script
                }

                CD.ActivateDivision();//dans le script CubeDivisions
                TR.GestionRoller();//active dans script des rollers
            }
        }
        else if (Input.GetKey(Touches[2]))
        {

            if (Physics.Raycast(Camera.main.transform.position, thisRay.direction, out hittedCam, GDDP.DistanceMaxGrappin, maskToIgnore))
            {

                Grap.Cancel();// annule le grappin (si y'en a déjà un)
                Grap.RushTo(hittedCam);// déplace le joueur vers l'endroit visé
                DJ.CanMoveBasic = false;//empeche le joueur de bouger

            }
        }*/
/*if (hittedCam.transform.gameObject != null && hittedCam.transform.CompareTag("Destructible")) // vieu clic gauche
    {
        print("je touche un cube");
        if (!hittedCam.transform.GetComponent<CubeDivision>())//si l'objet n'a pas le script
        {
            CD = hittedCam.transform.gameObject.AddComponent<CubeDivision>();//ajoute le script

        }
        else //sinon
        {
            CD = hittedCam.transform.GetComponent<CubeDivision>();//recupére le script
        }

        CD.ActivateDivision();//dans le script CubeDivisions
        TR.GestionRoller();//active dans script des rollers
        hittedCam = new RaycastHit();

    }
    */
