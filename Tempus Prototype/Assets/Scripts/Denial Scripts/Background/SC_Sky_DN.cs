using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SC_Sky_DN : SC_Shared_DN
{
    #region Variables

    #region Game Objects
    public GameObject skyHolder;
    public GameObject clouds;
    public GameObject player;
    #endregion

    #region Animation
    // MH - Create dynamic movement of the clouds in the scene
    private float xSway;
    private float pi;
    private bool swaySwap;
    #endregion

    #region Offsets
    public float deltaPlayerZPos = 0.0f;
    public float maxDeltaPlayerZPos = 0.5f;
    public float distanceInFront = 0.0f;
    public float prevPlayerZPos = 0.0f;
    #endregion

    // MH - Set the maximum distance in which to create new clouds
    public int renderDistance;
    // MH - Set the clouds z-position
    private float zOffset;
    
    // MH - Define a value for the players z-position
    float playerZPos;
    // MH - Set parameters for the clouds
    public int numberOfClouds = 15;
    int cloudOutOfSight = 15;
    // MH - RNG Variables (Random Number Generation)
    public float cloudSpawnProbability;


    #endregion

    #region Functions

    #region Start Functions
    void Start()
    {
        // MH - Initialise the Cloud Animation Variables
        SetupCloudAnimation();
        GenerateInitialClouds();
    }


    void SetupCloudAnimation()
    {
        xSway = 0.0f;
        pi = 3.14159265f;
        swaySwap = true;
    }

    void GenerateInitialClouds()
    {
        // MH - Setup an initial set of obstacles
        for (int i = 0; i < renderDistance; i++)
        {
            // 0 to 100 gives us a nice mental picture of what probability to select
            if (UnityEngine.Random.Range(0.0f, 100.0f) < cloudSpawnProbability)
            {
                // MH - Create the obstacle
                (Instantiate(
                    clouds,
                    new Vector3(
                        UnityEngine.Random.Range(-75.0f, 75.0f),
                        transform.position.y,
                        i),
                    Quaternion.identity)
                    as GameObject).transform.parent = skyHolder.transform;
            }
        }
    }
    #endregion

    #region Update Functions
    void Update()
    {
        if (!isPaused())
        {
            // MH - Get the current position of the player on the z-axis
            playerZPos = player.transform.position.z;
            CloudGenerator();
        }   
    }

    void CloudGenerator()
    {
        distanceInFront = playerZPos + renderDistance;
        deltaPlayerZPos += (playerZPos - prevPlayerZPos);

        if (deltaPlayerZPos > maxDeltaPlayerZPos)
        {
            deltaPlayerZPos = 0.0f;
            //MH - This sets the limit that the path can draw. Set this to be just outside the camera view
            if (distanceInFront < playerZPos + renderDistance)
            {
                // 0 to 100 gives us a nice mental picture of what probability to select
                if (UnityEngine.Random.Range(0.0f, 100.0f) <= cloudSpawnProbability)
                {
                    // MH - Create the obstacle
                    (Instantiate(
                        clouds,
                        new Vector3(
                            UnityEngine.Random.Range(-75.0f, 75.0f), 
                            transform.position.y, 
                            distanceInFront),
                        Quaternion.identity)
                        as GameObject).transform.parent = skyHolder.transform;
                }
            }
        }

        prevPlayerZPos = playerZPos;
    }

    #endregion

    #endregion
}
