using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SC_PlayerControl_01_BA : MonoBehaviour
{


    public Rigidbody rb;
    public Camera cam;
    public Texture textureLine;
    public GameObject endScreen;
    public LineRenderer lineRenderer;
    private Vector2 firstTouch;
    private Vector2 distance;
    private float screenWidthX;
    private Vector2 touchDeltaPosition;
    public bool canJump;
    public Animator animate;
    public AudioSource jumpSound;
    public AudioSource inAir;

    private Vector3 endPos;
    private Vector2 velocity = Vector2.zero;
    private float smoothTime = 3.5f;
    private float time1;
    private float time2;
   

    public ParticleSystem thrusterL;
    public ParticleSystem thrusterR;


    //Ryan Fearon
    // Use this for initialization
    void Start()
    {
        SetUp();
    }

    // Runs every frame
    void Update()
    {
        float lasPos = rb.position.y;
        playerOnScreen();

        UpdateLine();
       
        //end player
        Die(false);
        //show current boost value
        //boosterValue.text = "yea";
        // 

        if(rb.velocity.y > 1)
        {
            animationCheck(true);
        }
        else
        {
            animationCheck(false);
        }

    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 1)
        {
            //move player
            Control();
           

        }
    }

    //Keep player on screen
    void playerOnScreen()
    {
        Vector3 position;
        position = cam.WorldToScreenPoint(rb.transform.position);
        if (position.x <= 0)
        {
            position.x = 10;
            rb.transform.position = cam.ScreenToWorldPoint(position);
            rb.velocity = new Vector3(-rb.velocity.x/2, rb.velocity.y/2, 0.0f);
        }

        if (position.x >= screenWidthX) {
            position.x = screenWidthX;
            rb.transform.position = cam.ScreenToWorldPoint(position);
            rb.velocity = new Vector3(-rb.velocity.x/2, rb.velocity.y/2, 0.0f);
        }


    }

    void animationCheck(bool movement)
    {



        animate.SetBool("move", movement);
        if(movement == true)
        {
            inAir.Play();
        }
        if(movement == false)
        {
            inAir.Stop();
        }
           //     animate.SetTrigger("Exit");


        
    }
    //Handle player input
    void Control()
    {
        //for testing
        if (Input.GetKey(KeyCode.UpArrow)) {

            rb.velocity = new Vector3(0.0f, 5.0f, 0.0f);
         //   animationCheck(true);

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
           // i = i - 0.001f;
            rb.velocity = new Vector3(-5.0f, 0.0f, 0.0f);
            //animationCheck(false);
            //    sr1.color = new Color(sr1.color.r, sr1.color.g, sr1.color.b, i);
            
            //sr.color = Color.blue;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //i = i + 0.001f;
            rb.velocity = new Vector3(5.0f, 0.0f, 0.0f);
           // sr1.color = new Color(sr1.color.r, sr1.color.g, sr1.color.b, i);

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            SceneManager.LoadScene("MetaGame");

        }


         if (Input.touchCount > 0) {
        if (Input.GetTouch(0).phase == TouchPhase.Began) {

                time1 = Time.unscaledTime;
            firstTouch = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);

        }

        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {

                if (canJump == true)
                {
                    // Get movement of the finger since last frame
                    Vector2 touchDeltaPosition = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);


                    // distance = firstTouch - touchDeltaPosition;
                    //lineRenderer.SetPosition(0, rb.position);
                    //lineRenderer.SetPosition(1, new Vector3(distance.x, distance.y, rb.position.z));
                    lineRenderer.enabled = true;

                }
        }
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
                if (canJump == true)
                {
                    time2 = Time.unscaledTime;
                    if (time2 - time1 > 0.2)
                    {
                        MovePlayer();
                        jumpSound.Play();
                    }
                    lineRenderer.enabled = false;

                    thrusterL.Stop();
                    thrusterR.Stop();
                }
        }


    }

    }

    //}

   public void Die(bool death)
    {
        if (cam.WorldToViewportPoint(rb.position).y <=0 || death == true) {
            // die and activate endscreen
            // SceneManager.LoadScene ("MetaGame");
            StopGame();
            Time.timeScale = 0;


        }
    }

    void UpdateLine()
    {
        if (lineRenderer.enabled == true)
        {
            
            Vector3 startPos = rb.position;
            
            touchDeltaPosition = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);

            distance = firstTouch - touchDeltaPosition;
            distance = (new Vector2(distance.x, distance.y) / 2);

            Vector3 lineDistance = new Vector3(distance.x, distance.y, rb.position.z);
            float dist = Mathf.Clamp(distance.magnitude, 0.0f, 10.0f);
            endPos = startPos + (lineDistance.normalized * dist);
            //lineRenderer.materials[0].mainTextureScale = new Vector3(2.0f, 1.0f, 1.0f);


            lineRenderer.SetPosition(0, rb.position);
            lineRenderer.SetPosition (1, new Vector3(endPos.x, endPos.y, rb.position.z));
        }
   
        
    }
     void StopGame()
    {
        if (endScreen != null)
        {
            endScreen.SetActive(true);
        }
    }
	//Set up values
	void SetUp ()
	{
        if (Time.timeScale != 1)
        {

            Time.timeScale = 1;

        }

        rb = GetComponent<Rigidbody> ();
        canJump = true;
   

		thrusterL.Stop ();
		thrusterR.Stop ();
		screenWidthX = Screen.width;
		
       lineRenderer = gameObject.AddComponent<LineRenderer>();

        



        lineRenderer.sortingLayerName = "OnTop";
        lineRenderer.sortingOrder = 5;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = (0.3f);
        lineRenderer.textureMode = LineTextureMode.Tile;
        lineRenderer.material.mainTextureScale = new Vector3(0.01f, 2.0f, 0.0f);
        lineRenderer.useWorldSpace = true;
        lineRenderer.material.mainTexture = textureLine;
        lineRenderer.enabled = false;
       
    }

	// handle screen input and get player move direction
   
   

	// move player left+up pr right+up
	void MovePlayer ()
	{
        
        Vector3 clamp = Vector3.ClampMagnitude(distance, 100.0f);
        Vector2 target = new Vector2(clamp.x * 0.5f, clamp.y*0.5f);
        //Vector3.SmoothDamp(Follower.transform.position, targetPosition, ref velocity, smoothTime);
        rb.velocity = target;
            distance = new Vector3(0.0f, 0.0f, 0.0f);
        
        canJump = false;
           

	}

	//falling if true
	bool Falling ()
	{
		if (rb.velocity.y < 0.0f) {
			return true;
		} else
			return false;
	}

   

	void RotateForward ()
	{
		if (rb.velocity.x > 0)
        {
			
		}
        
	}

	void RotateBackward ()
	{
		if (rb.velocity.x < 0)
        {
			
		}

	}


   

}

