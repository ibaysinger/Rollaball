using UnityEngine;
using System.Collections;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TiltController : MonoBehaviour
{
    //drag drop the Joystick child in the Inspector to animate
    // the joystick when moved
    public Transform Joystick;

    public Transform Ball;

    //this refers to the vive's touch pad or oculus's joystick
    public SteamVR_Action_Vector2 moveAction = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("platformer", "Move");
    //this refers to a click event on the touch pad/joystick
    public SteamVR_Action_Boolean jumpAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("platformer", "Jump");

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

    private Vector3 ballOriginalPosition;

    //sound that plays when player picks up controller
    public AudioClip GrabbedSound;

    //Audio Source where game sounds are played through
    public AudioSource source;

    //boolean to track if controller hold status changed
    private bool isBeingHeld = false;

    private void Start()
    {
        //get the Interactable script on this GameObject (the controller)
        interactable = GetComponent<Interactable>();

        //get the ball's Rigidbody so we can add force to it
        tiltingBoardRb = GameObject.Find("TiltingBoard").GetComponent<Rigidbody>();

        ballOriginalPosition = Ball.position;
    }

    private void Update()
    {
        Vector3 movement = Vector2.zero;
        bool reset = false;
        //if the controller is attached to the hand...
        
        if (interactable.attachedToHand)
        {
            if(isBeingHeld == false)
            {
                isBeingHeld = true;
                source.clip = GrabbedSound;
                source.Play();
            }
            //get the hand's type, LeftHand or RightHand so that the controller can be used in either hand
            SteamVR_Input_Sources hand = interactable.attachedToHand.handType;
            //get the touch pad/joystick x/y coordniates of that particular hand
            Vector2 m = moveAction[hand].axis;
            movement = new Vector3((m.x), 0, (m.y));
            Quaternion target = Quaternion.Euler((m.x * tiltAngle), 0, (m.y * tiltAngle));
            tiltingBoardRb.rotation = Quaternion.Slerp(tiltingBoardRb.rotation, target, Time.deltaTime * smooth);

            reset = jumpAction[hand].stateDown;
        } else if (isBeingHeld == true)
        {
            isBeingHeld = false;
        }

        Joystick.localPosition = movement * joyMove;

        if (reset)
        {
            //teleports ball back to original point
            Ball.position = ballOriginalPosition;
            
            //kills the existing movement on the ball
            Ball.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        }
    }
}