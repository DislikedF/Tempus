//this script was written by Alan Guild 
//this script controls all the in air obstacles

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Obstacles_AC : MonoBehaviour
{
    public GameObject Player;
    public GameObject UFO;
    public GameObject asteroids;
    public GameObject birdsRight;
    public GameObject birdsLeft;

    public AudioSource birdsRightChurp;
    public AudioSource birdsLeftChurp;
    public AudioSource UFOSound;

    private bool sky = false;
    private bool space = false;

    private Vector3 lastObstacle;

    int skySpawn;       //get no for how many times your spawning birds
    int spaceSpawn;     //get no for how many times your spawning space obstacles
    int spaceObject;    //decide what object should be spawned
    int spaceDir;

    // Use this for initialization
    void Start ()
    {
        //skySpawn = Random.Range(1, 5);      //get no for how many times your spawning birds
        skySpawn = 1;       //change after testing

        spaceSpawn = 1;     //change after testing
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float playerPosY = Player.transform.position.y;    //get players Y position
        float playerPosZ = Player.transform.position.z;    //get players Z position


        if (playerPosY >= 2.0f && playerPosY < 25.0f && sky == false)     //if players position between ground and transition to space clone birds
        {
            sky = true;

            for(int i = 0; i < skySpawn; i++)
            {
                int birdSide = Random.Range(1, 3);

                if (birdSide == 1)
                {
                    float rand = Random.Range(10.0f, 15.0f);

                    GameObject toRight;
                    toRight = Instantiate(birdsRight, new Vector3(-5f, playerPosY + rand, playerPosZ), Quaternion.identity);
                    toRight.gameObject.tag = "Clone";
                    toRight.transform.localScale = new Vector3(150.0f,150.0f,150.0f);
                    toRight.SetActive(true);
                    birdsRightChurp.Play();
                    toRight.GetComponent<Rigidbody>().velocity = new Vector3(1.0f, 0.0f, 0.0f);
                }
                else if(birdSide == 2)
                {
                    float rand = Random.Range(10.0f, 15.0f);

                    GameObject toLeft;
                    toLeft = Instantiate(birdsLeft, new Vector3(5f, playerPosY + rand, playerPosZ), Quaternion.identity);
                    toLeft.gameObject.tag = "Clone";
                    toLeft.transform.localScale = new Vector3(150.0f, 150.0f, 150.0f);
                    toLeft.SetActive(true);
                    birdsLeftChurp.Play();
                    toLeft.GetComponent<Rigidbody>().velocity = new Vector3(-1.0f, 0.0f, 0.0f);
                }
            }
        }
       
        if(playerPosY > 25.0f && space == false)     //if players position between transition and top 
        {
            space = true;

            for (int i = 0; i < spaceSpawn; i++)
            {
                spaceObject = Random.Range(1, 3);

                if (spaceObject == 1)
                {
                    spaceDir = Random.Range(1, 3);

                    if(spaceDir == 1)
                    {
                        float rand = Random.Range(20.0f, 30.0f);

                        GameObject UFORight;
                        UFORight = Instantiate(UFO, new Vector3(-5f, playerPosY + rand, playerPosZ), Quaternion.identity);
                        UFORight.gameObject.tag = "Clone";
                        UFORight.transform.localScale = new Vector3(200.0f, 200.0f, 200.0f);
                        UFORight.SetActive(true);
                        UFOSound.Play();
                        UFORight.GetComponent<Rigidbody>().velocity = new Vector3(1.5f, 0.0f, 0.0f);
                    }
                    else if(spaceDir == 2)
                    {
                        float rand = Random.Range(20.0f, 30.0f);

                        GameObject UFOLeft;
                        UFOLeft = Instantiate(UFO, new Vector3(5f, playerPosY + rand, playerPosZ), Quaternion.identity);
                        UFOLeft.gameObject.tag = "Clone";
                        UFOLeft.transform.localScale = new Vector3(200.0f, 200.0f, 200.0f);
                        UFOLeft.SetActive(true);
                        UFOSound.Play();
                        UFOLeft.GetComponent<Rigidbody>().velocity = new Vector3(-1.5f, 0.0f, 0.0f);
                    }
                }
                else if (spaceObject == 2)
                {
                    spaceDir = Random.Range(1, 3);

                    if (spaceDir == 1)
                    {
                        GameObject asteroidRight;
                        asteroidRight = Instantiate(asteroids, new Vector3(-5f, playerPosY + 20.0f, playerPosZ), Quaternion.identity);
                        asteroidRight.gameObject.tag = "Clone";
                        asteroidRight.transform.localScale = new Vector3(200.0f, 200.0f, 200.0f);
                        asteroidRight.SetActive(true);
                        asteroidRight.GetComponent<Rigidbody>().velocity = new Vector3(1.5f, 0.0f, 0.0f);
                    }
                    else if (spaceDir == 2)
                    {
                        GameObject asteroidsLeft;
                        asteroidsLeft = Instantiate(asteroids, new Vector3(5f, playerPosY + 20.0f, playerPosZ), Quaternion.identity);
                        asteroidsLeft.gameObject.tag = "Clone";
                        asteroidsLeft.transform.localScale = new Vector3(200.0f, 200.0f, 200.0f);
                        asteroidsLeft.SetActive(true);
                        asteroidsLeft.GetComponent<Rigidbody>().velocity = new Vector3(-1.5f, 0.0f, 0.0f);
                    }
                }
            }
        }
	}
}
