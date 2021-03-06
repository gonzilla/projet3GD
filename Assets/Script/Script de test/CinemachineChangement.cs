﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineChangement : PersonnalMethod
{
    //Public variable
    /*public Transform Player;
    public Transform Camera;
    public float AngleLimiteDeCam;
    public float LimAngleNeg = 0;
    public float LimAnglePos = 0;
    public float[] ValueRangeMoving;
    public float[] ValueRangeNotMoving;
    public bool IsWrap;*/
    public float ValueStartX;
    public float ValueStartY;
    //Local variable
    CinemachineFreeLook CFL;
    bool recentreX = false;
    bool recentreY = false;
    GestionDataNonJoueur GDNJ;
    GestionDesDatasPlayer GDDP;

    void start() 
    {
        GoFindDataNonJoueur(out GDNJ);
        GoFindDataPlayer(out GDDP);
    
    }

    /*private void Update()
    {
        if (recentreY)
        {

            CFL.m_YAxis.Value = Quaternion.Lerp(Quaternion.Euler(0, CFL.m_XAxis.Value, 0),
                Quaternion.Euler(0, ValueStartX, 0), GDNJ.VitesseRetourX * Time.deltaTime).eulerAngles.y;

            if (Mathf.Abs(CFL.m_XAxis.Value - ValueStartX) < 0.1f)
            {

                recentreX = false;
            }
        }
        if (recentreX)
        {
            CFL.m_XAxis.Value =Quaternion.Lerp(Quaternion.Euler(CFL.m_YAxis.Value, 0 , 0),
                Quaternion.Euler(ValueStartY, 0 , 0), GDNJ.VitesseRetourY * Time.deltaTime).eulerAngles.x;

            if (Mathf.Abs(CFL.m_YAxis.Value - ValueStartY) < 0.1f)
            {

                recentreY = false;
            }
            
        }
    }*/

    void Start()
    {
        CFL = GetComponent<CinemachineFreeLook>();
        //LimAngleNeg = -AngleLimiteDeCam / 2;
        //LimAnglePos = AngleLimiteDeCam / 2;
        
    }

    
    public void Viser() //quand je vise
    {
        CFL.m_YAxis.m_InputAxisName = "Mouse Y";
        CFL.m_XAxis.m_InputAxisName = "Mouse X";
        recentreX = false;
        recentreY = false;

    }
    public void StopViser() 
    {
        Invoke("goog", 0.5f);
    }

    void goog() 
    {
        CFL.m_YAxis.m_InputAxisName = "";
        CFL.m_XAxis.m_InputAxisName = "";
        CFL.m_XAxis.Value = ValueStartX;
        CFL.m_YAxis.Value = ValueStartY;
    }



    /*public void Mooving() //quand je bouge
    {
        CFL.m_XAxis.m_Wrap = false;//wrap false
        CFL.m_XAxis.m_MinValue = ValueRangeMoving[0];// min value est égale à la min du tab1
        CFL.m_XAxis.m_MaxValue = ValueRangeMoving[1];// la meme avec le max
        float angleActuelle = Vector3.Angle(Player.TransformDirection(Vector3.forward), Vector3.forward); //calcul l'angle
        float angleJoueur = Player.eulerAngles.y; //angle du joueur
        float sens = 1;//décide du sens
        //LimAngleNeg = -AngleLimiteDeCam / 2 - angleJoueur;
        //LimAnglePos = AngleLimiteDeCam / 2 - angleJoueur;
        if (angleJoueur > 180f)
        {
            sens = 1;
            //print("negatif");
        }
        else
        {
            sens = -1;
        }



        LimAnglePos =  angleJoueur - 180 + (180 * (-1  *sens)) - AngleLimiteDeCam / 2;   //180 * sens + 180 + angleJoueur - AngleLimiteDeCam / 2;
        LimAngleNeg =  angleJoueur - 180 + (180 * (-1 * sens)) + AngleLimiteDeCam / 2;

        Debug.DrawRay(Player.position, new Vector3(Mathf.Sin(Mathf.Deg2Rad * LimAngleNeg), 0, -Mathf.Cos((Mathf.Deg2Rad * LimAngleNeg))).normalized * 10, Color.blue);//Mathf.Cos(Mathf.Deg2Rad * LimAngleNeg
        Debug.DrawRay(Player.position, new Vector3(Mathf.Sin(Mathf.Deg2Rad * LimAnglePos), 0, -Mathf.Cos(Mathf.Deg2Rad * LimAnglePos)).normalized * 10, Color.red);
        /*if (CFL.m_XAxis.Value < LimAngleNeg + (angleActuelle * -sens))
        {
            CFL.m_XAxis.Value = LimAngleNeg + (angleActuelle * -sens);

        }
        if (CFL.m_XAxis.Value > LimAnglePos + (angleActuelle * -sens))
        {

            CFL.m_XAxis.Value = LimAnglePos + (angleActuelle * -sens);

        }

    }*/

    /*public void Limitation(bool Moving) //quand je reste imobile
    {
       
        if (Moving)// si je bouge
        {
            CFL.m_XAxis.m_Wrap = false;//wrap false
            CFL.m_XAxis.m_MinValue = ValueRangeMoving[0];// min value est égale à la min du tab1
            CFL.m_XAxis.m_MaxValue = ValueRangeMoving[1];// la meme avec le max
            float angleActuelle = Vector3.Angle(Player.TransformDirection(Vector3.forward), Vector3.forward);
            float angleJoueur = Player.eulerAngles.y;
            float sens = 1;

            if (angleJoueur > 180f)
            {
                sens = 1;
            }
            else
            {
                sens = -1;
            }
            if (CFL.m_XAxis.Value < LimAngleNeg + (angleActuelle * -sens))
            {
                CFL.m_XAxis.Value = LimAngleNeg + (angleActuelle * -sens);

            }
            if (CFL.m_XAxis.Value > LimAnglePos + (angleActuelle * -sens))
            {

                CFL.m_XAxis.Value = LimAnglePos + (angleActuelle * -sens);

            }
        }
        else 
        {
            CFL.m_XAxis.m_MinValue = ValueRangeNotMoving[0];
            CFL.m_XAxis.m_MaxValue = ValueRangeNotMoving[1];
            CFL.m_XAxis.m_Wrap = IsWrap;
        }
        
        /*if (angleJoueur>180f)
        {
            angleJoueurPourCAM = angleJoueur - 360f;
        }
        //print(sens);
        
       
        ///CFL.m_XAxis.Value
    }*/
}
//CFL.m_XAxis.m_Wrap = false;
//calcul des valeurs + angles
//float sens = 0;

