using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float minMoveSpped;
    [SerializeField] private float maxMoveSpped;
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject controllButton;
    [SerializeField] private GameObject LINK_Camera;
    private Camera _camera;
    private CharacterController charPlayer;
    private bool isMouseDawn;

    [SerializeField] private float SET_Gravity;
    [SerializeField] private float SET_JumpSpeed;
    private float TECH_VerticalSpeed;
    private RaycastHit TECH_Hit;

    void Start()
    {
        charPlayer = GetComponent<CharacterController>();
    }


    void Update()
    {
        PlayerControl();
        Jump();
    }



    void PlayerControl()
    {
        if (isMouseDawn == true)
        {
            Vector3 mousePosition = Input.mousePosition - controllButton.transform.position;          
            float magnitudePos = mousePosition.magnitude;   
            magnitudePos = Mathf.Clamp(magnitudePos, 0, 130);
            cursor.transform.position = (mousePosition.normalized * magnitudePos) + controllButton.transform.position;
            float moveSpped = ((magnitudePos / 130) * (maxMoveSpped - minMoveSpped)) + minMoveSpped;

            transform.LookAt(new Vector3(mousePosition.normalized.x, transform.position.y, mousePosition.normalized.y) + new Vector3(transform.position.x, 0, transform.position.z));
            transform.Rotate(LINK_Camera.transform.rotation.eulerAngles);
            charPlayer.Move(transform.forward * moveSpped * Time.deltaTime);

        }
        else
        {
            cursor.transform.position = controllButton.transform.position;
        }

    }

    void Jump()
    {
        Physics.Raycast(transform.position, -transform.up, out TECH_Hit, 0.1f, 1, QueryTriggerInteraction.Ignore);

        if (TECH_Hit.collider != null)
        {
            if (Input.GetButtonDown("Jump"))
            {

                TECH_VerticalSpeed = 0;
                TECH_VerticalSpeed += SET_JumpSpeed;
            }
        }

        TECH_VerticalSpeed -= SET_Gravity * Time.deltaTime;
        charPlayer.Move(transform.up * TECH_VerticalSpeed * Time.deltaTime);

    }

    public void ButtonJump()
    {
        TECH_VerticalSpeed = 0;
        TECH_VerticalSpeed += SET_JumpSpeed;
    }


    public void MouseDown()
    {
        isMouseDawn = true;
    }

    public void MouseUp()
    {
        isMouseDawn = false;
    }


}
