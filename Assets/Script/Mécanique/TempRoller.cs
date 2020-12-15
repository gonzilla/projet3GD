using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempRoller : PersonnalMethod
{
    //Script pour les "Rollers"
    //Public variable
    public GameObject roller1, Roller2;
    //Local variable
    GestionDataNonJoueur MyGDNJ;
    GestionDesDatasPlayer MyGDDP;
    DeplacementJ DJ;
    GrappinV2 G2;
    //
    float intitalSpeedHook, InitialSpeedDeplacement, InitialForcejump, InitialDistanceGrappin;
    float NewSpeedHook, NewSpeedDeplacement, NewJumpForce, NewDistanceGrappin;
    int nombreDeCube ;
    void Start()
    {
        setValueInitial();
    }

    
    
    public void GestionRoller() 
    {
        //Calcul des nouvelle variables
        nombreDeCube = MyGDNJ.MesPetitsCube.Count;
        NewSpeedHook =intitalSpeedHook-(MyGDNJ.VitesseGrappinParCube * nombreDeCube);
        NewSpeedDeplacement =InitialSpeedDeplacement - (MyGDNJ.DiminutionVitesseParCube * nombreDeCube);
        NewJumpForce = InitialForcejump - (MyGDNJ.ForceDeSautEnMoinsParCube*nombreDeCube);
        NewDistanceGrappin = InitialDistanceGrappin + (MyGDNJ.DistanceGrappinSuppParCube * nombreDeCube);
        //Impose des limites
        if (NewSpeedHook < 15)
        {
            NewSpeedHook = 15;
        }
        if (NewSpeedDeplacement < 3)
        {
            NewSpeedDeplacement = 3;
        }
        if (NewJumpForce < 2)
        {
            NewJumpForce = 2;
        }
        print("Ma new speedHook" + NewSpeedHook + " New SpeedDeplacement" + NewSpeedDeplacement + " jumpForce" + NewJumpForce);
        //Applications des variables
        G2.SpeedHook = NewSpeedHook;
        DJ.F_SpeedDeplacementClassic = NewSpeedDeplacement;
        DJ.F_JumpForce=NewJumpForce;
        MyGDDP.DistanceMaxGrappin = NewDistanceGrappin;
        ActiveRoller();
    }
    

    void setValueInitial() // récupére les variables dans leurs états d'origine
    {
        GoFindDataNonJoueur(out MyGDNJ);//methode perso
        GoFindDataPlayer(out MyGDDP);//methode perso
        DJ = GetComponent<DeplacementJ>();
        G2 = GetComponent<GrappinV2>();


        InitialDistanceGrappin = MyGDDP.DistanceMaxGrappin;
        InitialForcejump = DJ.F_JumpForce;
        InitialSpeedDeplacement = DJ.F_SpeedDeplacementClassic;
        intitalSpeedHook = G2.SpeedHook;
    }

    void ActiveRoller() //active les mesh des rollers
    {
        if (nombreDeCube==0)
        {
            roller1.SetActive(false);
            Roller2.SetActive(false);
        }
        else 
        {
            roller1.SetActive(true);
            Roller2.SetActive(true);
        }
    
    }
}
//Précédent systeme
/*if (MyGDNJ.MesPetitsCube.Count>0 && MyGDNJ.MesPetitsCube.Count <28)
       {
           print("Bonus");
           NewSpeedHook = intitalSpeedHook* Mult_Hook;
           NewSpeedDeplacement = InitialSpeedDeplacement* Mult_Depla;
           NewJumpForce = InitialForcejump* Multi_Jump;
           NewDistanceGrappin = InitialDistanceGrappin* Mult_Grappin;
       }
       else if (MyGDNJ.MesPetitsCube.Count > 27)
       {
           print("Malus");
           NewSpeedHook = intitalSpeedHook / Mult_Hook;
           NewSpeedDeplacement = InitialSpeedDeplacement / Mult_Depla;
           NewJumpForce = InitialForcejump / Multi_Jump;
           NewDistanceGrappin = InitialDistanceGrappin / Mult_Grappin;
       }
       else if (MyGDNJ.MesPetitsCube.Count == 0)
       {
           print("Reset");
           NewSpeedHook = intitalSpeedHook;
           NewSpeedDeplacement = InitialSpeedDeplacement;
           NewJumpForce = InitialForcejump;
           NewDistanceGrappin = InitialDistanceGrappin;

       }*/
