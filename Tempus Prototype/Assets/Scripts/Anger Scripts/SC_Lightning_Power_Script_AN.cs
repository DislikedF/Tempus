using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Lightning_Power_Script_AN : MonoBehaviour {

    public float speedMult = 1.5f;
    public GameObject firePart;

    private IEnumerator particleCoroutine;
    private bool isOnFire = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            // Get the player's rigidbody
            Rigidbody rb = col.GetComponent<Rigidbody>();
            // Speed the player up
            rb.AddForce(new Vector3(rb.velocity.x * speedMult, 0.0f, rb.velocity.z * speedMult), ForceMode.Impulse);

            // Check if the player is already on fire
            if (!isOnFire)
            {
                // Start the fire coroutine
                StartCoroutine("SetFire");
            }
        }
    }

    IEnumerator SetFire()
    {
        // Set the player on fire, and let the script know
        isOnFire = true;
        firePart.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        // Turn the fire particles off
        firePart.SetActive(false);
        isOnFire = false;
        yield return null;
    }
}
