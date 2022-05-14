using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float STATS_minMoveSpped;
    [SerializeField] private float STATS_maxMoveSpped;
    [SerializeField] private GameObject LINK_Cursor;
    [SerializeField] private GameObject LINK_ControllButton;
    [SerializeField] private GameObject LINK_Camera;
    private CharacterController charPlayer;
    private bool isMouseDawn;
    [SerializeField] private Animator FoxAnomation;
    [SerializeField] private float SET_Gravity;
    [SerializeField] private float SET_JumpSpeed;
    [SerializeField] private bool isGround;
    private float TECH_VerticalSpeed;
    private RaycastHit TECH_Hit;
    private float TECH_BaseDownMovement = 2;
    private float TECH_JumpSurfaceAngle = 45;
    private float TECH_MoveVertical;
    private float TECH_MoveSpped;
    private Vector3 TECH_MoveVector;
    [SerializeField] private bool TECH_DoubleJump;

    void Start()
    {
        charPlayer = GetComponent<CharacterController>();
        TECH_DoubleJump = false;
    }


    void Update()
    {
        PlayerControl();
  

    }




    void PlayerControl()
    {
        //Move
        if (isMouseDawn == true)
        {
            Vector3 mousePosition = Input.mousePosition - LINK_ControllButton.transform.position;
            float magnitudePos = mousePosition.magnitude;
            magnitudePos = Mathf.Clamp(magnitudePos, 0, 130);
            LINK_Cursor.transform.position = (mousePosition.normalized * magnitudePos) + LINK_ControllButton.transform.position;
            float moveSpped = ((magnitudePos / 130) * (STATS_maxMoveSpped - STATS_minMoveSpped)) + STATS_minMoveSpped;

            transform.LookAt(new Vector3(mousePosition.normalized.x, transform.position.y, mousePosition.normalized.y) + new Vector3(transform.position.x, 0, transform.position.z));
            transform.Rotate(LINK_Camera.transform.rotation.eulerAngles);
            TECH_MoveSpped = moveSpped;
            FoxAnomation.SetBool("isRun", true);
        }
        else
        {
            LINK_Cursor.transform.position = LINK_ControllButton.transform.position;
            FoxAnomation.SetBool("isRun", false);
            TECH_MoveSpped = 0;
        }

        //Gravity & Jump

        if (Input.GetButtonDown("Jump"))
        {
            ButtonJump();
        }


        if (TECH_MoveVertical < 0)
        {

            FoxAnomation.SetTrigger("Fall");
        }

        if (Physics.SphereCast(new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 2, transform.position.z), 0.5f, Vector3.down, out TECH_Hit, 0.3f))
        {
            FoxAnomation.SetBool("isGround", true);
            isGround = true;

            if (TECH_MoveVertical < -TECH_BaseDownMovement)
            {
                TECH_MoveVertical = -TECH_BaseDownMovement;
            }
        }
        else
        {
            isGround = false;
            FoxAnomation.SetBool("isGround", false);

        }
        TECH_MoveVertical -= SET_Gravity * Time.deltaTime;

        TECH_MoveVector = new Vector3(0, TECH_MoveVertical, 0) + (transform.forward * TECH_MoveSpped);
        charPlayer.Move(TECH_MoveVector * Time.deltaTime);
    }


    public void ButtonJump()
    {
        if (TECH_DoubleJump == true && isGround == false)
        {
            TECH_MoveVertical = 0;
            TECH_MoveVertical += SET_JumpSpeed;
            FoxAnomation.SetTrigger("DubleJump");
            TECH_DoubleJump = false;
        }

        if (Physics.Raycast(transform.position, Vector3.down, 0.05f + TECH_SurfaceAngleToRangeCast(TECH_JumpSurfaceAngle)))
        {
            if (isGround == true)
            {
                TECH_MoveVertical = 0;
                TECH_MoveVertical += SET_JumpSpeed;
                FoxAnomation.SetTrigger("Jump");
                TECH_DoubleJump = true;
            }        
        }
    }


    float TECH_SurfaceAngleToRangeCast(float Angle)
    {
        return 0.5f / Mathf.Cos(Angle * Mathf.PI / 180f) - 0.5f;
    }


    public void MouseDown()
    {
        isMouseDawn = true;
    }

    public void MouseUp()
    {
        isMouseDawn = false;
    }

    public void MovementWithAnObject(string axis , float speed, Transform TR)
    {
        if (axis == "Axis_X")
        {
            charPlayer.Move(TR.right * speed * Time.deltaTime);
        }

        if (axis == "Axis_Y")
        {
            charPlayer.Move(TR.up * speed * Time.deltaTime);
        }
        if (axis == "Axis_Z")
        {
            charPlayer.Move(TR.forward * speed * Time.deltaTime);
        }
    }


}
