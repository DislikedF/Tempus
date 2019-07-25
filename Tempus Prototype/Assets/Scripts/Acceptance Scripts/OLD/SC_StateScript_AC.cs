using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_StateScript_AC : MonoBehaviour
{
    //sigleton of the main controller
    private static SC_StateScript_AC StateInstance;

    //Access it in every script
    public static SC_StateScript_AC StateInstance_1
    {
        get
        {
            if(StateInstance == null)
            {
                StateInstance = GameObject.FindObjectOfType<SC_StateScript_AC>();
            }

            return SC_StateScript_AC.StateInstance;
        }
    }

    //Possible states
    public enum Gamestate
    {
        GROUNDED, PLAY, PAUSED, GAME_OVER,
    };

    private Gamestate game_state;

    //Get and set for outside this script
    public Gamestate Current_Gamestate
    {
        get { return game_state; }
        set { game_state = value; }
    }

    public GameObject EndScreen;
    public GameObject UI;
    public GameObject PauseScreenUI;

	// Use this for initialization
	void Start ()
    {
        if (Time.timeScale != 1)        //ensure timescale is correct
        {
            Time.timeScale = 1;
        }

        game_state = Gamestate.GROUNDED;
    }
	
	// Update is called once per frame
	void Update ()
    {
		switch (game_state)
        {
            case (Gamestate.GROUNDED):
                //User Taps to take-off
                break;

            case (Gamestate.PLAY):

                break;

            case (Gamestate.PAUSED):

                break;

            case (Gamestate.GAME_OVER):
                UI.SetActive(false);
                PauseScreenUI.SetActive(false);
                EndScreen.SetActive(true);
                if(Time.timeScale != 0)
                {
                    Time.timeScale = 0;
                }
                break;
         
        }
	}

    private void Check_Input()
    {
        switch (game_state)
        {
            case (Gamestate.GROUNDED):      //gets user input and plays game
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        game_state = Gamestate.PLAY;
                    }
                }
                break;

            case (Gamestate.PLAY):
                if (Input.touchCount > 0)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.name == "pauseButton")
                    {
                        game_state = Gamestate.PAUSED;
                    }
                }
                break;

            case (Gamestate.PAUSED):
                if(Time.timeScale == 1)
                {
                    game_state = Gamestate.PLAY;
                }
                break;

            case (Gamestate.GAME_OVER):
               
                break;
        }
    }
}
