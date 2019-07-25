using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BigPowerup_AN : MonoBehaviour {

    private IEnumerator powerupEffectCoroutine;

    public GameObject ground;

    public int duration;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            // Start the powerup coroutine
            powerupEffectCoroutine = GetBig(col.gameObject);
            StartCoroutine(powerupEffectCoroutine);
        }
    }

    IEnumerator GetBig(GameObject player)
    {
        // Double the player's size
        player.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);

        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        float oldY = player.transform.position.y;
        player.transform.position = new Vector3(player.transform.position.x,
                                        (player.GetComponent<Renderer>().bounds.size.y / 2)
                                        + (ground.GetComponent<Renderer>().bounds.size.y / 2) - 0.1f,
                                        player.transform.position.z);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;

        // Wait for the powerup duration
        yield return new WaitForSecondsRealtime(duration);

        // Return the player to their original size;
        player.transform.localScale -= new Vector3(1.0f, 1.0f, 1.0f);
        player.transform.position = new Vector3(player.transform.position.x, oldY, player.transform.position.z);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;


        yield return null;
    }
}
