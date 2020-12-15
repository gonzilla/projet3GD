using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionDataNonJoueur : MonoBehaviour
{
    //Script pour modifier des variables communes (sur d'autre script que le joueur)
    //Public variable
    [Header("CubeDivisions")]
    public float ForceExplosions;
    public int NombreDecube; // le nombre de cube
    public List<Rigidbody> MesPetitsCube = new List<Rigidbody>();
    public List<Telekynesys> telekynesysScript = new List<Telekynesys>();
    public float DiminutionVitesseParCube;
    public float DistanceGrappinSuppParCube;
    public float VitesseGrappinParCube;
    public float ForceDeSautEnMoinsParCube;

    //Local variable

    /*private void Update()
    {
        print(MesPetitsCube.Count+" "+telekynesysScript.Count);
    }*/
}
