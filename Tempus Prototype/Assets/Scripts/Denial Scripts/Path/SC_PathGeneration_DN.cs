using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PathGeneration_DN : SC_Shared_DN
{
    #region Variables
    #region Game Objects
    // MH - Path Game Objects
    public GameObject[] path = new GameObject[6];
    public GameObject pathHolder;
    private GameObject[] pathObjects;
    // MH - Obstacle Game Objects
    public GameObject obstacleHolder;
    public GameObject[] bushes = new GameObject[8];
    public GameObject[] rocks = new GameObject[3];
    private GameObject[] pathObstacles;
    // MH - Player Game Objects
    public GameObject player;
    // MH - Camera Game Objects
    public GameObject cam;
    #endregion

    #region Offsets
    // MH - Offset Variables
    float playerZPos;
    float offsetPathBreak;
    // MH - Obstacle specific offsets
    public int obstacleFreeZone = 10;
    public float rockXOffset = 0.1f;
    public float rockYOffset = 0.7f;
    #endregion

    #region Obstacle RNG
    int numberOfBushes;
    int numberOfRocks;
    public float obstacleSpawnProbability;
    enum obstacleType { bush, tree, rock };
    obstacleType selectedObstacleType;
    int selectedObstacle;
    int selectedPath;
    #endregion
    #endregion

    #region Functions
    #region Start
    void Start ()
    {
        // MH - This is an integral part of the UI setup
        SetupUI();
  
        SetupVariables();

        GeneratePath();

        GenerateObstacles();
    }

    void SetupUI()
    {
        //////////////////////////////////////////////////////////
        if (Time.timeScale != 1)     // Don't Delete            //
        {                                                       //
            Time.timeScale = 1;                                 //
        }                                                       //
        //////////////////////////////////////////////////////////
    }
    void SetupVariables()
    {
        // MH -Instantiate the obstacle type
        selectedObstacleType = obstacleType.bush;

        // MH - Get these game object array sizes for later calculations
        numberOfBushes = bushes.Length;
        numberOfRocks = rocks.Length;

        // MH - Instantiate the offset for the path breaking
        offsetPathBreak = 0.0f;
    }

    void GeneratePath()
    {
        for (int i = 0; i < GetRenderDistance_DN(); i++)
        {
            for (float width = 0; width < GetNumberOfPaths_DN(); width++)
            {
                (Instantiate(
                    path[(int)width],
                    new Vector3(width, 0.0f, (int)i),
                    Quaternion.Euler(0.0f, 90.0f, 0.0f))
                    as GameObject).transform.parent = pathHolder.transform;
            }
        }

        // MH - Create an array of path objects
        pathObjects = GameObject.FindGameObjectsWithTag("Path_DN");
    }

    // MH - These functions handle the initial obstacle generation
    void GenerateObstacles()
    {
        // MH - Create the number of obstacles we will be using for the game
        // MH - This equation means we will have obstacles along the entire path
        for (int i = obstacleFreeZone; i < GetRenderDistance_DN() + obstacleFreeZone; i++)
        {
            // MH - Determine which kind of obstacle will be spawned for this iteration
            selectedObstacleType = (obstacleType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(obstacleType)).Length);
            // MH - Spawn the selected obstacle
            switch (selectedObstacleType)
            {
                case obstacleType.bush:
                    BushSpawn(i);
                    break;
                case obstacleType.rock:
                    RockSpawn(i);
                    break;
            }
        }

        // MH - Create an array of the obstacles in the scene
        pathObstacles = GameObject.FindGameObjectsWithTag("Obstacle_DN");

        foreach (GameObject item in pathObstacles)
        {
            // MH - Determine if the object is active or inactive according to our spawn probablilty
            // MH - 0 to 100 gives us a nice mental picture of what probability to select
            // MH - If the obstacle is NOT supposed to be spawned yet:
            if (!(UnityEngine.Random.Range(0.0f, 100.0f) < obstacleSpawnProbability))
            {
                // MH - Set it to be invisible and uninteractable with the player:

                // MH - This is a necessary check, if there are other obstacles with different
                //      colliders attached then another check can be simply added, e.g. Box Colliders:
                //      if (item.GetComponent<BoxCollider>() == true)
                //      {
                //          item.GetComponent<BoxCollider>().enabled = false;
                //      }
                if (item.GetComponent<SphereCollider>() == true)
                {
                    item.GetComponent<SphereCollider>().enabled = false;
                }

                // MH - Set the obstacle to be invisible:
                item.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
    void BushSpawn(int i)
    {
        // MH - Seed the pRNG (Pseudo Random Number Generator)
        // MH - Don't use a float here as data is entered into an array
        // MH - This outputs values from (0) to (the obstacle array size - 1)
        selectedObstacle = UnityEngine.Random.Range(0, numberOfBushes);

        // MH - Selects which path the obstacle will be placed on
        // MH - This will output values from a range of 0 -> 3 (4 Values)
        // MH - this corresponds with the x-positions of the paths in the scene
        selectedPath = UnityEngine.Random.Range(0, GetNumberOfPaths_DN());

        // MH - Create the obstacle
        (Instantiate(
            bushes[selectedObstacle],
            new Vector3(selectedPath, 1.0f, i),
            Quaternion.identity)
            as GameObject).transform.parent = obstacleHolder.transform;
    }
    void RockSpawn(int i)
    {
        // MH - Seed the pRNG (Pseudo Random Number Generator)
        // MH - Don't use a float here as data is entered into an array
        // MH - This outputs values from (0) to (the obstacle array size - 1)
        selectedObstacle = UnityEngine.Random.Range(0, numberOfRocks);

        // MH - Selects which path the obstacle will be placed on
        // MH - This will output values from a range of 0 -> 3 (4 Values)
        // MH - this corresponds with the x-positions of the paths in the scene
        selectedPath = UnityEngine.Random.Range(0, GetNumberOfPaths_DN());

        // MH - Create the obstacle
        (Instantiate(
            rocks[selectedObstacle],
            new Vector3(selectedPath + rockXOffset, transform.position.y + rockYOffset, i),
            Quaternion.identity)
            as GameObject).transform.parent = obstacleHolder.transform;
    }
    #endregion

    #region Update
    void FixedUpdate()
    {
        if (!isPaused())
        {
            // MH - Take the current transform in the z-axis for the player
            playerZPos = player.transform.position.z;

            // MH - Increase the probability of obstacles spawning over time
            if (obstacleSpawnProbability < 70)
            {
                obstacleSpawnProbability += 0.45f * Time.deltaTime;
            }

            // MH - This will set the path to break every 6 seconds
            // MH - Could alternatively use the players position
            offsetPathBreak += Time.deltaTime;
            if (offsetPathBreak > 5.0f)
            {
                cam.AddComponent<SC_CamShake_DN>();
                PlayAudio();
                offsetPathBreak = 0.0f;
                RandomPathBreak();
            }

            // MH - Update every third of a second
            if (Time.frameCount % 20 == 0)
            {
                UpdateObstacles();
                UpdatePath();
            } 
        }
    }

    void RandomPathBreak()
    {
        float tempRandNum = Random.Range(0.0f, 100.0f);

        // MH - Decide which side of the path to break
        if (tempRandNum < 50.0f)
        {
            // MH - Breaking from the Left side of the path
            int pathsToBreak = Random.Range(1, 4);
            for (int pathID = 0; pathID < pathsToBreak; pathID++)
            {
                //BreakPath(pathID, Random.Range(40, 60), Random.Range(80, 110));
                BreakPath(pathID, Random.Range(10, 30), Random.Range(50, 80));
            }
        }
        else
        {
            int pathsToBreak = Random.Range(1, 4);
            for (int pathID = 5; pathID > pathsToBreak; pathID--)
            {
                //BreakPath(pathID, Random.Range(40, 60), Random.Range(80, 110));
                BreakPath(pathID, Random.Range(10, 30), Random.Range(50, 80));
            }
        }
    }
    void BreakPath(int pathID, int startPos, int endPos)
    {
        // MH - A check to identify that a correct value has been entered 0 - 5 (or 6 Values/Paths)
        if ((pathID >= 0) && (pathID <= (GetNumberOfPaths_DN() - 1)))
        {
            foreach (GameObject item in pathObjects)
            {
                // The first two checks are to make sure we dont exclude any path objects in the x bounds
                // The third is to limit the length of the path being broken off.
                if ((item.transform.position.x < ((float)pathID + 0.3f))
                    && (item.transform.position.x > ((float)pathID - 0.3f))
                    && (item.transform.position.z < (endPos + playerZPos))
                    && (item.transform.position.z > (startPos + playerZPos)))
                {
                    // MH - Activate the path breaking component
                    item.GetComponent<SC_PathBreak_DN>().enabled = true;
                }
            }
            foreach (GameObject item in pathObstacles)
            {
                // The first two checks are to make sure we dont exclude any path objects in the x bounds
                // The third is to limit the length of the path being broken off.
                if ((item.transform.position.x < ((float)pathID + 0.3f))
                    && (item.transform.position.x > ((float)pathID - 0.3f))
                    && (item.transform.position.z < (endPos + playerZPos))
                    && (item.transform.position.z > (startPos + playerZPos)))
                {
                    // MH - Activate the path breaking component
                    item.GetComponent<SC_PathBreak_DN>().enabled = true;
                }
            }
        }
        else
        {
            Debug.Log("Break path is having incorrect values entered");
        }
    }

    void UpdateObstacles()
    {
        foreach (GameObject item in pathObstacles)
        {
            // MH - Debugging, an easy error to make so best catch it when it does ahaha
            if (item == null) Debug.Log("Denial Obstacle Error");
            else if ((item.transform.position.z + 5.0f) < playerZPos)
            {
                // MH - Set the new position of the Obstacle
                // MH - 1. Give the obstacle a new position on the x-axis
                transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
                // MH - 2. Translate the object ahead of the player
                item.transform.Translate(0.0f, 0.0f, GetRenderDistance_DN());

                // MH - If the obstacle is NOT to be spawned:
                if (!(UnityEngine.Random.Range(0.0f, 100.0f) < obstacleSpawnProbability))
                {
                    // MH - Disable its collider:
                    if (item.GetComponent<SphereCollider>() == true)
                    {
                        item.GetComponent<SphereCollider>().enabled = false;
                    }

                    // MH - Set the obstacle to be invisible:
                    item.GetComponent<MeshRenderer>().enabled = false;
                }
                else // MH - If the obstacle IS to be spawned:
                {
                    // MH - Enable its collider:
                    if (item.GetComponent<SphereCollider>() == true)
                    {
                        item.GetComponent<SphereCollider>().enabled = true;
                    }

                    // MH - Set the obstacle to be visible:
                    item.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
    }

    void UpdatePath()
    {
        foreach (GameObject item in pathObjects)
        {
            // MH - Debugging, an easy error to make so best catch it when it does ahaha
            if (item == null) Debug.Log("Denial Path Object Error");
            else if ((item.transform.position.z + 5.0f) < playerZPos)
            {
                item.transform.Translate(-GetRenderDistance_DN(), 0.0f, 0.0f);
            }
        }
    }

    void PlayAudio()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        audio.Play(44100);
    }
    #endregion
    #endregion
}