using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private string TECH_MoveType;
    private Vector3 TECH_MoveVector;
    private bool TECH_StartCoroutine;

    private GameObject LinkPlayer;
    private bool ontgriger;

    private enum MoveType
    {
        Axis_X,
        Axis_Y,
        Axis_Z
    }

    [SerializeField] private MoveType moveType;
    [SerializeField] private float STATS_MoveSpeed;
    [SerializeField] private float STATS_FromPosition; //проще с 0 не уверен что этот параметр воощбе нужен
    [SerializeField] private float STATS_ToPosition;
    [SerializeField] private float STATS_DelayInPosition;


    void Start()
    {
        WriteType();
    }

    void WriteType()
    {
        switch (moveType)
        {
            case MoveType.Axis_X:
                TECH_MoveType = "Axis_X";
                TECH_MoveVector = Vector3.right;
                STATS_FromPosition = transform.position.x + STATS_FromPosition -0.01f;
                STATS_ToPosition = transform.position.x + STATS_ToPosition;
                break;
            case MoveType.Axis_Y:
                TECH_MoveType = "Axis_Y";
                TECH_MoveVector = Vector3.up;
                STATS_FromPosition = transform.position.y + STATS_FromPosition -0.01f;
                STATS_ToPosition = transform.position.y + STATS_ToPosition;

                break;
            case MoveType.Axis_Z:
                TECH_MoveType = "Axis_Z";
                TECH_MoveVector = Vector3.forward;
                STATS_FromPosition = transform.position.z + STATS_FromPosition -0.01f;
                STATS_ToPosition = transform.position.z + STATS_ToPosition;
                break;
        }
    }

    void Update()
    {
        MoveControl();   
    }


    void MoveControl()
    {
        if (TECH_MoveType == "Axis_X")
        {
            if (transform.position.x >= STATS_ToPosition || transform.position.x <= STATS_FromPosition)
            {
                if (TECH_StartCoroutine == false)
                {
                    StartCoroutine(MoveDelay());
                }
            }
        }

        if (TECH_MoveType == "Axis_Y")
        {
            if (transform.position.y >= STATS_ToPosition || transform.position.y <= STATS_FromPosition)
            {
                if (TECH_StartCoroutine == false)
                {
                    StartCoroutine(MoveDelay());
                }
            }
        }

        if (TECH_MoveType == "Axis_Z")
        {
            if (transform.position.z >= STATS_ToPosition || transform.position.z <= STATS_FromPosition)
            {
                if (TECH_StartCoroutine == false)
                {
                    StartCoroutine(MoveDelay());
                }
            }
        }

        transform.Translate(TECH_MoveVector * STATS_MoveSpeed * Time.deltaTime);
    }

    IEnumerator MoveDelay()
    {
        TECH_StartCoroutine = true;
        float actualSpeed = STATS_MoveSpeed;
        STATS_MoveSpeed = 0;
        yield return new WaitForSeconds(STATS_DelayInPosition);
        STATS_MoveSpeed = -actualSpeed;
        yield return new WaitForSeconds(0.5f);
        TECH_StartCoroutine = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().MovementWithAnObject(TECH_MoveVector, STATS_MoveSpeed);
        }
    
    }
  


}
