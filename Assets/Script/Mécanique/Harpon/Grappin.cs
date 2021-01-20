using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappin : MonoBehaviour
{
    //Public variable
   
    public bool Activate;
    public float speedChangementLongueur;
    
    //Local variable
    Rigidbody CibleRB;
    ConfigurableJoint CJoint;
    int nom = 0;
    GameObject Cible;
    //LineRenderer LR_MaLigne;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void CreationGrappinPlayerToObject(RaycastHit Info)//(RaycastHit Info) 
    {
        Activate = true;
        Cible = Info.transform.gameObject;
        if (!Cible.GetComponent<Rigidbody>())
        {
            CibleRB = Cible.AddComponent<Rigidbody>();
            if (Cible.CompareTag("Mur")|| Cible.CompareTag("Inmovable"))
            {
                CibleRB.constraints = RigidbodyConstraints.FreezeAll;
            }
            
        }

        CJoint = this.gameObject.AddComponent<ConfigurableJoint>();//add le component au joueur
        CJoint.connectedBody = Cible.GetComponent<Rigidbody>();//vas chercher le ridbody

        //anchor
        CJoint.autoConfigureConnectedAnchor = false;//empecher de tout faire lui même pour que je puis modifier moi-même
        CJoint.connectedAnchor = Vector3.zero;// set la position de lanchor // a changer là ou raycast.point
        //Motion
        CJoint.xMotion = ConfigurableJointMotion.Limited;//pour limiter le truc et faire reglages
        CJoint.yMotion = ConfigurableJointMotion.Limited;//pour limiter le truc et faire reglages
        CJoint.zMotion = ConfigurableJointMotion.Limited;//pour limiter le truc et faire reglages
        //Linear
        SoftJointLimit LinearValue = CJoint.linearLimit; //First we get a copy of the limit we want to change
        LinearValue.limit = Vector3.Distance(transform.position, Cible.transform.position);//met sur la distance entre deux vecteur
        CJoint.linearLimit = LinearValue;//changement de la valeur
        //Spring
        SoftJointLimitSpring SpringValue = CJoint.linearLimitSpring;//comme pour linear 
        SpringValue.spring = 50f;//change la veleur du spring
        SpringValue.damper = 10f;// son damper
        CJoint.linearLimitSpring = SpringValue;//set les valeurs

        CJoint.enableCollision = true;//permet aux objets de se toucher
        //Vector3 positionLinerenderOffset = Cible.transform.position;
        nom++;// une fois le joint créer de le refasse plus
        //setLineRenderer(Cible.transform.position);
    }

    public void CreationGrappinObjectToPlayer(RaycastHit Info)//(RaycastHit Info) 
    {
        Activate = true;
        Cible = Info.transform.gameObject;
        if (!Cible.GetComponent<Rigidbody>())
        {
            CibleRB = Cible.AddComponent<Rigidbody>();
            if (Cible.CompareTag("Mur") || Cible.CompareTag("Inmovable"))
            {
                CibleRB.constraints = RigidbodyConstraints.FreezeAll;
            }
        }

        CJoint = Cible.AddComponent<ConfigurableJoint>();
        CJoint.connectedBody = this.gameObject.GetComponent<Rigidbody>();

        //anchor
        CJoint.autoConfigureConnectedAnchor = false;//empecher de tout faire lui même pour que je puis modifier moi-même
        CJoint.connectedAnchor = Vector3.zero;// set la position de lanchor // a changer là ou raycast.point
        //Motion
        CJoint.xMotion = ConfigurableJointMotion.Limited;//pour limiter le truc et faire reglages
        CJoint.yMotion = ConfigurableJointMotion.Limited;//pour limiter le truc et faire reglages
        CJoint.zMotion = ConfigurableJointMotion.Limited;//pour limiter le truc et faire reglages
        //Linear
        SoftJointLimit LinearValue = CJoint.linearLimit; //First we get a copy of the limit we want to change
        LinearValue.limit = Vector3.Distance(transform.position, Cible.transform.position);//met sur la distance entre deux vecteur
        CJoint.linearLimit = LinearValue;//changement de la valeur
        //Spring
        SoftJointLimitSpring SpringValue = CJoint.linearLimitSpring;//comme pour linear 
        SpringValue.spring = 50f;//change la veleur du spring
        SpringValue.damper = 10f;// son damper
        CJoint.linearLimitSpring = SpringValue;//set les valeurs

        CJoint.enableCollision = true;//permet aux objets de se toucher
        //Vector3 positionLinerenderOffset = Cible.transform.position;
        nom++;// une fois le joint créer de le refasse plus
        //setLineRenderer(Cible.transform.position);
    }

    public void ChangementLongueurGrappin(float changement) 
    {
        SoftJointLimit LinearValue = CJoint.linearLimit; //First we get a copy of the limit we want to change
        LinearValue.limit += changement * speedChangementLongueur * Time.deltaTime;
        CJoint.linearLimit = LinearValue;//changement de la valeur

    }
    public void DestruuctionGrappin() 
    {
        nom = 0;
        Activate = false;
        Destroy(CJoint);
    
    }
}
