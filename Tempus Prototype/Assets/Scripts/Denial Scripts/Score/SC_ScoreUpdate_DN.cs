 using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SC_ScoreUpdate_DN : SC_Shared_DN
{
    // MH - Define necessary variables to create a player score value
	public GameObject player;
    public Text Score;
    public TextMesh endScore;

    // MH - Update the players score
    void Update ()
	{
        if (!isPaused())
        {
            AddScore();
        }
	}
		
    // MH - This is made public so it can be accessed from other scripts to add to the score of the player
	public void AddScore ()
	{
            //PlayerPrefs.SetInt("DenialHighScore", (int)player.transform.position.z);
            Score.text = "Score: " + (int)player.transform.position.z;// PlayerPrefs.GetInt("DenialHighScore");
    }
}