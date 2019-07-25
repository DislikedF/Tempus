//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class OLD_SC_Player_Controller_DE : MonoBehaviour {

//    public float Jump_Vertical_Force;
//    public float Jump_Horizontal_Force;
//    public float Ray_Hit_Distance;
//    public float Movement_Speed;

//    public Rigidbody Player_Rigidbody;
//    public SphereCollider Player_Collider;

//    private bool Is_Grounded;
//    private bool IsTrapped = false;
//    private int Input_Count = 0;
//    private int Current_Trap_Level;
//    private float worldRadius;

//    void Start()
//    {
//        // assign components to our objects
//        Player_Rigidbody = GetComponent<Rigidbody>();

//        // messy, but for the player object, transform.localScale.x == .y and == .z anyway - J
//        // converts local space radius to world space for measuring raycasting vs collider's actual size
//        worldRadius = transform.localScale.x * Player_Collider.radius;
//    }

//    void Update()
//    {
//        if (SC_Main_DE.Main_Instance1.Current_Gamestate == SC_Main_DE.Gamestate.PLAYING)
//        {
//            // Update IsGrounded bool per frame
//            Check_Grounded();

//            // Check for input - change to trap-event-success or trap-event-fail once implemented
//            Check_Input();
//            Check_Trap_Outcome();
//            Move_Right();
//            Update_Score();
//        }
//    }

//    void Check_Trap_Outcome()
//    {
//        // replace with trap end flag - add Fail_Response
//        if (IsTrapped == true && Input_Count >= Current_Trap_Level)
//        {
//            Success_Response();
//        }
//    }

//    void Success_Response()
//    {
//            // Break out of trap
//            Jump_Out();
//            // Wait & animate during jump-out event  
//            // yield return new WaitUntil(() => Is_Grounded == true);
//    }

//    // Called when entering trap collision box 
//    public void Get_Trapped(int Trap_Count)
//    {
//        IsTrapped = true;
//        Current_Trap_Level = Trap_Count;
//    }

//    // Called when leaving trap collision box
//    public void Get_Free()
//    {
//        IsTrapped = false;
//        Input_Count = 0;
//        Current_Trap_Level = 0;
//    }

//    private void Check_Input()
//    {
//        if (IsTrapped == true)
//        {
//            if (Input.touchCount > 0)
//            {
//                Touch touch = Input.GetTouch(0);
//                if (touch.phase == TouchPhase.Began)
//                {
//                    Input_Count += 1;
//                }
//            }
//            else if (Input.GetMouseButtonUp(0))
//            {
//                Input_Count += 1;
//            }
//        }
//    }

//    private bool Check_Grounded()
//    {
//        if (Is_Grounded)
//        {
//            Ray_Hit_Distance = 0.7f * (Player_Collider.radius * worldRadius);
//        }
//        else
//        {
//            Ray_Hit_Distance = 0.3f * (Player_Collider.radius * worldRadius);
//        }


//        if (Physics.Raycast (transform.position - new Vector3(0, (0.9f * worldRadius), 0), -transform.up, Ray_Hit_Distance))
//        {
//            Is_Grounded = true;
//            return true;
//        }
//        else
//        {
//            Is_Grounded = false;
//            return false;
//        }
//    }


//    void Jump_Out()
//    {
//        if (IsTrapped == true)
//        {
//            // Really inconsistent due to a = F/m -- no direct relation to speed, 
//            // worse when it's few frames of acceleration only
//            //Player_Rigidbody.AddForce(new Vector3(Jump_Horizontal_Force, Jump_Vertical_Force, 0));

//            // this is better
//            // but something still seems to reduce horizontal speed mid-air halfway between apex and landing
//            // Move_Right() only affects velocity on the ground, so idk what's doing it yet - J

//            // on second check it might just be unnatural-looking motion 
//            // with it instantly moving when hitting the ground
//            // short landing time might make it look better.
//            Player_Rigidbody.velocity = new Vector3(Jump_Horizontal_Force, Jump_Vertical_Force, 0);
//        }
//    }

//    void Move_Right()
//    {
//        if (IsTrapped == false && Is_Grounded == true)
//        {
//            Vector3 Move_Vect = new Vector3(1, 0, 0) * Movement_Speed * Time.deltaTime;
//            Player_Rigidbody.MovePosition(transform.position + Move_Vect);
//        }
//    }

//    void Update_Score()
//    {
//        SC_Main_DE.Main_Instance1.score = (int)Player_Rigidbody.position.x;
//    }
//}
