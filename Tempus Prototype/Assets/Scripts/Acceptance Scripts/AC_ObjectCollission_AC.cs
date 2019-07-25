//this script was written by Alan Guild
//this script controls game ending collissions

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_ObjectCollission_AC : MonoBehaviour
{
    public GameObject EndScreen;

    public AudioSource constantFlame;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	private void OnCollisionEnter(Collision collided)
    {
		if(collided.gameObject.name == "HotAirBaloon_AC")
        {
            constantFlame.Stop();

            //play Popping end animation
            EndScreen.SetActive(true);
            Time.timeScale = 0;
        }

    }
}
