using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class SC_Player_Controller_DE : MonoBehaviour {

    public float Jump_Vertical_Force;
    public float Jump_Horizontal_Force;
    public float Ray_Hit_Distance;
    public float Initial_Movement_Speed;
    public float Acceleration_Factor;
    public float Current_Movement_Speed;

    public Rigidbody Player_Rigidbody;
    public SphereCollider Player_Collider;

    public Animator playerAnim;

    private bool Is_Grounded;
    private bool Is_Jumping;
    private bool IsTrapped = false;
    private int Input_Count = 0;
    private float worldRadius;
    public bool Invincibility;

    Vector3 Move_Vect;

    public float Max_Jump_Height;
    public float Min_Jump_Height;

    public float Max_Jump_Velocity;
    public float Min_Jump_Velocity;
    private float Time_To_Jump_Apex = 0.6f;
    public float Specific_Gravity;

    //private static float Gravity_Reset = -44f;


    // Audio sources
    // Don't you dare ask what happened to audio_jump_1 if you know what's good for you - J

    public GameObject audio_jump_2;
    public GameObject audio_jump_3;
    public GameObject audio_land;
    public GameObject audio_echo_noise_1;
    public GameObject audio_echo_noise_2;
    public GameObject audio_echo_noise_3;



    // Used for animation and sound mostly
    enum PlayerState
    {
        START,
        MOVING,
        JUMPING,
        LANDING,
        CRASHING
    }

    // Instance of player state
    PlayerState playerState;


    void Start()
    {
        // assign components to our objects
        Player_Rigidbody = GetComponent<Rigidbody>();

        // messy, but for the player object, transform.localScale.x == .y and == .z anyway - J
        // converts local space radius to world space for measuring raycasting vs collider's actual size
        worldRadius = transform.localScale.x * Player_Collider.radius;
        Current_Movement_Speed = Initial_Movement_Speed;

        // Calculate our jump velocities based on how high we want to jump, and the gravity setting
        Specific_Gravity = -(2 * Max_Jump_Height) / Mathf.Pow(Time_To_Jump_Apex, 2);

        Min_Jump_Velocity = Mathf.Sqrt(2 * Mathf.Abs(Specific_Gravity) * Min_Jump_Height);
        Max_Jump_Velocity = Mathf.Abs(Specific_Gravity) * Time_To_Jump_Apex;

        playerState = PlayerState.START;

    }

    private void Update()
    {
        Check_Input();
    }

    private void FixedUpdate()
    {
        if (SC_Main_DE.Main_Instance1.Current_Gamestate == SC_Main_DE.Gamestate.PLAYING)
        {
            Move_Right();
            Apply_Specific_Gravity();

            // Update IsGrounded bool per frame
            Check_Grounded();
            Update_Score();

            Current_Movement_Speed += Acceleration_Factor;
            Debug.Log("PlayerState : " + playerState);
            Debug.Log("IsGrounded : " + Is_Grounded);

        }
    }

    private void Check_Input()
    {

        if (Input.touchCount == 1)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Debug.Log("Touched UI");
            }
            else
            {
                //Is_Jumping = true;
                //StartCoroutine(Jump_Coroutine());
                Jump();
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            // Check first to see if a UI element is being pressed
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Touched UI");
            }
            else
            {
                //Is_Jumping = true;
                //StartCoroutine(Jump_Coroutine());
                Jump();
            }
        }
        // If jump button is released
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Cut_Jump();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Cut_Jump();
        }                
    }

    private bool Check_Grounded()
    {

        if (Is_Grounded)
        {
            Ray_Hit_Distance = 0.3f * (Player_Collider.radius * worldRadius);
        }
        else
        {
            Ray_Hit_Distance = 0.1f * (Player_Collider.radius * worldRadius);
        }


        if (Physics.Raycast (transform.position - new Vector3(0, (0.9f * worldRadius), 0), -transform.up, Ray_Hit_Distance))
        {
            Is_Grounded = true;

            // Check if the player was in jumping state, if so, switch state and play landing sound - J
            if (playerState == PlayerState.JUMPING && Player_Rigidbody.velocity.y < 0)
            {
                audio_land.GetComponent<AudioSource>().Play();     
            }

            if (playerState != PlayerState.MOVING)
            {
                playerState = PlayerState.MOVING;
            }

            return true;
        }
        else
        {
            Is_Grounded = false;
            if (playerState == PlayerState.MOVING)
            {
                playerState = PlayerState.JUMPING;
            }
            return false;
        }
    }


    void Jump()
    {
        if (Is_Grounded)   // double check player is on the ground, and jump
        {
            Player_Rigidbody.velocity = new Vector3(Jump_Horizontal_Force, Max_Jump_Velocity, 0);

            // If player isn't already jumping, play a jump sound and change state - J
            if (playerState == PlayerState.MOVING)
            {
                if (Random.Range(0, 100) < 50)
                {
                    audio_jump_2.GetComponent<AudioSource>().Play();
                }
                else
                {
                    audio_jump_3.GetComponent<AudioSource>().Play();
                }

                playerAnim.SetTrigger("Jump");

                playerState = PlayerState.JUMPING;
            }
        }

    }

    void Cut_Jump()
    {
        if (Player_Rigidbody.velocity.y > Min_Jump_Velocity)
        {
            Player_Rigidbody.velocity = new Vector3(Jump_Horizontal_Force, Min_Jump_Velocity, 0);
        }
    }

    void Apply_Specific_Gravity()
    {
        if (!Is_Grounded && Player_Rigidbody.velocity.y <= 0)
        {
            //Move_Vect.y += Specific_Gravity * Time.deltaTime;
            Physics.gravity = new Vector3(0, Specific_Gravity * 1.5f, 0);

            //Player_Rigidbody.velocity = Move_Vect;
        }
        else if (Is_Grounded || Player_Rigidbody.velocity.y > 0)
        {
            Physics.gravity = new Vector3(0, Specific_Gravity / 1.5f, 0);
        }
        //Debug.Log("Gravity = " + Physics.gravity.y);
        //Debug.Log("Specific Gravity = " + Specific_Gravity);
    }

    void Move_Right()
    {

        Move_Vect.x = Current_Movement_Speed * Time.deltaTime;
        Player_Rigidbody.MovePosition(transform.position + Move_Vect);
    }

    void Update_Score()
    {
        SC_Main_DE.Main_Instance1.score = (int)Player_Rigidbody.position.x;
    }

    //IEnumerator Jump_Coroutine() - Didn't work - J
    //{
    //    Player_Rigidbody.velocity = Vector3.zero;
    //    float Jump_Timer = 0;

    //    while(((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0)) && Jump_Timer < Jump_Max_Time)
    //    {

    //        float Jump_Length_Completed = Jump_Timer / Jump_Max_Time;
    //        Vector3 Frame_Jump_Vector = Vector3.Lerp(Vector3.up, Vector3.zero, Jump_Length_Completed);
    //        Player_Rigidbody.AddForce(Frame_Jump_Vector);
    //        Jump_Timer += Time.deltaTime;
    //        yield return null;
    //    }

    //    Is_Jumping = false;
    //}
}
