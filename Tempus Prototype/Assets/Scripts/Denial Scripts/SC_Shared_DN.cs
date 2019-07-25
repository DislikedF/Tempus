using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_Shared_DN : MonoBehaviour
{
    public GameObject endScreen;

    // Private definitions
    private static int numberOfPaths_DN = 6;
    private int score_DN = 0;
    private int renderDistance_DN = 150;

    // Mutator Methods
    protected int GetNumberOfPaths_DN() { return numberOfPaths_DN; }

    protected int GetRenderDistance_DN() { return renderDistance_DN; }

    protected int GetScore_DN() { return score_DN; }
    protected void SetScore_DN(int newScore) { score_DN = newScore; }



    // General Functions

    protected void Start()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
    }
    protected void PauseScreenSetup()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
    }
    protected bool isPaused()
    {
        if (Time.timeScale == 1)
        {
            // Game is not paused
            return false;
        }
        else
        {
            // Game is paused
            return true;
        }
    }
    //protected void Clean(string tag, float playerZPos, int offset)
    //{
    //    GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

    //    foreach (GameObject Block in objects)
    //    {
    //        if ((Block.transform.position.z + offset) < playerZPos)
    //        {
    //            Destroy(Block, 1);
    //        }
    //    }

    //    objects = new GameObject[0];
    //}
    public void EndGame() // ryan changed to public so can be accesed by collsion
    {
        // Save the players score
        //PlayerPrefs.SetInt("DenialCurrentScore", score_DN);

        endScreen.SetActive(true);
        //PlayerPrefs.SetInt("DenialCurrentScore", score_DN);

        Time.timeScale = 0;
        //PlayerPrefs.SetInt("DenialCurrentScore", score_DN);
    }
}
