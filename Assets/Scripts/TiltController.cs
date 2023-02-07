using UnityEngine;
using System.Collections;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TiltController : MonoBehaviour
{
    //drag drop the Joystick child in the Inspector to animate
    // the joystick when moved
    public Transform Joystick;

    //this refers to the vive's touch pad or oculus's joystick
    public SteamVR_Action_Vector2 moveAction = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("platformer", "Move");

    //multiplier for rotation smooth
    private float smooth = 5.0f;

    //multiplier for tilt limit of plane
    float tiltAngle = 10.0f;

    //the original scene was on a different scale, so we've modified the multipler
    public float joyMove = 0.01f;

    //Interactable script of this GameObject
    private Interactable interactable;

    //game plane's Rigidbody
    private Rigidbody tiltingBoardRb;

    private void Start()
    {
        //get the Interactable script on this GameObject (the controller)
        interactable = GetComponent<Interactable>();

        //get the ball's Rigidbody so we can add force to it
        tiltingBoardRb = GameObject.Find("TiltingBoard").GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 movement = Vector2.zero;
        //if the controller is attached to the hand...
        
        if (interactable.attachedToHand)
        {
            //get the hand's type, LeftHand or RightHand so that the controller can be used in either hand
            SteamVR_Input_Sources hand = interactable.attachedToHand.handType;
            //get the touch pad/joystick x/y coordniates of that particular hand
            Vector2 m = moveAction[hand].axis;
            movement = new Vector3((m.x), 0, (m.y));
            Quaternion target = Quaternion.Euler((m.x * tiltAngle), 0, (m.y * tiltAngle));
            tiltingBoardRb.rotation = Quaternion.Slerp(tiltingBoardRb.rotation, target, Time.deltaTime * smooth);
        }

        Joystick.localPosition = movement * joyMove;


        // The movement of the ball is done relative to the controller.  
        // To do this, we get the angle with respect to the y-axis (vertical
        // in world space)

        /**
        float rot = transform.eulerAngles.y;
        movement = Quaternion.AngleAxis(rot, Vector3.up) * movement;
        */


    }
}