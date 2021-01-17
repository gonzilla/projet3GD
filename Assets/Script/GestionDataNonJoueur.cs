using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionDataNonJoueur : MonoBehaviour
{
    //Script pour modifier des variables communes (sur d'autre script que le joueur)
    //Public variable

    [Header("Pour Tout Scipt")]
    public GameObject Player;


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
    public float forceJumpByCube;
    public AnimationCurve VitesseScalling;


    [Header("Autre")]
    public CinemachineChangement CC;


    [Header("ElementVisuel")]
    public Transform Trans_PositionSpawn;
    public float F_tailleDuCube;
    public float F_VitesseElement;
    public Material Mat_materialImpact;
    public Material Mat_Corde;
    public bool B_Coller;
    public bool Gauche;


    [Header("Grappin")]
    public string[] ListeDeTagObjetAccrochable;

    

    //Local variable

    /*private void Update()
    {
        print(MesPetitsCube.Count+" "+telekynesysScript.Count);
    }*/
}
