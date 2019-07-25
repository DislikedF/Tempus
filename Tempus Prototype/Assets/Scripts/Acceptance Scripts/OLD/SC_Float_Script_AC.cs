//Acceptance_Script authored by Alan Guild
// This script applys the physics to the Hot Air Balloon and also works with the state of the stage.

//To Do
//- send back to meta when ended

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SC_Float_Script_AC : MonoBehaviour
{

    public Rigidbody hotAirBalloon;
    public Transform balloonTransform;
    public Transform planetTransform;
    public ParticleSystem engine;

    private bool isBoost = false;       //bool for if a boost is currently being applied
    private bool isGrounded = true;     //bool to say if Balloon has Taken off
    private bool fingerOnScreen = false;        //bool to say if there is currently a touch
    private bool isCoRoutineActive = false;     //bool to check if co-routines are active
    private bool hasMoved = false;      //bool to say if the balloon has moved over the planet

    public int noOfCanisters;

    //UI Variables

    public Text cans;       //text to show the amount of fuel cannisters the playere has left to you
    public Text scoreText;
    private int score = 0;



    //power bar variables
    public Slider PowerSlider;
    private Slider powerBar;
    private float fuelLoss = 2.0f;
    private float boostOnTouch;


    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        //Power Bar
        powerBar = PowerSlider;

        powerBar.minValue = 0f;
        powerBar.maxValue = 10f;
        powerBar.value = 10.0f;

        //Particle System
        engine.Stop();
    }

    // Update is called once per frame
    private void Update()
    {
        //UI

        cans.text = noOfCanisters.ToString();
        scoreText.text = score.ToString();

        if (hotAirBalloon.velocity.y > 0.0)
        {
            Score();
        }

        if (isGrounded && Input.touchCount > 0 )      //takeoff
        {
            fingerOnScreen = true;
            noOfCanisters += 1;
            TakeOff();
        }

        //reduce fuel & apply constant speed
        if (!isGrounded)
        {
            PowerBar();
        }

    }

    void PowerBar()
    {
        ConstantSpeed();        //gives balloon constant speed

        //fuel loss
        powerBar.value -= fuelLoss * Time.deltaTime;
        if (fingerOnScreen == false)
        {
            fingerOnScreen = true;
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)       //if user touch and noOfCanisters is not empty
            {
                if (noOfCanisters >= 0 && powerBar.value != 0.0f)
                {
                    isCoRoutineActive = true;
                    StartCoroutine(ApplyBoost());
                }

            }
            else if (noOfCanisters == 0 || powerBar.value == 0.0f)
            {
                isCoRoutineActive = true;
                StartCoroutine(Empty());
            }
        }

        if (Input.touchCount == 0)
        {
            fingerOnScreen = false;
        }

    }

    void Boosts(int boostCase)
    {
        switch (boostCase)
        {

            case 0:     //Bad Boost
                isBoost = true;
                Handheld.Vibrate();
                hotAirBalloon.AddForce(0.0f, 1.5f, 0.0f, ForceMode.Impulse);
                noOfCanisters--;
                isBoost = false;

                Debug.Log("Bad");
                break;

            case 1:     //Ok Boost
                isBoost = true;
                Handheld.Vibrate();
                hotAirBalloon.AddForce(0.0f, 3.0f, 0.0f, ForceMode.Impulse);
                score += 250;
                noOfCanisters--;
                isBoost = false;

                Debug.Log("Ok");
                break;

            case 2:     //Perfect Boost
                isBoost = true;
                Handheld.Vibrate();
                hotAirBalloon.AddForce(0.0f, 8.0f, 0.0f, ForceMode.Impulse);
                score += 500;
                noOfCanisters--;
                isBoost = false;

                Debug.Log("Perfect");
                break;

            case 3:     //out of fuel
                isBoost = true;
                Handheld.Vibrate();
                engine.Stop();
                Handheld.Vibrate();
                noOfCanisters = 0;
                hotAirBalloon.velocity = new Vector3(0.0f, (-1.0f * Time.deltaTime), 0.0f);
                isBoost = false;

                Debug.Log("Out Of Fuel");

                break;

            case 4:
                reachedPlanet();
                break;

        }
    }

    IEnumerator ApplyBoost()
    {
        boostOnTouch = powerBar.value;
        powerBar.value = 10.0f;

        if (boostOnTouch >= 5.0f && !isBoost)
        {
            Boosts(0);
        }
        else if (boostOnTouch < 5.0f && boostOnTouch >= 2.0f && !isBoost)
        {
            Boosts(1);
        }
        else if (boostOnTouch < 2.0f && boostOnTouch > 0.0f && !isBoost)
        {
            Boosts(2);
        }

        yield return null;

        isCoRoutineActive = false;
    }

    IEnumerator Empty()
    {
        powerBar.value = 0.0f;

        if (balloonTransform.position.y < planetTransform.position.y + 2.0f && !isBoost)
        {
            Boosts(3);
        }
        else if (balloonTransform.position.y >= planetTransform.position.y + 2.0f && !isBoost)
        {
            Boosts(4);
        }


        yield return null;

        isCoRoutineActive = false;
    }

    void TakeOff()
    {
        if (isGrounded)     //initial constant take off speed
        {
            engine.Play();
            isGrounded = false;
            hotAirBalloon.AddForce(0.0f, 1.0f, 0.0f, ForceMode.Impulse);
        }
        fingerOnScreen = false;
    }

    void ConstantSpeed()
    {
        if (hotAirBalloon.velocity.y < 1.0 && noOfCanisters > 0 && isGrounded == false)
        {
            hotAirBalloon.AddForce(0.0f, 2.5f, 0.0f, ForceMode.Impulse);
        }
    }

    void Score()
    {
        // score = (int)hotAirBalloon.position.y;

        score += 1;

    }

    void reachedPlanet()        //reached the new reality
    {
        if (!hasMoved)
        {
            hasMoved = true;
            isCoRoutineActive = true;
            StartCoroutine(moveToEndPoint());
            PlayerPrefs.SetInt("AcceptanceScore", score);       //Assign Score To Save
        }

        engine.Stop();

        //Hot Airballoon Object has a script attatched to detect when it collides with the new planet 
    }

    IEnumerator moveToEndPoint()
    {
        float i = 0.0f;
        Vector3 from = hotAirBalloon.position;
        Vector3 to = new Vector3(planetTransform.position.x, hotAirBalloon.position.y, hotAirBalloon.position.z);
        while (i < 1f)     //move over planet
        {
            i += Time.deltaTime;
            hotAirBalloon.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            hotAirBalloon.transform.position = Vector3.Lerp(from, to, Mathf.SmoothStep(0f, 1f, i));
            yield return null;
        }
        hotAirBalloon.velocity = new Vector3(0.0f, (-1.0f * Time.deltaTime), 0.0f);
        isCoRoutineActive = false;
    }



}

