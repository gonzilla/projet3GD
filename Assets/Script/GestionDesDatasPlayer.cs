using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionDesDatasPlayer : MonoBehaviour
{
    //Script pour modifier des variables communes (pour script sur le joueur)
    //Public variable

    public GestionDesFormes GDF;
    public DeplacementJ DJ;
    public GestionDuPoids GDP;



    [Header("Telekinesys")]
    public float SpeedTelekinesy;
    public float TempsAvantDebutTelekinesy;
    public float DistanceMinTelekinesy;
    public Transform Cible;
    public bool Using_Tele;
    

    

    [Header("Déplacement")]
    public float SpeedRoller;
    public float DistanceCameraPoint;
    public float ForceDash;
    public float NombreDeGrosCubeParDash;
    [HideInInspector] public bool LanceUnAutreDepl;


    [Header("InputSystem")]
    [HideInInspector] public bool Vise=false;
    [HideInInspector] public float tauxMaintien;

    
    
}
