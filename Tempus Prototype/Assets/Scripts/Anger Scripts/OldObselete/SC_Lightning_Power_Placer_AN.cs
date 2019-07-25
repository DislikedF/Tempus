using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Lightning_Power_Placer_AN : MonoBehaviour {

    public UnityEngine.GameObject ground;
    public UnityEngine.GameObject player;
    public UnityEngine.GameObject powerup;
    public float minPlaceDist;
    public float maxPlaceDist;
    public int strikeTimer;
    public int strikeLength;

    private Vector3 obstaclePos;
    private int strikeCounter = 0;

    void RandomisePos()
    {
        Renderer groundRenderer = ground.GetComponent<Renderer>();
        obstaclePos = new Vector3(Random.Range((ground.transform.position.x - (groundRenderer.bounds.size.x/2)), (ground.transform.position.x + (groundRenderer.bounds.size.x / 2))),
                                 ground.transform.position.y + 1.0f,
                                 Random.Range(player.transform.position.z + minPlaceDist, (player.transform.position.z + maxPlaceDist)));

    }

    // Use this for initialization
    void Start () {
        powerup.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        // If it's time for a lightning strike
		if(strikeCounter == strikeTimer * 60)
        {
            // Call the randomise function and set the position of the lightning bolt
            RandomisePos();
            powerup.transform.position = obstaclePos;
            // Make the lightning bolt active
            powerup.SetActive(true);
        }
        // If the lightning strike has reached the end of its lifespan
        if(strikeCounter == ((strikeTimer + strikeLength) * 60))
        {
            // Get rid of it
            powerup.SetActive(false);
            // reset the timer
            strikeCounter = 0;
        }
        strikeCounter++;
	}
}
