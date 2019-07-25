using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

//KNOWN BUGS

// - Player Prefs AC_PlanetReached returning a stupid numbe causing alot of space tiles to appear


public class SC_GameScript_AC : MonoBehaviour
{
    //variables passed in from editor

    public Rigidbody playerRigidBod;       //hot air balloon rigidbody
    public ConstantForce playerConstForce;      //constant force on player
    public GameObject player;       //hot air balloon object
    public GameObject planet;
    public GameObject spaceBackground;     //tileable Background
    public GameObject star;         //star object to clone in the space background
    public GameObject endScreen;        //endscreen
    public GameObject pauseScreen;      //pause screen & button
    public GameObject UI;       //UI 
    public ParticleSystem engine;       //flame particle system
    public Animator anim;       //animator for cannister being thrown out
  
    public int fuelCans;        //the starting number of fuel cannisters

    //private Variables

    private float movePace = 2.0f;        //speed balloon moves to planet
    private bool isGrounded = true;        //bool to say if the balloon has taken off
    private bool ended = false;     //bool to say if the game has ended
    private bool isSaved = false;       //start with no save
    private bool fingerOnScreen = false;        //bool to say if there is currently a touch
    private bool Down = false;      //bool to tell if the balloon is going down
    private bool coRoutineActive = false;       //bool to indicate if co-routine is active
    private bool isPaused;

    //UI Variables

    public Text scoreDisplay;       //in-game score
    public Text fuelAvailable;      //in-game fuel can left display
    private int score = 0;      //score always starts at 0

    //Temporary Variables

    public Slider fuelBar;
    private float fuelDecrease = 2.5f;
    private float boostTapType;

    // Use this for initialization
    void Start ()
    {
        if (Time.timeScale != 1)        //set time to normal
        {
            Time.timeScale = 1;
        }

        setUpBackground();      //set up back groud to level distance

        //fuelCans = PlayerPrefs.GetInt("AC_noOfCannisters");     //check if player has bought cannisters (UNCOMMENT WHEN IN GAME MONEY ADDED!)

        engine.Stop();      //have the particle system for the engine turned off to start with
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        //update score after Take off until player stops gaining height
        if (!ended && !isGrounded && Time.timeScale == 1)
        {
            updateScore();
        }

        //update UI (no of cannisters text, score text)
        scoreDisplay.text = score.ToString();
        fuelAvailable.text = fuelCans.ToString();

        //take off 
        if (isGrounded && Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) //&& Input.GetTouch(0).position.y < Screen.height - Screen.height / 6
        {
            fingerOnScreen = true;
            TakeOff();
        }
        else if (!isGrounded)       //in air
        {
            gamePlay();        //main game control function
        }
                  
		//when planet is reached add 1 to PlayerPrefs PlanetReached
	}

    void setUpBackground()      //set up and render size of background depending on how many times the player has reached the planet
    {
        //int tile = PlayerPrefs.GetInt("AC_PlanetReached");            //FIXME 
        int tile = 0;      //TESTING

        Debug.Log(tile);    //BUG

        Quaternion spawnRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        Vector3 currentPos = new Vector3(spaceBackground.transform.position.x, spaceBackground.transform.position.y, spaceBackground.transform.position.z);

        float nextPos = currentPos.y + 8.95f;

        for (int i = 0; i < tile; i++)      //clones the space background tile
        {
            GameObject spaceClone;
            spaceClone = Instantiate(spaceBackground, new Vector3(currentPos.x, nextPos, currentPos.z), spawnRotation);
           // spaceClone.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            spaceClone.gameObject.tag = "Clone";
            spaceClone.SetActive(true);
            nextPos += 8.95f;

            planet.transform.position = new Vector3(planet.transform.position.x, planet.transform.position.y + 8.95f, planet.transform.position.z);
        }

        float starStart = 30;
        float starEnd = nextPos - 8.95f;
        int starCount = (int)nextPos;

        for(int s = 0; s < starCount; s++)      //creates clones of stars upto the same height of the background
        {
            GameObject starClone;
            starClone = Instantiate(star, new Vector3(Random.Range(-10, 10), Random.Range(starStart, starEnd), Random.Range(17, 25)), Quaternion.identity);
            starClone.gameObject.tag = "Clone";
            starClone.SetActive(true);
        }

    }

    void updateScore()      //update the score acorrding to height
    {
        score += 1;     //score incrments by one every update
    }

    void TakeOff()
    {
        if (isGrounded)
        {
            engine.Play();      //play engine particle animation
            isGrounded = false;     //change bool as player is no longer on the ground
        }
        fingerOnScreen = false;
    }

