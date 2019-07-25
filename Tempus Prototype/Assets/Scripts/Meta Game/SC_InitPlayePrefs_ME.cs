//This script is written by Alan Guild
//This script will initialise the PlayerPrefs on the games first play

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_InitPlayePrefs_ME : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        if (!PlayerPrefs.HasKey("DenialHighScore"))
        {
            PlayerPrefs.SetInt("DenialHighScore", 0);
        }

        if (!PlayerPrefs.HasKey("AngerHighScore"))
        {
            PlayerPrefs.SetInt("AngerHighScore", 0);
        }

        if (!PlayerPrefs.HasKey("BargainingHighScore"))
        {
            PlayerPrefs.SetInt("BargainingHighScore", 0);
        }

        if (!PlayerPrefs.HasKey("DepressionHighScore"))
        {
            PlayerPrefs.SetInt("DepressionHighScore", 0);
        }

        if (!PlayerPrefs.HasKey("AcceptanceHighScore"))
        {
            PlayerPrefs.SetInt("AcceptanceHighScore", 0);
        }

        if (!PlayerPrefs.HasKey("Essence"))
        {
            PlayerPrefs.SetInt("Essence", 0);
        }

        if (!PlayerPrefs.HasKey("AC_PlanetReached"))
        {
            PlayerPrefs.SetInt("AC_PlanetReached", 0);
        }

        if (!PlayerPrefs.HasKey("AC_noOfCannisters"))
        {
            PlayerPrefs.SetInt("AC_noOfCannisters", 3);
        }

    }
	
}
