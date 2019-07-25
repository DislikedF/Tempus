using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to a path object when it is to be broken off from the rest of the path.
// This works by adding a rigidbody to the object and applying gravity.
// The object is then removed from the game when it reaches a certain height below the path.

public class SC_PathBreak_DN : SC_Shared_DN
{
    // MH - This script controls how this game object breaks from the path

    float displacement = 0.0f;

    float warningTimer = 0.0f;

    float flash = 0.0f;

    Color redWarningFlash = new Vector4(0,0,0,0);

    Color originalColours;


    private void Start()
    {
        originalColours = GetComponent<Renderer>().material.color;
    }

    void Update ()
    {
        if (!isPaused())
        {
            if (warningTimer < 1.0f)
            {
                warningTimer += 1.0f * Time.deltaTime;
                flash += 1.0f * Time.deltaTime;
                redWarningFlash = new Vector4(flash,0,0,0);
                GetComponent<Renderer>().material.color += redWarningFlash;
            }
            else
            {
                // MH - Acceleration due to gravity, this is cheaper than using a rigidbody
                displacement += 9.81f * Time.deltaTime;
                gameObject.transform.Translate(0.0f, -(displacement * Time.deltaTime), 0.0f);

                if (gameObject.transform.position.y < -5.0f)
                {
                    // MH - Reset the displacement value and position
                    displacement = 0.0f;
                    transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
                    // MH - Now move these blocks ahead like any other
                    if (gameObject.tag == "Path_DN")
                    {
                        gameObject.transform.Translate(-GetRenderDistance_DN(), 0, 0);
                    }
                    else if (gameObject.tag == "Obstacle_DN")
                    {
                        gameObject.transform.Translate(0, 1, GetRenderDistance_DN());
                    }
                    // MH - Reset the warning timer
                    warningTimer = 0.0f;
                    // MH - Reset the objects colour
                    GetComponent<Renderer>().material.color = originalColours;
                    // MH - Disable this component
                    enabled = false;
                }
            }
        } 
    }
}
