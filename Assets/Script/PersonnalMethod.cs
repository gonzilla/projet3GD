using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonnalMethod : MonoBehaviour
{
    //Public variable


    //Local variable
    static GestionDataNonJoueur GdnjFind;
    static GestionDesDatasPlayer gddpFind;

    private void Awake()
    {
        
    }

    public static bool test(bool free, out string Montext) 
    {
        if (free)
        {
            Montext = "prends la rouge";
            return false;
        }
        else 
        {
            Montext = "prends la bleue";
            return true;
        }
        
    }

    public static void GoFindDataNonJoueur(out GestionDataNonJoueur gdnj) 
    {
        if (GdnjFind == null)
        {
            GdnjFind = GameObject.Find("GestionData").GetComponent<GestionDataNonJoueur>();
        }
        gdnj = GdnjFind;

    }
    public static void GoFindDataPlayer(out GestionDesDatasPlayer gddp) 
    {
        if (gddpFind == null)
        {
            gddpFind = GameObject.Find("GestionData").GetComponent<GestionDesDatasPlayer>();
        }
        gddp = gddpFind;

    }

    
   
}
