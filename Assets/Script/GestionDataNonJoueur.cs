using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionDataNonJoueur : MonoBehaviour
{
    //Script pour modifier des variables communes (sur d'autre script que le joueur)
    //Public variable


     
     public ElementsVisuelle EV;
     public GrappinV2 Grap;
     public CinemachineChangement CC;

    [Header("Pour Tout Scipt")]
    public GameObject Player;
    public Camera Cam;

    [Header("CubeDivisions")]
    public float ForceExplosions;
    public int NombreDecube; // le nombre de cube
    public List<Rigidbody> MesPetitsCube = new List<Rigidbody>();
    public List<Telekynesys> telekynesysScript = new List<Telekynesys>();
    /*public float DiminutionVitesseParCube;
    public float DistanceGrappinSuppParCube;
    public float VitesseGrappinParCube;
    public float ForceDeSautEnMoinsParCube;*/

    public int nombreDeGrosCubeMax;
    public float poidsCube;
    public float forceByCube;
    public float forceByCubeCote;
    public float forceJumpByCube;
    public AnimationCurve VitesseScalling;


    


    [Header("ElementVisuel")]
    public Transform Trans_PositionSpawn;
    public float F_tailleDuCube;
    public Material Mat_materialImpact;
    public Material Mat_Corde;
    public bool B_Coller;
    public bool Gauche;

    [Header("ProjectilHarpon")]
    public float F_VitesseHarpon;


    [Header("Grappin")]
    public string[] ListeDeTagObjetAccrochable;
    public string[] ObjetDivisable;
    public float DistanceMinGrappin;
    public float DistanceMaxGrappin;
    public float DistanceCassure;
    public float ValueCalculate;
    

    //[Header("Autre")]
    //public bool InAir;

    [Header("Cinemachine")]
    public float VitesseRetourY;
    public float VitesseRetourX;
    //Local variable

    /*private void Update()
    {
        print(MesPetitsCube.Count+" "+telekynesysScript.Count);
    }*/
}
