using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_Score_01_BA : MonoBehaviour
{
    //Ryan Fearon

    public Text score;
    private int scoreUpdate;
    private int scoreInt;
    
	// Use this for initialization
	void Start ()
    {
        scoreInt = 0;
        score.text = "Score:" + scoreInt.ToString();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Update and show score
   public void ScoreUpdate()
    {
        scoreUpdate = scoreInt;
        scoreUpdate = scoreUpdate + 1;
        scoreInt = scoreUpdate;
        PlayerPrefs.SetInt("BargainingCurrentScore", scoreInt);
        score.text = "Score:" + scoreUpdate.ToString();
    }

}
