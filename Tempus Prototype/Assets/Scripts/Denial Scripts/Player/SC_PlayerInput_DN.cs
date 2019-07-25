using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerInput_DN : SC_Shared_DN
{
    // MH - variables for the application of touch screen swipe input
    private Vector2 startPos;
	private Vector2 direction;
    public bool isDirLocked = false;
    public bool isLeftLocked = false;
    public bool isRightLocked = false;

    public float movVel;
    private float disT = 8.0f;
    private float inputDelay = 0.0f;

    public int playerCurrPos;

	void Update()
	{
        if (!isPaused())
        {
            // MH - Input Handling
            DesktopInput();
            MobileInput();
        }
	}

    // MH - Functions to handle types of input
	void MobileInput()
    {
        if (isDirLocked)
        {
            if (isLeftLocked)
            {
                MoveLeft();
            }
            if (isRightLocked)
            {
                MoveRight();
            }
        }

        if (inputDelay < 0.001f)
        {
            inputDelay = 0.0f;
            // MH - Track a single touch as a direction control.
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // MH - Handle finger movements based on touch phase.
                switch (touch.phase)
                {
                    // MH - Record initial touch position.
                    case TouchPhase.Began:
                        startPos.x = touch.position.x;
                        break;

                    // MH - Determine direction by comparing the current touch position with the initial one.
                    case TouchPhase.Moved:
                        direction.x = touch.position.x - startPos.x;
                        if ((direction.x < -5.0f) || (direction.x > 5.0f))
                        {
                            PlayAudio();
                            inputDelay = 0.5f;
                        }
                        if (!isDirLocked)
                        {
                            // MH - Detect if the player has swiped left or riht and move the player accordingly
                            // MH - Swipe Left  (-ve x val) 
                            if ((direction.x < -5.0f) && (transform.position.x >= 0.2f))
                            {
                                MoveLeft();
                            }
                            // MH - Swipe Right (+ve x val) 
                            else if ((direction.x > 5.0f) && (transform.position.x <= 4.8f))
                            {
                                MoveRight();
                            }
                        }
                        break;
                }
            }
        }
        else if (inputDelay > 0.0f)
        {
            inputDelay -= Time.deltaTime;
        }

    }
	void DesktopInput()
	{
        if (!isDirLocked)
        {
            if ((Input.GetKeyDown("a")) && (transform.position.x >= 0.5f)) // Player "swipes" left
            {
                PlayAudio();
                // MH - move player to path on the left
                MoveLeft();
            }
            else if ((Input.GetKeyDown("d")) && (transform.position.x <= 4.5f)) // Player "swipes" right
            {
                PlayAudio();
                // MH - move player to path on the right
                MoveRight();
            }
        }
		else
        {
            if (isLeftLocked)
            {
                MoveLeft();
            }
            if (isRightLocked)
            {
                MoveRight();
            }
        }
	}


    void MoveLeft()
    {
        // MH - Locking
        isDirLocked = true;
        isLeftLocked = true;
        isRightLocked = false;

        movVel += (disT * Time.deltaTime);

        // MH - Smooth maths for a smoooooth animation
        float x = playerCurrPos - ((-1.0f / 2.0f) * (Mathf.Cos(movVel)) + 0.5f);

        // MH - move player to path on the left
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, x * 10);


        if (transform.position.x < ((float)playerCurrPos - 0.98f))
        {
            // Set the new position of the player
            playerCurrPos -= 1;
            transform.position = new Vector3(playerCurrPos, transform.position.y, transform.position.z);
            transform.rotation = Quaternion.Euler(0, 0, 0);

            // Reset movVel
            movVel = 0.0f;

            inputDelay = 0.3f;

            // Reset the locks
            isDirLocked = false;
            isLeftLocked = false;
            isRightLocked = false;
        }
    }
    void MoveRight()
    {
        // Locking
        isDirLocked = true;
        isLeftLocked = false;
        isRightLocked = true;

        movVel += (disT * Time.deltaTime);

        // Smooth maths for a smoooooth animation
        float x = playerCurrPos + ((-1.0f / 2.0f) * (Mathf.Cos(movVel)) + 0.5f);

        // MH - move player to path on the Right
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -(x * 10));


        if (transform.position.x > ((float)playerCurrPos + 0.95f))
        {
            // Set the new position of the player
            playerCurrPos += 1;
            transform.position = new Vector3(playerCurrPos, transform.position.y, transform.position.z);
            transform.rotation = Quaternion.Euler(0, 0, 0);

            // Reset movVel
            movVel = 0.0f;

            inputDelay = 0.3f;

            // Reset the locks
            isDirLocked = false;
            isLeftLocked = false;
            isRightLocked = false;
        }
    }


    void PlayAudio()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        audio.Play(44100);
    }
}