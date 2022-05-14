using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform LINK_CameraX;
    [SerializeField] private Transform LINK_Camera;
    [SerializeField] private GameObject LINK_Player;

    [SerializeField] private float SET_CameraMinAngle;
    [SerializeField] private float SET_CameraMaxAngle;
    [SerializeField] private float SET_CameraVerticalSens;
    [SerializeField] private float SET_CameraHorizontalSens;

    [SerializeField] private float SET_CameraRange;
    [SerializeField] private float SET_CameraRangeAim;


    private float TECH_CameraRangeActual;
    private float TECH_CameraActualAngle;
    private bool isButtonControlDawn;

    private float TECH_Position_X;
    private float TECH_Position_Y;
    private float TECH_Actual_Angle_X;
    private float TECH_Actual_Angle_Y;

    private RaycastHit TECH_Hit;

    void Start()
    {
        TECH_CameraRangeActual = SET_CameraRange;
    }

    void LateUpdate()
    {
        CameraControl();
    }

    void CameraControl()
    {

        transform.position = new Vector3(LINK_Player.transform.position.x, LINK_Player.transform.position.y + 0.7f, LINK_Player.transform.position.z);
        //Range
        TECH_CameraRangeActual = Mathf.Clamp(TECH_CameraRangeActual, SET_CameraRangeAim, SET_CameraRange);

        //Raycast
        if (Physics.Raycast(transform.position, LINK_Camera.transform.position - transform.position, out TECH_Hit, TECH_CameraRangeActual))
        {

            LINK_Camera.transform.position = transform.position + (TECH_Hit.point - transform.position) * 0.9f;
        }
        else
        {
            LINK_Camera.transform.localPosition = new Vector3(0, 0, -TECH_CameraRangeActual);
        }

        if (!isButtonControlDawn)
        {
            if (/*Input.GetKey(KeyCode.Mouse0)*/ Input.GetButton("Fire1"))
            {
                Vector3 actual_mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                TECH_Actual_Angle_X = -(TECH_Position_X - actual_mousePosition.x) / Screen.width;
                TECH_Actual_Angle_Y = -(TECH_Position_Y - actual_mousePosition.y) / Screen.height;
            }
            else
            {
                TECH_Actual_Angle_X = 0;
                TECH_Actual_Angle_Y = 0;
            }

            //Horizontal
            transform.Rotate(0, TECH_Actual_Angle_X * SET_CameraHorizontalSens, 0);

            //Vertical
            TECH_CameraActualAngle = Mathf.Clamp(TECH_CameraActualAngle -= TECH_Actual_Angle_Y * SET_CameraVerticalSens, SET_CameraMinAngle, SET_CameraMaxAngle);
            LINK_CameraX.localEulerAngles = new Vector3(TECH_CameraActualAngle, 0, 0);

            TECH_Position_X = Input.mousePosition.x;
            TECH_Position_Y = Input.mousePosition.y;
        }
       

    }

    public void ButtonControlDown()
    {
        isButtonControlDawn = true;
    }

    public void ButtonControlUp()
    {
        isButtonControlDawn = false;
    }



}
