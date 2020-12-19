using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDivision : PersonnalMethod
{
    //Script qui divise un cube en plusieurs
    //Public variable
    public int LayerCube; // le layer du cube
    public bool Activate; // a enlever pour le moment juste le test
    
    
    //Local variable
    float LengthOriginel; // longueur du cube
    float lengthOfNewCube; // longueur du petit cube
    GestionDataNonJoueur GDNJ;

   

    List<GameObject> CubeExplosions = new List<GameObject>();


    private void Awake()
    {
       
        GoFindDataNonJoueur(out GDNJ);// methode personnel
    }

    void Start()
    {

        LengthOriginel = transform.localScale.x;//GetComponent<MeshRenderer>().bounds.size.x; // vas chercher la taille du cube
        lengthOfNewCube = LengthOriginel / GDNJ.NombreDecube; // la taille du nouveau cube
        
       

    }

    
    void Update()
    {
        if (Activate)
        {
            ActivateDivision();
        }
    }

    public void ActivateDivision() 
    {
       
        
        for (int x = 0; x < GDNJ.NombreDecube; x++) // triple pour ligne/colonne/profondeur
        {
            for (int y = 0; y < GDNJ.NombreDecube; y++)
            {
                for (int z = 0; z < GDNJ.NombreDecube; z++)
                {
                    createMiniCube(positionPetitCube(x,y,z));//créer le cube à la position calculer
                }
            }
        }
        
        Activate = false;//A fini la subdivision
        GenerateExplosions();//fais l'explosion
        Destroy(this.gameObject); // Détruit l'objet
        
    }

    void createMiniCube(Vector3 cordonnate) // fonction créer le cube
    {

        //le parent objet vide

        GameObject CUBE = GameObject.CreatePrimitive(PrimitiveType.Cube);//creer le cube
        
        //CUBE.transform.parent = this.gameObject.transform;
        CUBE.layer = LayerCube;//set le layer du cube
        CubeExplosions.Add(CUBE);//ajout le game object dans la liste
        Telekynesys Tele = CUBE.AddComponent<Telekynesys>();//lui ajoute le scrypte de telekynesis
        GDNJ.telekynesysScript.Add(Tele);// ajoute la ref dans les data
        CUBE.transform.localRotation = Quaternion.identity;//set la rotation // à refaire 
        CUBE.transform.position = cordonnate;//change la position
        CUBE.transform.localScale = Vector3.one * lengthOfNewCube;//met à la bonne taille
        Rigidbody CB = CUBE.AddComponent<Rigidbody>();//add la physique
        Tele.GoToCoordonate();//lui dis d'aller à ces coordonné
        GDNJ.MesPetitsCube.Add(CB);//ajoute le cube dans les datas
        CB.mass = lengthOfNewCube;//met la mass
        
        
    }

    void GenerateExplosions() 
    {
        //print("explosions");
        foreach (GameObject petitCube in CubeExplosions)
        {
            
            Vector3 DirectionDuBloc = -(petitCube.transform.position - transform.position);//calcul la direction du cube
            petitCube.GetComponent<Rigidbody>().AddForce((DirectionDuBloc.normalized) * GDNJ.ForceExplosions,ForceMode.Impulse);//lui met la force
        }
    
    
    }

    Vector3 positionPetitCube(int x,int y,int z) //calcul de la position du petit cube 
    {
        float Fx= transform.localPosition.x - LengthOriginel / 2 + lengthOfNewCube / 2 + lengthOfNewCube * x; //position x
        float Fy= transform.localPosition.y - LengthOriginel / 2 + lengthOfNewCube / 2 + lengthOfNewCube * y; //position y
        float Fz= transform.localPosition.z - LengthOriginel / 2 + lengthOfNewCube / 2 + lengthOfNewCube * z; //position z
        Vector3 final=new Vector3(Fx,Fy,Fz); // position
        return final; //renvois le résultat final
    }

    /*Vector3 FuturePosition() 
    {
        //print("go");
        return CibleTest.position;
    }*/

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, RayonExplosions);
    }*/


    //Temp.transform.rotation = this.gameObject.transform.rotation;
    /*foreach (Transform tr in transform)
    {
        tr.transform.parent = null;
    }*/
    //GameObject Temp = new GameObject("temp Parent");
    //Temp.transform.position = this.gameObject.transform.position;
}
