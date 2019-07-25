using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Procedural_Generation_AN : MonoBehaviour
{
    //Holds the different colors for the path
    //public UnityEngine.GameObject wall;
    public UnityEngine.GameObject ground;
    public UnityEngine.GameObject leftEdge;
    public UnityEngine.GameObject rightEdge;
    public UnityEngine.GameObject cloud;
    public UnityEngine.GameObject player;
    public UnityEngine.GameObject rock;
    public UnityEngine.GameObject tree;
    public GameObject barn;
    public GameObject sign1;
    public GameObject sign2;
    public GameObject sign3_1;
    public GameObject sign3_2;
    public GameObject sign3_3;
    public GameObject tent1;
    public GameObject tent2;
    public GameObject tent3;
    public GameObject grill;
    public GameObject basket;
    public GameObject bench;
    public GameObject blanket;

    public UnityEngine.GameObject bigPowerup;
    public UnityEngine.GameObject lightningPowerup;
    public UnityEngine.GameObject lightningPart;
    public float minPlaceDist;
    public float maxPlaceDist;
    public int strikeTimer;
    public int strikeLength;
    public int preStrike;
    public int bigPowerLife;

    private bool isStriking = false;
    private bool hasBigSpawned = false;

    const int COUNTER_START = -20;
    int counter = COUNTER_START;
    int lastCounter = 0;
    float playerZPos;

    public int obstacleSep;
    private int obstacleCounter;
    private float lastGroundZ;
    private Vector3 obstaclePos;
    private Vector3 strikePos;
    private Vector3 bigPos;
    private Vector3 backgroundPos;

    private const int BATCH_SIZE = 300;
    private GameObject[] rockBatch;
    private GameObject[] treeBatch;
    private GameObject[] tentBatch;
    private GameObject[] signBatch;
    private GameObject[] barnBatch;
    private GameObject[] grillBatch;
    private GameObject[] benchBatch;
    private GameObject[] blanketBatch;
    private GameObject[] basketBatch;

    private int lastRock = 0;
    private int lastTree = 0;
    private int lastTent = 0;
    private int lastSign = 0;
    private int lastBarn = 0;
    private int lastBench = 0;
    private int lastBasket = 0;
    private int lastBlanket = 0;
    private int lastGrill = 0;

	private static int INIT_SPAWN_DIST = 500;

    private Queue<Vector3> usedPos = new Queue<Vector3>();
    private static int POS_HISTORY_MAX = 20;

    // Use this for initialization
    void Start()
    {
        if(Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }

        rockBatch = new GameObject[BATCH_SIZE];
        treeBatch = new GameObject[BATCH_SIZE];
        tentBatch = new GameObject[BATCH_SIZE];
        signBatch = new GameObject[BATCH_SIZE];
        barnBatch = new GameObject[BATCH_SIZE];
        basketBatch = new GameObject[BATCH_SIZE];
        benchBatch = new GameObject[BATCH_SIZE];
        grillBatch = new GameObject[BATCH_SIZE];
        blanketBatch = new GameObject[BATCH_SIZE];

        for (int i = 0; i < BATCH_SIZE; i++)
        {
            rockBatch[i] = Instantiate(rock);
            rockBatch[i].transform.localScale = new Vector3(Random.Range(1.0f, 2.0f), Random.Range(1.0f, 2.0f), Random.Range(1.0f, 2.0f));
            rockBatch[i].transform.Rotate(new Vector3(0.0f, Random.Range(0.0f, 360.0f), 0.0f));

            treeBatch[i] = Instantiate(tree);
            treeBatch[i].transform.localScale = new Vector3(Random.Range(70.0f, 72.0f), Random.Range(70.0f, 75.0f), Random.Range(70.0f, 72.0f));
            treeBatch[i].transform.Rotate(new Vector3(0.0f, Random.Range(0.0f, 360.0f), 0.0f));

            switch ((int)Random.Range(0.0f, 2.9f))
            {
                case 0:
                    tentBatch[i] = Instantiate(tent1);
                    break;

                case 1:
                    tentBatch[i] = Instantiate(tent2);
                    break;

                case 2:
                    tentBatch[i] = Instantiate(tent3);
                    break;

                default:
                    break;
            }

            tentBatch[i].transform.Rotate(new Vector3(0.0f, Random.Range(0.0f, 360.0f), 0.0f));

            switch ((int)Random.Range(0.0f, 4.9f))
            {
                case 0:
                    signBatch[i] = Instantiate(sign1);
                    break;

                case 1:
                    signBatch[i] = Instantiate(sign2);
                    break;

                case 2:
                    signBatch[i] = Instantiate(sign3_1);
                    break;

                case 3:
                    signBatch[i] = Instantiate(sign3_2);
                    break;

                case 4:
                    signBatch[i] = Instantiate(sign3_3);
                    break;

                default:
                    break;
            }

            signBatch[i].transform.Rotate(new Vector3(0.0f, Random.Range(0.0f, 360.0f), 0.0f));

            barnBatch[i] = Instantiate(barn);
            barnBatch[i].transform.Rotate(new Vector3(0.0f, Random.Range(0.0f, 360.0f), 0.0f));

            basketBatch[i] = Instantiate(basket);
            basketBatch[i].transform.Rotate(new Vector3(0.0f, Random.Range(0.0f, 360.0f), 0.0f));

            benchBatch[i] = Instantiate(bench);
            benchBatch[i].transform.Rotate(new Vector3(0.0f, Random.Range(0.0f, 360.0f), 0.0f));

            grillBatch[i] = Instantiate(grill);
            grillBatch[i].transform.Rotate(new Vector3(0.0f, Random.Range(0.0f, 360.0f), 0.0f));

            blanketBatch[i] = Instantiate(blanket);
            blanketBatch[i].transform.Rotate(new Vector3(0.0f, Random.Range(0.0f, 360.0f), 0.0f));
        }

        obstacleCounter = obstacleSep;
        lightningPowerup.SetActive(false);



        for (int i = 0; i < INIT_SPAWN_DIST; i++)
        {
            //DrawPath();
            StartCoroutine("DrawPath");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            //DrawPath();
            //StartCoroutine("DrawPath");

            if (!isStriking)
            {
                StartCoroutine("LightningStrike");
            }
            
            if(!hasBigSpawned)
            {
                StartCoroutine("PlaceBigPower");
            }

            // Clean up path
            if (counter % 2 == 0) CleanPath();
        }

        //Debug.Log(counter);
        //Debug.Log(usedPos.Count);
    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 1)
        {
            StartCoroutine("DrawPath");
        }
    }

    IEnumerator DrawPath()
    {
        playerZPos = player.transform.position.z;

        //This sets the limit that the path can draw. Set this to be just outside the camera view
        if (counter < playerZPos + 500)
        {
            // Places the ground clones next to eachother with no overlap
            UnityEngine.GameObject groundClone;
            if (counter == COUNTER_START)
            {
                groundClone = Instantiate(ground, new Vector3(0.0f, 0.0f, counter), Quaternion.identity);
            } else
            {
                groundClone = Instantiate(ground, new Vector3(0.0f, 0.0f, lastGroundZ + ground.GetComponent<Renderer>().bounds.size.z), Quaternion.identity);
            }
            groundClone.SetActive(true);
            groundClone.gameObject.tag = "Ground_Clone_AN";
            lastGroundZ = groundClone.transform.position.z;

            if (counter % 2 == 0)
            {
                UnityEngine.GameObject cloudClone;
                cloudClone = Instantiate(cloud, new Vector3(Random.Range(-60.0f, 60.0f), Random.Range(15.0f, 20.0f), counter), Quaternion.identity);
                cloudClone.SetActive(true);
            }


            // Place Obstacles, as long as they are not within eachother
            if (counter != lastCounter && counter > 20)
            {
                // Draw the background objects
                if (counter % 2 == 0)
                {
                    // Randomly chooses an obstacle type, weighted towards rocks and trees
                    switch ((int)Random.Range(0.0f, 16.9f))
                    {
                        //case 0:
                        //    RandomiseBackground(tentBatch[lastTent].GetComponent<Collider>());
                        //    tentBatch[lastTent].transform.position = backgroundPos;
                        //    tentBatch[lastTent].SetActive(true);
                        //    lastTent++;
                        //    if (lastTent > BATCH_SIZE - 1)
                        //    {
                        //        lastTent = 0;
                        //    }
                        //    break;

                        //case 1:
                        //    RandomiseBackground(signBatch[lastSign].GetComponent<Collider>());
                        //    signBatch[lastSign].transform.position = backgroundPos;
                        //    signBatch[lastSign].SetActive(true);
                        //    lastSign++;
                        //    if (lastSign > BATCH_SIZE - 1)
                        //    {
                        //        lastSign = 0;
                        //    }
                        //    break;

                        //case 2:
                        //    RandomiseBackground(barnBatch[lastBarn].GetComponent<Collider>());
                        //    barnBatch[lastBarn].transform.position = backgroundPos;
                        //    barnBatch[lastBarn].SetActive(true);
                        //    lastBarn++;
                        //    if (lastBarn > BATCH_SIZE - 1)
                        //    {
                        //        lastBarn = 0;
                        //    }
                        //    break;

                        //case 3:
                        //    RandomiseBackground(basketBatch[lastBasket].GetComponent<Collider>());
                        //    basketBatch[lastBasket].transform.position = backgroundPos;
                        //    basketBatch[lastBasket].SetActive(true);
                        //    lastBasket++;
                        //    if (lastBasket > BATCH_SIZE - 1)
                        //    {
                        //        lastBasket = 0;
                        //    }
                        //    break;

                        //case 4:
                        //    RandomiseBackground(blanketBatch[lastBlanket].GetComponent<Collider>());
                        //    blanketBatch[lastBlanket].transform.position = backgroundPos;
                        //    blanketBatch[lastBlanket].SetActive(true);
                        //    lastBlanket++;
                        //    if (lastBlanket > BATCH_SIZE - 1)
                        //    {
                        //        lastBlanket = 0;
                        //    }
                        //    break;

                        //case 5:
                        //    RandomiseBackground(benchBatch[lastBench].GetComponent<Collider>());
                        //    benchBatch[lastBench].transform.position = backgroundPos;
                        //    benchBatch[lastBench].SetActive(true);
                        //    lastBench++;
                        //    if (lastBench > BATCH_SIZE - 1)
                        //    {
                        //        lastBench = 0;
                        //    }
                        //    break;

                        //case 6:
                        //    RandomiseBackground(grillBatch[lastGrill].GetComponent<Collider>());
                        //    grillBatch[lastGrill].transform.position = backgroundPos;
                        //    grillBatch[lastGrill].SetActive(true);
                        //    lastGrill++;
                        //    if (lastGrill > BATCH_SIZE - 1)
                        //    {
                        //        lastGrill = 0;
                        //    }
                        //    break;

                        default:
                            if ((int)Random.Range(0.0f, 1.9f) == 0)
                            {
                                RandomiseBackground(rockBatch[lastRock].GetComponent<Collider>());
                                rockBatch[lastRock].transform.position = backgroundPos;
                                rockBatch[lastRock].SetActive(true);
                                lastRock++;
                                if (lastRock > BATCH_SIZE - 1)
                                {
                                    lastRock = 0;
                                }
                            }
                            else
                            {
                                RandomiseBackground(treeBatch[lastTree].GetComponent<Collider>());
                                treeBatch[lastTree].transform.position = backgroundPos;
                                treeBatch[lastTree].SetActive(true);
                                lastTree++;
                                if (lastTree > BATCH_SIZE - 1)
                                {
                                    lastTree = 0;
                                }
                            }
                            break;
                    }

                }

                // Places obstacles
                if (counter % obstacleCounter == 0)
                {
                    // Chooses randomly between obstacle types, weighted towards rocks and trees
                    switch ((int)Random.Range(0.0f, 16.9f))
                    {
                        case 0:
                            RandomisePos(tentBatch[lastTent].GetComponent<Collider>());
                            tentBatch[lastTent].transform.position = obstaclePos;
                            tentBatch[lastTent].SetActive(true);
                            lastTent++;
                            if (lastTent > BATCH_SIZE - 1)
                            {
                                lastTent = 0;
                            }
                            break;

                        case 1:
                            RandomisePos(signBatch[lastSign].GetComponent<Collider>());
                            signBatch[lastSign].transform.position = obstaclePos;
                            signBatch[lastSign].SetActive(true);
                            lastSign++;
                            if (lastSign > BATCH_SIZE - 1)
                            {
                                lastSign = 0;
                            }
                            break;

                        case 2:
                            RandomisePos(barnBatch[lastBarn].GetComponent<Collider>());
                            barnBatch[lastBarn].transform.position = obstaclePos;
                            barnBatch[lastBarn].SetActive(true);
                            lastBarn++;
                            if (lastBarn > BATCH_SIZE - 1)
                            {
                                lastBarn = 0;
                            }
                            break;

                        case 3:
                            RandomisePos(basketBatch[lastBasket].GetComponent<Collider>());
                            basketBatch[lastBasket].transform.position = obstaclePos;
                            basketBatch[lastBasket].SetActive(true);
                            lastBasket++;
                            if (lastBasket > BATCH_SIZE - 1)
                            {
                                lastBasket = 0;
                            }
                            break;

                        case 4:
                            RandomisePos(blanketBatch[lastBlanket].GetComponent<Collider>());
                            blanketBatch[lastBlanket].transform.position = obstaclePos;
                            blanketBatch[lastBlanket].SetActive(true);
                            lastBlanket++;
                            if (lastBlanket > BATCH_SIZE - 1)
                            {
                                lastBlanket = 0;
                            }
                            break;

                        case 5:
                            RandomisePos(benchBatch[lastBench].GetComponent<Collider>());
                            benchBatch[lastBench].transform.position = obstaclePos;
                            benchBatch[lastBench].SetActive(true);
                            lastBench++;
                            if (lastBench > BATCH_SIZE - 1)
                            {
                                lastBench = 0;
                            }
                            break;

                        case 6:
                            RandomisePos(grillBatch[lastGrill].GetComponent<Collider>());
                            grillBatch[lastGrill].transform.position = obstaclePos;
                            grillBatch[lastGrill].SetActive(true);
                            lastGrill++;
                            if (lastGrill > BATCH_SIZE - 1)
                            {
                                lastGrill = 0;
                            }
                            break;

                        default:
                            if ((int)Random.Range(0.0f, 1.9f) == 0)
                            {
                                RandomisePos(rockBatch[lastRock].GetComponent<Collider>());
                                rockBatch[lastRock].transform.position = obstaclePos;
                                rockBatch[lastRock].SetActive(true);
                                lastRock++;
                                if (lastRock > BATCH_SIZE - 1)
                                {
                                    lastRock = 0;
                                }
                            }
                            else
                            {
                                RandomisePos(treeBatch[lastTree].GetComponent<Collider>());
                                treeBatch[lastTree].transform.position = obstaclePos;
                                treeBatch[lastTree].SetActive(true);
                                lastTree++;
                                if (lastTree > BATCH_SIZE - 1)
                                {
                                    lastTree = 0;
                                }
                            }
                            break;
                    }
                    // Change when the next obstacle will be spawned, so as to make it seem less patterned
                    //obstacleCounter = (int)Random.Range(obstacleSep / (2 / counter + 1), obstacleSep);
                }
                lastCounter = counter;
            }

            counter++;
        }
        yield return null;
    }

    // Destroys the path behind the player
    void CleanPath()
    {
        UnityEngine.GameObject[] ground = UnityEngine.GameObject.FindGameObjectsWithTag("Ground_Clone_AN");
        UnityEngine.GameObject[] obstacles = UnityEngine.GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (UnityEngine.GameObject Block in ground)
        {
            if ((Block.transform.position.z + 50) < playerZPos)
            {
                UnityEngine.GameObject holder = Block;
                Destroy(holder, 1);
            }
        }

        foreach(UnityEngine.GameObject obs in obstacles)
        {
            if ((obs.transform.position.z + 50) < playerZPos)
            {
                obs.SetActive(false);
            }
        }

        ground = new UnityEngine.GameObject[0];
        obstacles = new UnityEngine.GameObject[0];
    }

    // Randomise obstacle positions
    void RandomisePos(Collider obstacleCollider)
    {

        Renderer groundRenderer = ground.GetComponent<Renderer>();
        Renderer playerRenderer = player.GetComponent<Renderer>();

        // Repeat randomisation until it's an unused position
        do
        {
            // Random location ensures there is space for the player to move past
            obstaclePos = new Vector3(Random.Range((ground.transform.position.x - (groundRenderer.bounds.size.x / 2) + playerRenderer.bounds.size.x), (ground.transform.position.x + (groundRenderer.bounds.size.x / 2) - playerRenderer.bounds.size.x)),
                                     (groundRenderer.bounds.size.y) - 1, counter);
        } while (usedPos.Contains(obstaclePos));

        // Add this new position to the list of used positions
        usedPos.Enqueue(obstaclePos);

        // Get rid of the oldest position if over list maximum
        if(usedPos.Count > POS_HISTORY_MAX)
        {
            usedPos.Dequeue();
        }
    }

    // Populate the background with more obstacles than the path
    void RandomiseBackground(Collider obstacleCollider)
    {
        Renderer edgeRenderer;
        if((int)Random.Range(1.0f, 2.9f) == 1)
        {
            edgeRenderer = leftEdge.GetComponent<Renderer>();
            backgroundPos = new Vector3(Random.Range((leftEdge.transform.position.x - (edgeRenderer.bounds.size.x / 2)), (leftEdge.transform.position.x + (edgeRenderer.bounds.size.x / 2))), 
                                        (edgeRenderer.bounds.size.y) - 1, counter);
        }
        else
        {
            edgeRenderer = rightEdge.GetComponent<Renderer>();
            backgroundPos = new Vector3(Random.Range((rightEdge.transform.position.x - (edgeRenderer.bounds.size.x / 2)), (rightEdge.transform.position.x + (edgeRenderer.bounds.size.x / 2))),
                                        (edgeRenderer.bounds.size.y) - 1, counter);
        }
    }

    IEnumerator LightningStrike()
    {
        isStriking = true;
        Renderer groundRenderer = ground.GetComponent<Renderer>();


        yield return new WaitForSecondsRealtime(strikeTimer);


        // Randomise the position of the lightning bolt, ensuring it doesn't overlap an obstacle
        do
        {
            strikePos = new Vector3(Random.Range((ground.transform.position.x - (groundRenderer.bounds.size.x / 2)), (ground.transform.position.x + (groundRenderer.bounds.size.x / 2))),
                                      ground.transform.position.y + 1.0f,
                                      Random.Range(player.transform.position.z + minPlaceDist, (player.transform.position.z + maxPlaceDist)));
        } while (usedPos.Contains(strikePos));
        lightningPowerup.transform.position = strikePos;
        lightningPart.transform.position = strikePos;

        // Start the particle effects
        lightningPart.SetActive(true);
        yield return new WaitForSecondsRealtime(preStrike);
        lightningPart.SetActive(false);
        // Make the lightning bolt active
        lightningPowerup.SetActive(true);
        IEnumerator lightningFlash = Flash(lightningPowerup);
        StartCoroutine(lightningFlash);
        // If the lightning strike has reached the end of its lifespan
        yield return new WaitForSecondsRealtime(strikeLength);
    
            // Get rid of it
        lightningPowerup.SetActive(false);

        isStriking = false;

        yield return null;
    }

    // Handles placing the enlarge powerup
    IEnumerator PlaceBigPower()
    {
        // Keep track of if there is already a big powerup active
        hasBigSpawned = true;
        // Get the ground renderer
        Renderer groundRenderer = ground.GetComponent<Renderer>();
        // Randomise the powerup position until it is in an unused position
        do
        {
            bigPos = new Vector3(Random.Range((ground.transform.position.x - (groundRenderer.bounds.size.x / 2)), (ground.transform.position.x + (groundRenderer.bounds.size.x / 2))),
                                      ground.transform.position.y + 1.0f,
                                      Random.Range(player.transform.position.z + minPlaceDist, (player.transform.position.z + maxPlaceDist)));
        } while (usedPos.Contains(bigPos));
        bigPowerup.transform.position = bigPos;
        // Make the powerup active
        bigPowerup.SetActive(true);
        // Wait the powerup's lifetime
        yield return new WaitForSecondsRealtime(bigPowerLife);
        // Deactivate the powerup
        bigPowerup.SetActive(false);
        // Notify the script there is no longer a powerup
        hasBigSpawned = false;

        yield return null;
    }

    // Makes the lightning flash
    IEnumerator Flash(GameObject obj)
    {
        for (int i = 0; i < strikeLength / 0.2; i++)
        {
            obj.GetComponent<Renderer>().enabled = !obj.GetComponent<Renderer>().enabled;
            yield return new WaitForSecondsRealtime(0.2f);
        }
        obj.GetComponent<Renderer>().enabled = true;
        yield return null;
    }
}
