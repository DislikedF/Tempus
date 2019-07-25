using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_OnCollission_AC : MonoBehaviour
{
    public GameObject otherObject;      //object to collide with

    public GameObject endScreen;        //end screen to activate when level reached end state

    void start()
    {
        endScreen.SetActive(false);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject == otherObject)
        {
            endScreen.SetActive(true);
        }
    }

}

