using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekynesys : PersonnalMethod
{
    //Public variable
     public bool GO = false;
     


    //Local variable
    //bool going = false;
    Rigidbody RB;
    
    
    GestionDesDatasPlayer GDDP;

    void Awake()
    {
          GoFindDataPlayer(out GDDP);
    }

    void Start()
    {
        //RBcible = Cible.GetComponent<Rigidbody>();
        
    }

    
    void Update()
    {
        if (GO)
        {
            if (transform.position != GDDP.Cible.position)// script de déplacement
            {
                Vector3 direction = GDDP.Cible.position - transform.position;
                RB.velocity = direction.normalized * GDDP.SpeedTelekinesy;
            }
            if (Vector3.Distance(GDDP.Cible.position, transform.position) < GDDP.DistanceMinTelekinesy)//pas vraiment utile // le garde au cas ou
            {
                transform.position = GDDP.Cible.position;
            }
        }
    }

    public void GoToCoordonate() 
    {
       
        RB = GetComponent<Rigidbody>();
        GDDP.Using_Tele = true;//dis au systeme que je ramene des petis cube
        Invoke("TimeSkip", GDDP.TempsAvantDebutTelekinesy);//laisse le temps d'avoir l'effet d'explosions 
    }

    void TimeSkip() 
    {
        GO = true;//dis au cube de venir
        GDDP.Using_Tele = false;//dis au systeme que je ne ramene plus des petis cube
        
    }

    public void changeLayerBack() 
    {
        this.gameObject.layer = 0;
    }
   
}
