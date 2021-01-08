using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionDesDatasPlayer : MonoBehaviour
{
    //Script pour modifier des variables communes (pour script sur le joueur)
    //Public variable
    [Header("Telekinesys")]
    public float SpeedTelekinesy;
    public float TempsAvantDebutTelekinesy;
    public float DistanceMinTelekinesy;
    public Transform Cible;
    public bool Using_Tele;
    

    [Header("Grappin")]
    public float DistanceMaxGrappin;

    //[Header("Autre")]
    //public bool InAir;

    [Header("Déplacement")]
    public float SpeedRoller;
    public float DistanceCaméraPoint;
    public float ForceDash;

    [Header("InputSystem")]
    [HideInInspector] public bool Vise=false;
    //Local variable

    
}
