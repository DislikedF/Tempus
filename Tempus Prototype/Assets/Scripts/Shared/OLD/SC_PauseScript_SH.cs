//This script was written by Alan Guild
//This script is a generalised Pause Button and Pause Menu Script to be shared between all mini games.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_PauseScript_SH : MonoBehaviour
{
    public GameObject pauseScreen;      //pass in pause screen from editor
    public GameObject loadingScren;     //pass in loading screen from editor
    public bool touchedPause;          //has pause been touched  ryan
   

    bool isCoRoutineActive;     //Check if there is a co-routine running

    void Start()
    {
        pauseScreen.SetActive(false);       //initially have the pause screen Hidden
        isCoRoutineActive = false;
        touchedPause = false;           // initialise to false
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch0 = Input.GetTouch(0);

            if (touch0.phase == TouchPhase.Ended && touch0.deltaPosition.x < 0.5 && touch0.deltaPosition.x > -0.5 && touch0.deltaPosition.y < 0.5 && touch0.deltaPosition.y > -0.5)     //parameters to make it a tap not an accidental swipe
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);     //Create Raycast from camera to location of touch
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit)  && isCoRoutineActive == false)    //check if its hit anything and that a co-routine isnt running
                {
                    switch (hit.transform.gameObject.name)       //check game object that has been hits identity and compare to identities with command
                    {
                        case "pauseButton":
                            Time.timeScale = 0;     //Pause Game by stopping the Game Time
                            pauseScreen.SetActive(true);        //turn on Pause Splash Screen
                            touchedPause = true;        //change bool to true
                            break;

                        case "Resume":
                            pauseScreen.SetActive(false);       //hide Pause Splash Screen
                            Time.timeScale = 1;     //Set time back to normal 
                            touchedPause = false;       //change bool to false
                           
                            break;

                        case "Restart":
                            isCoRoutineActive = true;
                            string currentScene = SceneManager.GetActiveScene().name;       //get current scenes name
                            AsyncOperation restartScene = SceneManager.LoadSceneAsync(currentScene);        //create ASync operation
                            StartCoroutine(loadSceneAsync(restartScene));        //restart current scene using co-routine
                           // Time.timeScale = 1;
                            break;

                        case "Exit":
                            isCoRoutineActive = true;
                            AsyncOperation Meta = SceneManager.LoadSceneAsync("MetaGame");        //create ASync operation
                            StartCoroutine(loadSceneAsync(Meta));        //return to Meta using the co-routine
                           // Time.timeScale = 1;     //set time back to normal
                            break;
                    }
                }
            }
        }
    }

    IEnumerator loadSceneAsync(AsyncOperation operation)    //loads scene ascynchronously (use this for Loading Screens)
    {
        //Deactivate Current Screen

        //Activate Loading Screen
        loadingScren.SetActive(true);

        for (float i = 0.0f; i <= 1; i += Time.deltaTime / 2.0f)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f); //sets loading betwen 0 and 1 not 0 and 0.9 
            yield return null;
        }
        isCoRoutineActive = false;
    }
}

