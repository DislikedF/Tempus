using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Background_DN : SC_Shared_DN
{
    #region Variables
    #region Game Objects
    // MH - Take the relevant GameObjects
    // MH - Ground
    public GameObject backgroundHolder;
    public GameObject ground;
    public GameObject tree;
    public GameObject[] bushes = new GameObject[8];
    public GameObject[] rocks = new GameObject[3];
    // MH - Sky
    public GameObject skyHolder;
    public GameObject clouds;
    // MH - Create arrays of gameobjects, to store the relevant generated ones
    // MH - Ground
    private GameObject[] groundObjects;
    private GameObject[] floraObjects;
    // MH - Sky
    private GameObject[] skyObjects;
    // MH - Take a representation of the player object
    public GameObject player;
    #endregion

    // MH - A holder to contain the most recent player Z-position
    float playerZPos;

    // MH - These are variables to correct for deleting ground objects outside the players view
    public int groundOutOfSight, floraOutOfSight, cloudOutOfSight;

    #region Procedural Generation
    // MH - Ground
    float groundXPos, groundZPos;
    public float groundXPosOffset;
    // MH - Tree
    public float treeSpawnProbability;
    public float treeYOffset;
    // MH - Bush
    int numberOfBushes;
    public float bushSpawnProbability;
    public float bushYOffset;
    // MH - Rock
    int numberOfRocks;
    public float rockSpawnProbability;
    public float rockYOffset;
    // MH - Cloud
    public float cloudSpawnProbability;
    #endregion
    #endregion

    #region Mutator Methods
    // MH - Output a random value between 0 - 100
    float GetRandPercentageVal() { return Random.Range(0.0f, 100.0f); }
    // MH - Ground
    float GetRandGroundOffset() { return Random.Range(-5.0f, 5.0f); }
    float GetRandPosOnGround() { return Random.Range(-22.5f, 27.5f); }
    // MH - Sky
    float GetRandPosInSky() { return Random.Range(-32.5f, 37.5f); }
    float GetRandSkyPos() { return Random.Range(5.0f, 25.0f); }
    // MH - Flora Types
    int GetRandBushType(int numOfBushes) { return Random.Range(0, (numOfBushes - 1)); }
    int GetRandRockType(int numOfRocks) { return Random.Range(0, (numOfRocks - 1)); }
    #endregion

    #region Functions
    #region Start
    void Start ()
    {
        SetupVariables();
        
        GenerateBackground();
    }

    void SetupVariables()
    {
        playerZPos = player.transform.position.z;

        // MH - Get these game object array sizes for later calculations
        numberOfBushes = bushes.Length;
        numberOfRocks = rocks.Length;
    }

    // MH - These functions handle the background setup:
    void GenerateBackground()
    {
        // MH - Spawn all ground items
        GroundSpawn();
        TreeSpawn();
        BushSpawn();
        RockSpawn();
        // MH - Spawn all sky items
        SkySpawn();

        // MH - For the big ground tiles we are using
        groundObjects = GameObject.FindGameObjectsWithTag("Ground_DN");
        // MH - Basically for everything we put on the ground
        floraObjects = GameObject.FindGameObjectsWithTag("Flora_DN");
        // MH - Contains all the clouds in the scene
        skyObjects = GameObject.FindGameObjectsWithTag("Sky_DN");
    }
    // MH - Spawn the sections of ground in a flat grid of 'x' by 'z'
    void GroundSpawn()
    {
        // MH - For each position on the z-axis
        for (int z = 1; z < 11; z++)
        {
            // MH - Multiply the value by 15 to magnify the grid
            groundZPos = z * 15;

            // MH - And for each position on the x-axis
            for (int x = 1; x < 6; x++)
            {
                // MH - Multiply the grid by 15 in the x-axis
                groundXPos = x * 15;

                // MH - Create the ground object based on these positions
                (Instantiate(
                    // MH - Object
                    ground,
                    // MH - Position (Add random variance to the grounds 'x' and 'z' positions)
                    new Vector3
                    (
                        groundXPos - groundXPosOffset + GetRandGroundOffset(),  // x
                        transform.position.y,                                   // y
                        groundZPos + GetRandGroundOffset()                      // z
                    ),
                    // MH - Rotation
                    Quaternion.identity)
                    // MH - Add to the parent object
                    as GameObject).transform.parent = backgroundHolder.transform;
            }
        }

        groundObjects = GameObject.FindGameObjectsWithTag("Ground_DN");
    }
    // MH - Functions to detail the ground with objects
    void TreeSpawn()
    {
        // MH - Setup tree objects
        for (int zPos = 0; zPos < GetRenderDistance_DN(); zPos++)
        {
            // MH - If the generated value is less than the spawn probability of the tree
            if (GetRandPercentageVal() < treeSpawnProbability)
            {
                // MH - Generate a tree
                (Instantiate(
                    // MH - Object
                    tree,
                    // Mh - Position
                    new Vector3
                    (
                        // MH - for the x position 
                        GetRandPosOnGround(),                       // x
                        transform.position.y + treeYOffset,         // y
                        zPos                                        // z
                    ),
                    // MH - Rotation
                    Quaternion.identity)
                    as GameObject).transform.parent = backgroundHolder.transform;
            }
        }
    }
    void BushSpawn()
    {
        // MH - Setup bush objects
        for (int zPos = 0; zPos < GetRenderDistance_DN(); zPos++)
        {
            // MH - If the generated value is less than the spawn probability of the bush
            if (GetRandPercentageVal() < bushSpawnProbability)
            {
                // MH - Place a Bush in the scene
                (Instantiate(
                    // MH - Object
                    bushes[GetRandBushType(numberOfBushes)],
                    // MH - Position
                    new Vector3
                    (
                        GetRandPosOnGround(),               // x
                        transform.position.y + bushYOffset, // y
                        zPos                                // z
                    ),
                    // MH - Rotation
                    Quaternion.identity)
                    as GameObject).transform.parent = backgroundHolder.transform;
            }
        }
    }
    void RockSpawn()
    {
        // MH - Setup rock objects
        for (int zPos = 0; zPos < GetRenderDistance_DN(); zPos++)
        {
            // MH - If the generated value is less than the spawn probability of the rock
            if (GetRandPercentageVal() < rockSpawnProbability)
            {
                // MH - Place a Rock in the scene
                (Instantiate(
                    // MH - Object
                    rocks[GetRandRockType(numberOfRocks)],
                    // MH - Position
                    new Vector3
                    (
                        GetRandPosOnGround(),
                        transform.position.y + rockYOffset,
                        zPos
                    ),
                    // MH - Rotation
                    Quaternion.identity)
                    as GameObject).transform.parent = backgroundHolder.transform;
            }
        }
    }
    // MH - Spawn the clouds we are to introduce into the scene
    void SkySpawn()
    {
        for (int zPos = cloudOutOfSight; zPos < GetRenderDistance_DN(); zPos++)
        {
            // MH - If the generated value is less than the spawn probability of the cloud
            if (GetRandPercentageVal() < cloudSpawnProbability)
            {
                // MH - Create the obstacle
                (Instantiate(
                    clouds,
                    new Vector3
                    (
                        GetRandPosInSky(),
                        40,
                        zPos
                    ),
                    Quaternion.identity)
                    as GameObject).transform.parent = skyHolder.transform;
            }
        }
    }
    #endregion

    #region Update
    void Update ()
    {
        if (!isPaused())
        {
            UpdateBackground();
        }   
    }

    // MH - These handle updating the terrain as the player moves forwards
    void UpdateBackground()
    {
        // MH - Get the current position of the player on the z-axis
        playerZPos = player.transform.position.z;

        UpdateGround();
        UpdateFlora();

        UpdateSky();
    }
    void UpdateGround()
    {
        foreach (GameObject item in groundObjects)
        {
            if (item == null) Debug.Log("Denial Ground Object Error");
            else if ((item.transform.position.z + 5.0f) < playerZPos)
            {
                item.transform.Translate
                (
                    0.0f,
                    0.0f,
                    GetRenderDistance_DN()
                );
            }
        }
    }
    void UpdateFlora()
    {
        foreach (GameObject item in floraObjects)
        {
            if (item == null) Debug.Log("Denial Flora Object Error");
            else if ((item.transform.position.z + floraOutOfSight) < playerZPos)
            {
                item.transform.Translate
                (
                    GetRandPosOnGround(),
                    0.0f,
                    GetRenderDistance_DN()
                );
            }
        }
    }
    void UpdateSky()
    {
        foreach (GameObject item in skyObjects)
        {
            if (item == null) Debug.Log("Denial Sky Object Error");
            else if ((item.transform.position.z + cloudOutOfSight) < playerZPos)
            {
                item.transform.Translate
                (
                    GetRandPosInSky(),
                    0.0f,
                    GetRenderDistance_DN()
                );
            }
        }
    }
    #endregion
    #endregion
}
