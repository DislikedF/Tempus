//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;



//public class OLD_SC_Main_DE : MonoBehaviour {

//    // Singleton of the main game controller
//    private static SC_Main_DE Main_Instance;

//    // and have a way of accessing it in every script
//    public static SC_Main_DE Main_Instance1
//    {
//        get
//        {
//            if (Main_Instance == null)
//            {
//                Main_Instance = GameObject.FindObjectOfType<SC_Main_DE>();
//            }
//            return OLD_SC_Main_DE.Main_Instance;
//        }
//    }

//    // Setup state machine along the same lines - J
//    public enum Gamestate { TAP_TO_BEGIN, PLAYING, PAUSED, GAME_OVER, };

//    private Gamestate game_state;

//    // for fetching outside of SC_MAIN_DE - J
//    public Gamestate Current_Gamestate
//    {
//        get { return game_state; }
//        set { game_state = value; }
//    }


//    public SC_Timer_DE Timer;
//    public Text Centre_Text;
//    public Text Score_Text;
//    public float trap_time_limit;
//    public float trap_lower_limit;
//    public float trap_time_step;
//    public int score;


//    // Use this for initialization
//    void Start ()
//    {

//        //// Disable timer
//        //Timer = GetComponent<SC_Timer_DE>();
//        //Timer.enabled = !Timer.enabled;

//        game_state = Gamestate.TAP_TO_BEGIN;
//        score = 0;
//        //trap_time_limit = 10.0f;
//        //trap_lower_limit = 1.0f;

//    }

//    // Update is called once per frame
//    void Update () {

//        switch (game_state)
//        {

//            case (Gamestate.TAP_TO_BEGIN):
//                Centre_Text.text = "Tap to begin";
//                Score_Text.text = "";
//                Check_Input();
//                break;
//            case (Gamestate.PLAYING):
//                Score_Text.text = "Score : " + score;
//                Centre_Text.text = "";
//                Check_Input();
//                break;
//            case (Gamestate.PAUSED):
//                Centre_Text.text = "Game Paused";
//                Check_Input();
//                break;
//            case (Gamestate.GAME_OVER):
//                Centre_Text.text = "Game Over! \n Return to menu?";
//                Time.timeScale = 0;
//                Check_Input();
//                break;

//        }
//    }



//    private void Check_Input()
//    {
//        switch (game_state)
//        {
//            case (Gamestate.TAP_TO_BEGIN):
//                // if on Tap to Begin.. uh.. begin
//                if (Input.touchCount > 0)
//                {
//                    Touch touch = Input.GetTouch(0);
//                    if (touch.phase == TouchPhase.Began)
//                    {
//                        Time.timeScale = 1;
//                        game_state = Gamestate.PLAYING;
//                    }
//                }
//                else
//                if (Input.GetMouseButtonDown(0) == true)
//                {
//                    Time.timeScale = 1;
//                    game_state = Gamestate.PLAYING;
//                }

//                break;
//            case (Gamestate.PLAYING):
//                // Pause/unpause if space is pressed (pc only until we put a button in) - J
//                if (Input.GetKeyDown(KeyCode.Space) == true)
//                {
//                    Pause_Toggle();
//                }
//                break;
//            case (Gamestate.PAUSED):
//                // Pause/unpause if space is pressed (pc only until we put a button in) - J
//                if (Input.GetKeyDown(KeyCode.Space) == true)
//                {
//                    Pause_Toggle();
//                }
//                break;
//            case (Gamestate.GAME_OVER):
//                if (Input.touchCount > 0)
//                {
//                    Touch touch = Input.GetTouch(0);
//                    if (touch.phase == TouchPhase.Began)
//                    {
//                        Time.timeScale = 1;
//                        SceneManager.LoadScene("MetaGame");
//                    }
//                }
//                else if (Input.GetMouseButtonDown(0) == true)
//                {
//                    Time.timeScale = 1;
//                    SceneManager.LoadScene("MetaGame");

//                }
//                break;
//        }
//    }


//    public void Pause_Toggle()
//    {
//        // Toggle pause by setting gamestate and timescale (timescale required to stop unity's functions)
//        if (game_state == Gamestate.PAUSED)
//        {
//            game_state = Gamestate.PLAYING;
//            Time.timeScale = 1;
//        }
//        else if (game_state == Gamestate.PLAYING)
//        {
//            game_state = Gamestate.PAUSED;
//            Time.timeScale = 0;
//        }
//    }
//}


