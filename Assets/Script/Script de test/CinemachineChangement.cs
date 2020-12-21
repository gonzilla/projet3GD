using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineChangement : MonoBehaviour
{
    //Public variable
    public Transform Player;
    public float LimAngleNeg = -90;
    public float LimAnglePos = 90;
    public float[] ValueRangeMoving;
    public float[] ValueRangeNotMoving;
    public bool IsWrap;
    //Local variable
    CinemachineFreeLook CFL;
   


    void Start()
    {
        CFL = GetComponent<CinemachineFreeLook>();
        
    }

    


    public void Limitation(bool Moving) 
    {
        if (Moving)
        {
            CFL.m_XAxis.m_Wrap = false;
            CFL.m_XAxis.m_MinValue = ValueRangeMoving[0];
            CFL.m_XAxis.m_MaxValue = ValueRangeMoving[1];
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
        }*/
        //print(sens);
        
       
        ///CFL.m_XAxis.Value
    }
}