    void gamePlay()        //main game controls
    {
        if (ended == false)
        {
            constSpeed();       //apply constant speed to the player
        }

        //decrease fuel (TEMP)
        fuelBar.value -= fuelDecrease * Time.deltaTime;


        if (fingerOnScreen == false)
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)     //&& Input.GetTouch(0).position.y < Screen.height - Screen.height / 6 //check for touch input and that there are still more fuel cans 
            {
                // checkPause();           //check if the game is Puased
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    Debug.Log("Touched UI");
                }
                else if (fuelCans > 0)
                {
                    fingerOnScreen = true;
                    decideBoost();      //function that decided boost
                }
            }

            if (fuelCans == 0 && fuelBar.value == 0.0f || fuelBar.value == 0.0f)        //player ran out of fuel
            {
                empty();
            }
        }
    }

    void checkPause()  // check if the game has been paused
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);     //Create Raycast from camera to location of touch
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))    //check if its hit anything and that a co-routine isnt running
        {
            switch (hit.transform.gameObject.layer)       //check game object that has been hits identity and compare to identities with command
            {
                case 4:
                    isPaused = true;

                    break;
            }
        }

    }

    void constSpeed()
    {
        playerConstForce.force = new Vector3(0.0f, 0.5f, 0.0f);
    }

    void decideBoost()      //function to decide boost to apply depending on re-fuel tap
    {
        boostTapType = fuelBar.value;       //get value when player taps
        fuelBar.value = 10.0f;      // "Refill fuel"

        if(boostTapType > 5.0f)        //Bad boost as player tapped before fuel down to half tank
        {
            applyBoost(0);      //apply bad boost
        }
        else if(boostTapType <= 5.0f && boostTapType > 2.5)     //ok boost as player tapped under half tank
        {
            applyBoost(1);
        }
        else if(boostTapType <= 2.5 && boostTapType > 0.5)      //good boost as player tapped under quarter tank (migh change to >1)
        {
            applyBoost(2);
        }
        else if(boostTapType <= 0.5 && boostTapType > 0.0)      //perfect boost as player refueled when on fumes
        {
            applyBoost(3);
        }
        else if(boostTapType == 0.0)
        {
            empty();        //out of fuel, cant replace can now
        }

        fingerOnScreen = false;

    }

    void applyBoost(int boostNumber)      //possible re-fuel boosts 
    {
        switch (boostNumber)
        {
            case 0:     //Bad Boost
                playerRigidBod.AddForce(0.0f, 1.0f, 0.0f);      //apply boost
                fuelCans--;     //change value of remaining fuel cans
                anim.SetTrigger("BeginDump"); //play animation of can being thrown out
                break;

            case 1:     //Ok Boost
                playerRigidBod.AddForce(0.0f, 1.5f, 0.0f);      //apply boost
                fuelCans--;     //change value of remaining fuel cans
                anim.SetTrigger("BeginDump"); //play animation of can being thrown out
                break;

            case 2:     //good Boost
                playerRigidBod.AddForce(0.0f, 2.0f, 0.0f);      //apply boost
                fuelCans--;     //change value of remaining fuel cans
                anim.SetTrigger("BeginDump"); //play animation of can being thrown out
                break;

            case 3:     //perfect Boost
                playerRigidBod.AddForce(0.0f, 2.5f, 0.0f);      //apply boost
                fuelCans--;     //change value of remaining fuel cans
                anim.SetTrigger("BeginDump"); //play animation of can being thrown out
                break;

        }
    }

    void empty()
    {
        ended = true;       //game has ended
       

        if (Down)       //Show End Screen
        {
            endState();
        }

        //check if player has passed planet
        if (player.transform.position.y > planet.transform.position.y + 3.0f )
        {
            playerConstForce.force = new Vector3(0.0f, 0.0f, 0.0f);     //stop player from moving
            playerRigidBod.velocity = new Vector3(0.0f, 0.0f, 0.0f);

            float travelSpeed = movePace * Time.deltaTime;
            player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(3.0f, planet.transform.position.y + 3.0f, 14.9f), travelSpeed);
            playerRigidBod.velocity = new Vector3(0.0f, (-100.0f * Time.deltaTime), 0.0f);     //player moves down to planet
            Down = true;

            if (!isSaved)
            {
                int planetReached = PlayerPrefs.GetInt("AC_PlanetReached");
                planetReached++;
                PlayerPrefs.SetInt("AC_PlanetReached", planetReached);
                isSaved = true;
            }

           
        }
        else if ( player.transform.position.y < planet.transform.position.y + 3.0f && Down == false && coRoutineActive == false)
        {
            playerConstForce.force = new Vector3(0.0f, 0.0f, 0.0f);     //stop player from moving
            engine.Stop();      //stop engine particle effect when out of fuel
            fuelCans = 0;
            playerRigidBod.velocity = new Vector3(0.0f, (-100.0f * Time.deltaTime), 0.0f);      //player not reached planet, float down
            coRoutineActive = true;
            StartCoroutine(waitBeforeEnd());
        }

    }

    void endState()
    {
        if (playerRigidBod.velocity.y == 0.0 || playerRigidBod.velocity.y < 0.0f && Down == false)
        {
            playerRigidBod.velocity = new Vector3(0.0f, 0.0f, 0.0f);     //player stops moving

            Time.timeScale = 0;
            UI.SetActive(false);
            pauseScreen.SetActive(false);
            PlayerPrefs.SetInt("AcceptanceCurrentScore", score);

            Debug.Log("ENDED");

            endScreen.SetActive(true);           
        }
    }

    IEnumerator waitBeforeEnd()
    {
        yield return new WaitForSeconds(2);
        endState();
        StopCoroutine(waitBeforeEnd());
    }

}
