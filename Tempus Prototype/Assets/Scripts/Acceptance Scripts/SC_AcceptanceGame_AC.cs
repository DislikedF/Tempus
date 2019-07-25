//this script was written by Alan Guild
// this script controls the majority of the game

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class SC_AcceptanceGame_AC : MonoBehaviour
{
    //variables passed in from editor

    public Rigidbody playerRigidBod;       //hot air balloon rigidbody
    public ConstantForce playerConstForce;      //constant force on player
    public GameObject player;       //hot air balloon object

    public GameObject BigBalloon;
    public GameObject Flash;

    public GameObject planet;       //planet object
    public GameObject birdsRight;   //birds objects
    public GameObject birdsLeft;    // birds objects
    public GameObject UFO;              //ufo object
    public GameObject asteroids;        //asteroids objects
    public GameObject spaceBackground;     //tileable Background
    public GameObject star;         //star object to clone in the space background
    public GameObject endScreen;        //endscreen
    public GameObject pauseScreen;      //pause screen & button
    public GameObject UI;       //UI 
    public ParticleSystem engineLeft;       //flame particle system
    public ParticleSystem engineRight;       //flame particle system
    public Animator balloonAnim;       //animator for balloon
    public Animator planetAnim;       //animator for planet

    public AudioSource balloonflameConst;   //sound for constant flame
    public AudioSource balloonSwoosh;       //sound for when boosts are applied
    public AudioSource birdChirpLeft;       //sound for birds
    public AudioSource birdChirpRight;      //sound for birds
    public AudioSource UFOSound;            //sound for UFO
    public AudioSource asteroidSound;       //sound for asteroids
    public AudioSource destroyObject;       //sound for destroying objects


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
    void Start()
    {
        if (Time.timeScale != 1)        //set time to normal
        {
            Time.timeScale = 1;
        }

        setUpBackground();      //set up back groud to level distance

        //fuelCans = PlayerPrefs.GetInt("AC_noOfCannisters");     //check if player has bought cannisters (UNCOMMENT WHEN IN GAME MONEY ADDED!)

        engineLeft.Stop();      //have the particle system for the engine turned off to start with
        engineRight.Stop();      //have the particle system for the engine turned off to start with
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

        for (int s = 0; s < starCount; s++)      //creates clones of stars upto the same height of the background
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
            engineLeft.Play();      //play engine particle animation
            engineRight.Play();      //play engine particle animation
            balloonflameConst.Play();
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
            if (Input.touchCount == 1)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    switch (hit.transform.gameObject.name)
                    {
                        case "NULL":
                            //do nothing if nothing is touched
                            break;

                        case "HotAirBaloon_AC":     //command if tap hits the balloon
                            if (Input.GetTouch(0).phase == TouchPhase.Ended)     //&& Input.GetTouch(0).position.y < Screen.height - Screen.height / 6 //check for touch input and that there are still more fuel cans 
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
                            break;

                        case "BirdFlockToRight(Clone)":        //commands for if tap is birds flying to right
                            Destroy(hit.transform.gameObject);        //destroy
                            birdChirpRight.Stop();
                            destroyObject.Play();
                            //play destroy animation/particle effect
                            Debug.Log("Bird Right");
                            break;

                        case "BirdFlockToLeft(Clone)":         //commands for if tap is birds flying to left
                            Destroy(hit.transform.gameObject);        //destroy
                            birdChirpLeft.Stop();
                            destroyObject.Play();
                            //play destroy animation/particle effect
                            Debug.Log("Bird Left");
                            break;

                        case "UFO(Clone)":                     //command for if tap is on UFO
                            Destroy(hit.transform.gameObject);          //destroy
                            UFOSound.Stop();
                            destroyObject.Play();
                            //play destroy animation/particle effect
                            Debug.Log("UFO");
                            break;

                        case "asteroids(Clone)":
                            Destroy(hit.transform.gameObject);       //destroy
                            asteroidSound.Play();       //sound for destroying asteroids
                            //play destroy animation/particle effect
                            Debug.Log("asteroids");
                            break;
                    }
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

        if (boostTapType > 5.0f)        //Bad boost as player tapped before fuel down to half tank
        {
            applyBoost(0);      //apply bad boost
        }
        else if (boostTapType <= 5.0f && boostTapType > 2.5)     //ok boost as player tapped under half tank
        {
            applyBoost(1);
        }
        else if (boostTapType <= 2.5 && boostTapType > 0.5)      //good boost as player tapped under quarter tank (migh change to >1)
        {
            applyBoost(2);
        }
        else if (boostTapType <= 0.5 && boostTapType > 0.0)      //perfect boost as player refueled when on fumes
        {
            applyBoost(3);
        }
        else if (boostTapType == 0.0)
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
                if (!balloonSwoosh.isPlaying)
                {
                    balloonSwoosh.Play();
                }
                playerRigidBod.AddForce(0.0f, 1.0f, 0.0f);      //apply boost
                fuelCans = fuelCans - 1;     //change value of remaining fuel cans
                balloonAnim.SetTrigger("BeginDump"); //play animation of can being thrown out
                break;

            case 1:     //Ok Boost
                if (!balloonSwoosh.isPlaying)
                {
                    balloonSwoosh.Play();
                }
                playerRigidBod.AddForce(0.0f, 1.5f, 0.0f);      //apply boost
                fuelCans = fuelCans - 1;    //change value of remaining fuel cans
                balloonAnim.SetTrigger("BeginDump"); //play animation of can being thrown out
                break;

            case 2:     //good Boost
                if (!balloonSwoosh.isPlaying)
                {
                    balloonSwoosh.Play();
                }
                playerRigidBod.AddForce(0.0f, 2.0f, 0.0f);      //apply boost
                fuelCans = fuelCans - 1;     //change value of remaining fuel cans
                balloonAnim.SetTrigger("BeginDump"); //play animation of can being thrown out
                break;

            case 3:     //perfect Boost
                if (!balloonSwoosh.isPlaying)
                {
                    balloonSwoosh.Play();
                }
                playerRigidBod.AddForce(0.0f, 2.5f, 0.0f);      //apply boost
                fuelCans = fuelCans - 1;     //change value of remaining fuel cans
                balloonAnim.SetTrigger("BeginDump"); //play animation of can being thrown out
                break;

        }
    }

    void empty()
    {
        ended = true;       //game has ended

        fuelBar.gameObject.SetActive(false);       //turn off the slider

        if (Down)       //Show End Screen
        {
            endState();
        }

        //check if player has passed planet
        if (player.transform.position.y > planet.transform.position.y + 3.0f)
        {
            playerConstForce.force = new Vector3(0.0f, 0.0f, 0.0f);     //stop player from moving
            playerRigidBod.velocity = new Vector3(0.0f, 0.0f, 0.0f);

            Destroy(Flash);
            BigBalloon.SetActive(true);

            balloonAnim.SetTrigger("EndState");        //Set animation trigger
            planetAnim.SetTrigger("EndPlanet");        //Set animation trigger

            StartCoroutine(waitForAnimations());

            //float travelSpeed = movePace * Time.deltaTime;
            //player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(3.0f, planet.transform.position.y + 3.0f, 14.9f), travelSpeed);
            //playerRigidBod.velocity = new Vector3(0.0f, (-100.0f * Time.deltaTime), 0.0f);     //player moves down to planet        change to animation

            // Down = true;

            if (!isSaved)
            {
                int planetReached = PlayerPrefs.GetInt("AC_PlanetReached");
                planetReached++;
                PlayerPrefs.SetInt("AC_PlanetReached", planetReached);
                isSaved = true;
            }


        }
        else if (player.transform.position.y < planet.transform.position.y + 3.0f && Down == false && coRoutineActive == false)
        {
            playerConstForce.force = new Vector3(0.0f, 0.0f, 0.0f);     //stop player from moving
            engineLeft.Stop();      //stop engine particle effect when out of fuel
            engineRight.Stop();      //stop engine particle effect when out of fuel
            balloonflameConst.Stop();
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
            player.SetActive(false);

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

    IEnumerator waitForAnimations()
    {
        yield return new WaitForSeconds(8.5f);
        balloonflameConst.Stop();
        endState();
        StopCoroutine(waitBeforeEnd());
    }

}
