using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionDuPoids : PersonnalMethod
{
    //Public variable
   
    //Local variable
    GestionDataNonJoueur MyGDNJ;
    GestionDesDatasPlayer MyGDDP;
    Rigidbody RbPlayer;

    int nombreDeCube;
    float massOriginal;
    float speedOriginal;
    float speedOriginalCote;
    float Jumporiginal;
    float NewSpeedCote;
    float NewSpeed;
    float NewJump;

    void Awake()
    {
        //setValueInitial();
        RbPlayer = GetComponent<Rigidbody>();
        GoFindDataPlayer(out MyGDDP);
        GoFindDataNonJoueur(out MyGDNJ);
        massOriginal = RbPlayer.mass;
        speedOriginal = MyGDDP.DJ.F_SpeedBobSleigue;
        speedOriginalCote = MyGDDP.DJ.F_SpeedBobSleigueCote;
        Jumporiginal = MyGDDP.DJ.F_JumpForce;
    }

    public void CalculDuPoids() 
    {
        //récupération du nombre de cube
        nombreDeCube = MyGDNJ.MesPetitsCube.Count;
        //calcul de ma nouvelle masse
        float maNewMasse = massOriginal + (float)nombreDeCube * MyGDNJ.poidsCube;
        //j'applique ma masse
        RbPlayer.mass = maNewMasse;
        // je calcule ma nouvelle vitesse sur les cotés
        NewSpeed = speedOriginal - nombreDeCube * MyGDNJ.forceByCube * MyGDNJ.VitesseScalling.Evaluate((float)nombreDeCube / (MyGDNJ.nombreDeGrosCubeMax * Mathf.Pow(MyGDNJ.NombreDecube,3)));

        // je calcule ma nouvelle vitesse en ligne droite 
        NewSpeedCote = speedOriginalCote + nombreDeCube * MyGDNJ.forceByCubeCote;

        //Je calcule force pour sauter
        NewJump = Jumporiginal + nombreDeCube * MyGDNJ.forceJumpByCube * MyGDNJ.VitesseScalling.Evaluate((float)nombreDeCube / (MyGDNJ.nombreDeGrosCubeMax * Mathf.Pow(MyGDNJ.NombreDecube, 3)));

        //j'applique mes calculs

        MyGDDP.DJ.F_SpeedBobSleigueCote = NewSpeedCote;
        MyGDDP.DJ.F_SpeedBobSleigue = NewSpeed;
        MyGDDP.DJ.F_JumpForce = NewJump;
    }

}