/* if (angleJoueur<180)
 {
     //print("sens positif"+ angleJoueur);
     sens = 1;
 }
 else 
 {
     //print("sens negatif"+ angleJoueur);
     sens = -1;
 }
 valuePourCam = angleActuelle * sens;*/
/*
    LimAnglePos = angleJoueur+ AngleLimiteDeCam / 2;
        LimAngleNeg = 360- AngleLimiteDeCam / 2 - angleJoueur;
        LimAnglePos = 0 + AngleLimiteDeCam / 2 - angleJoueur;   
    LimAngleNeg = angleJoueur - AngleLimiteDeCam / 2;
        LimAnglePos = angleJoueur + AngleLimiteDeCam / 2;
        if (LimAngleNeg < 0)
        {
            LimAngleNeg =360+ LimAngleNeg;
            //print(LimAngleNeg);
        }
        if (LimAnglePos > 360)
        {
            LimAnglePos = LimAnglePos -  360;
            //print(LimAnglePos);
        }
        if (LimAnglePos<LimAngleNeg)
        {
            if (CFL.m_XAxis.Value > LimAnglePos && CFL.m_XAxis.Value < LimAngleNeg)
            {
                print("trop a");
            }
        }
        else if (LimAngleNeg< LimAnglePos)
        {
            if (CFL.m_XAxis.Value > LimAngleNeg && CFL.m_XAxis.Value < LimAnglePos)
            {
                print("trop b");
            }
        }
        //visualisation angle
        Debug.DrawRay(Player.position, new Vector3(Mathf.Sin(Mathf.Deg2Rad * LimAngleNeg), 0,-Mathf.Cos((Mathf.Deg2Rad * LimAngleNeg) )).normalized * 10,Color.blue);//Quaternion.AngleAxis(LimAngleNeg, Vector3.forward)//Mathf.Cos(Mathf.Deg2Rad * LimAngleNeg
        Debug.DrawRay(Player.position, new Vector3(Mathf.Sin(Mathf.Deg2Rad * LimAnglePos), 0, -Mathf.Cos(Mathf.Deg2Rad * LimAnglePos)).normalized * 10,Color.red);

        //print(valuePourCam);
        //print("angle joueur" + angleJoueur); 
        //print(angleActuelle);
        //décide si wrap

        //ajuste 
        //CFL.m_XAxis.m_Wrap = false;
        */
