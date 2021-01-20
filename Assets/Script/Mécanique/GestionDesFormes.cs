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
        Invoke("lancerDeBoolAfter",0.3f);
    }

    public void LancerDeCertainCube(Vector3 DirectionDeDash, int NombreAlancer) 
    {
        if (MyGDNJ.telekynesysScript.Count>=Mathf.Pow(MyGDNJ.NombreDecube, 3))
        {
            List<int> IndexCeuxQuiSerontExpulser = new List<int>();
            Telekynesys[] Expulser = new Telekynesys[NombreAlancer];
            Rigidbody[] RBExpulser = new Rigidbody[NombreAlancer];
            int Index = 0;
            while (IndexCeuxQuiSerontExpulser.Count < NombreAlancer)
            {
                int toInclude = Random.Range(0, MyGDNJ.telekynesysScript.Count);
                bool isInclude = false;
                foreach (int ind in IndexCeuxQuiSerontExpulser)
                {
                    if (toInclude == ind)
                    {
                        isInclude = true;
                        break;
                    }
                }
                if (!isInclude)
                {
                    IndexCeuxQuiSerontExpulser.Add(toInclude);
                    Expulser[Index] = MyGDNJ.telekynesysScript[toInclude];
                    RBExpulser[Index] = MyGDNJ.MesPetitsCube[toInclude];
                    Index++;
                }
            }
            for (int i = 0; i < IndexCeuxQuiSerontExpulser.Count; i++)
            {
                MyGDNJ.MesPetitsCube.Remove(RBExpulser[i]);
                MyGDNJ.telekynesysScript.Remove(Expulser[i]);
            }

            foreach (Telekynesys item in Expulser)
            {
                item.GO = false;// lui dis d'arreter à la position
            }
            foreach (Rigidbody Cubies in RBExpulser)
            {

                Cubies.AddForce(DirectionDeDash * ForceDeLancer, ForceMode.Impulse);// lui donne une force pour l'expulser
            }
        }
        else 
        {
            LancerDeBoule(DirectionDeDash);

        }

    }
     
    void lancerDeBoolAfter() 
    {
        foreach (Telekynesys item in MyGDNJ.telekynesysScript)//à changer lorsque vrais controle
        {
            item.changeLayerBack();
        }

    }
    
}
