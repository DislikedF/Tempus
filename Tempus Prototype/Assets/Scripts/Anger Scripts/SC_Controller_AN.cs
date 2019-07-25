using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SC_Controller_AN : MonoBehaviour
{

    // Initialise variables
    //public Camera camera;

    public float bonusSpeed = 10;
    public float velocity;
    public Text timeText;
    public int timer = 30;
    public Text scoreText;
    public GameObject joystick;
    public GameObject endScreen;
    private int score;
    private int scoreMultiplier;
    private int loseCount = 0;
    public int loseTime = 1000;
    public int addTime;

    private float horizontalVel = 0.0f;

    private int touchCount;

    private Rigidbody rb;
    public float startSpeed = 1.0f;
    public float acceleration = 0.05f;
    public float MAX_VELOCITY = 50.0f;

    private Vector3 lastVelocity;

    public GameObject ground;

    void Start()
    {
        if(Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }

        // Set the player's position to the ground
        transform.position = new Vector3(0.0f, 
                                        (GetComponent<Renderer>().bounds.size.y/ 2) 
                                        + (ground.GetComponent<Renderer>().bounds.size.y /2),
                                        0.0f);
        joystick.SetActive(false);

        // Get the player's RigidBody
        rb = GetComponent<Rigidbody>();

        // Lock the screen orientation to portrait
        Screen.orientation = ScreenOrientation.Portrait;

        score = 0;
        scoreMultiplier = 1;

        UpdateScore();
        StartCoroutine("Countdown");
    }

    void Main()
    {
        // Prevent phone from sleeping
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

    }

    private void Update()
    {
        if (Time.timeScale == 1)
        {
            // Exit condition for mobile
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

            // Detect touch input
            touchCount = Input.touchCount;

            if (touchCount > 0)
            {
                switch (Input.GetTouch(0).phase)
                {
                    case TouchPhase.Began:

                        break;

                    case TouchPhase.Moved:
                        // Move the player based on horizontal touch movement
                        horizontalVel = Input.GetTouch(0).deltaPosition.x;
                        break;

                    case TouchPhase.Ended:
                        // Reset the horizontal movement once the player is no longer touching the screen
                        horizontalVel = 0.0f;

                        break;

                    default:
                        break;

                }
            }
            //StartCoroutine("SlowLose");
            velocity = rb.velocity.magnitude;

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.timeScale == 1)
        {
             // Calculate movement vector
             Vector3 movement = Vector3.zero;
             movement.x = horizontalVel;

            // Apply force to the player
            rb.AddForce(movement, ForceMode.Acceleration);

            if(rb.velocity.z <= MAX_VELOCITY)
            {
                rb.AddForce(new Vector3(0.0f, 0.0f, startSpeed), ForceMode.Force);
            }

            lastVelocity = rb.velocity;
            //speedForce += acceleration;
            

            // Display the timer
            timeText.text = "" + timer;
        }
    }

    // Handle Collisions
    void OnCollisionEnter(Collision col)
    {
        // Check for collisions with objects   
        if(col.gameObject.tag == "Obstacle")
        {
            // Preserve speed
            rb.velocity = lastVelocity;

            // Apply the speed boost
            startSpeed += acceleration;
            // Destroy the object
            col.gameObject.SetActive(false);
            // Add score
            AddScore(50);
            UpdateScore();
            // Add time to the timer
            timer += addTime;
        }
    }
    
    // Adds score
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue * scoreMultiplier;
        UpdateScore();
    }

    // Updates the score counter
    void UpdateScore()
    {
        scoreText.text = "" + score;
    }

    // Handles losing based on staying still too long
    IEnumerator SlowLose()
    {
        if (velocity < 1)
        {
            loseCount++;
        } else
        {
             loseCount = 0;
        }

        if(loseCount >= loseTime)
        {
            PlayerPrefs.SetInt("AngerCurrentScore", score);
            //SceneManager.LoadScene("MetaGame");
            Time.timeScale = 0;
            endScreen.SetActive(true);
        }
        yield return new WaitForSecondsRealtime(2);
        yield return null;
    }

    // Handles keeping the timer ticking down in real seconds
    IEnumerator Countdown()
    {
        do
        {
            yield return new WaitForSecondsRealtime(1);
            timer--;
        } while (timer >= 0);
        if(timer < 0)
        {
            timeText.text = "";
            endScreen.SetActive(true);
            PlayerPrefs.SetInt("AngerCurrentScore", score);
            Time.timeScale = 0;
        }
        yield return null;
    }
}
