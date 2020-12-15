using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionDesFormes : PersonnalMethod
{
    //Script qui vas gérer les formes créer par le joueur
    //Public Variable
    public float ForceDeLancer;
    //Local variable

    GestionDataNonJoueur MyGDNJ;
    
    void Awake()
    {
        GoFindDataNonJoueur(out MyGDNJ);
    }


    public void LancerDeBoule(Vector3 Direction) //Lancer de boule
    {
        
        //print("Je lance La boule");
        Vector3 DirectionDeLancer= Direction.normalized;//calcul la direction pour le lancer
        foreach (Telekynesys item in MyGDNJ.telekynesysScript)
        {
            item.GO = false;// lui dis d'arreter à la position
        }
        foreach (Rigidbody Cubies in MyGDNJ.MesPetitsCube)
        {
            
            Cubies.AddForce(DirectionDeLancer*ForceDeLancer,ForceMode.Impulse);// lui donne une force pour l'expulser
        }
        MyGDNJ.telekynesysScript.Clear();//clear dans  data
        MyGDNJ.MesPetitsCube.Clear();//clear  dans data

    }
     
    
    
}
