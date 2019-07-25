using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ObstacleCollision_DN : SC_Shared_DN
{
    SC_Shared_DN generalScript;        //ryan

    void OnTriggerEnter(Collider other)
    {
       GameObject general = GameObject.FindGameObjectWithTag("DenialGeneral"); //ryan
        generalScript = general.GetComponent<SC_Shared_DN>(); //ryan
        //PlayerPrefs.SetInt("DenialCurrentScore", GetScoreDN());
        generalScript.EndGame();        //ryan
                                        //PlayerPrefs.SetInt("DenialCurrentScore", GetScoreDN());
        PlayerPrefs.SetInt("DenialCurrentScore", (int)transform.position.z);
        endScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
