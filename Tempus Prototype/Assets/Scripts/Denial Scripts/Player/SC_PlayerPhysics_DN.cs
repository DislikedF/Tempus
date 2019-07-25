using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_PlayerPhysics_DN : SC_Shared_DN
{
    // MH - Variable Declarations
    public float constVel;
    // MH - Each stage requires a different velocity as the game increases in pace
    public float S1maxVel;
    public float S2maxVel;

    // MH - This will be attached to the 3D player model child object
    public Animator moveAnim;

    void FixedUpdate ()
    {
        if (!isPaused())
        {
            // MH - Update the players transform
            transform.Translate(Vector3.forward * constVel * Time.deltaTime);

            // MH - Update the velocity of the player over time to increase difficulty
            // MH - The velocity will increase in stages
            if (transform.position.z < 500)
            {
                if (constVel < S1maxVel)
                {
                    constVel += (0.5f * Time.deltaTime);
                }
            }
            if (transform.position.z > 500)
            {
                if (constVel < S2maxVel)
                {
                    constVel += (1.0f * Time.deltaTime);
                }
            }

            if (transform.position.y < -5)
            {
                PlayerPrefs.SetInt("DenialCurrentScore", (int)transform.position.z);
                endScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }   
    }

    
}
