using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Collision_Trigger_DE : MonoBehaviour {

    SC_Player_Controller_DE Player;

    // This was difficult to get to work, if anyone can tell me a better way that'd be great - J
    //public GameObject Timer;
    //private SC_Timer_DE Timer_Code; 

    //public int trap_layers;
    //public float trap_time_limit;
    //public bool trap_escaped = false;

    private void Start()
    {
        //Timer = GameObject.Find("Timer");
        //Timer_Code = Timer.GetComponent<SC_Timer_DE>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Handheld.Vibrate();
        Handheld.Vibrate();
        if (other.gameObject.name == "Player_Character_DE")
        {
            Player = other.gameObject.GetComponent<SC_Player_Controller_DE>();

            // Ends game on contact
            if (SC_Main_DE.Main_Instance1.Current_Gamestate == SC_Main_DE.Gamestate.PLAYING)
            {
                if (Player.Invincibility == false)
                {
                    SC_Main_DE.Main_Instance1.Current_Gamestate = SC_Main_DE.Gamestate.GAME_OVER;

                }
            }
            //}

            //Player = other.gameObject.GetComponent<SC_Player_Controller_DE>();

            //if (trap_escaped == false)
            //{
            //    Player.Get_Trapped(trap_layers);    // Let the player know it's trapped - J
            //    trap_time_limit = SC_Main_DE.Main_Instance1.trap_time_limit;
            //    Timer_Code.Begin_Countdown(trap_time_limit);
            //}
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.name == "Player_Character_DE")
        //{
        //    Player = other.gameObject.GetComponent<SC_Player_Controller_DE>();

        //    Player.Get_Free();      // Let the player know it's free - J
        //    trap_escaped = true;    // Flag this trap as completed so as not to repeatedly trap player - J

        //    Timer_Code.Stop_Countdown();
        //    if (SC_Main_DE.Main_Instance1.trap_time_limit > SC_Main_DE.Main_Instance1.trap_lower_limit)
        //    {
        //        SC_Main_DE.Main_Instance1.trap_time_limit -= SC_Main_DE.Main_Instance1.trap_time_step;
        //    }
        //    else if (SC_Main_DE.Main_Instance1.trap_time_limit <= SC_Main_DE.Main_Instance1.trap_lower_limit)
        //    {
        //        SC_Main_DE.Main_Instance1.trap_time_limit = SC_Main_DE.Main_Instance1.trap_lower_limit;
        //    }
        //}
    }
}
