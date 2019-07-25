using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SC_PauseUI_AC : MonoBehaviour
{
    public GameObject pauseScreen;      //pass in pause screen from editor
    public GameObject UI;       //pass in gameplay UI
    public GameObject loadingScren;     //pass in loading screen from editor

    bool isCoRoutineActive;     //Check if there is a co-routine running

    // Use this for initialization
    void Start ()
    {
        pauseScreen.SetActive(false);       //initially have the pause screen Hidden
        isCoRoutineActive = false;
    }

    void Update()
    {
        if(pauseScreen.activeInHierarchy)
        {
            UI.SetActive(false);
        }
        else if(!pauseScreen.activeInHierarchy)
        {
            UI.SetActive(true);
        }
    }
	
    public void Pause()
    {
        pauseScreen.SetActive(true);        //turn on Pause Splash Screen
        Time.timeScale = 0;     //Pause game by setting time to 0
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);       //hide Pause Splash Screen
        Time.timeScale = 1;     //Set time back to normal 
    }

    public void Restart()
    {
        isCoRoutineActive = true;
        string currentScene = SceneManager.GetActiveScene().name;       //get current scenes name
        AsyncOperation restartScene = SceneManager.LoadSceneAsync(currentScene);        //create ASync operation
        StartCoroutine(loadSceneAsync(restartScene));        //restart current scene using co-routine
    }

    public void Exit()
    {
        isCoRoutineActive = true;
        AsyncOperation Meta = SceneManager.LoadSceneAsync("MetaGame");        //create ASync operation
        StartCoroutine(loadSceneAsync(Meta));        //return to Meta using the co-routine
    }

    IEnumerator loadSceneAsync(AsyncOperation operation)    //loads scene ascynchronously (use this for Loading Screens)
    {
        //Deactivate Current Screen
        Destroy(UI);
        Destroy(pauseScreen);

        //Activate Loading Screen
        loadingScren.SetActive(true);

        Time.timeScale = 1;

        for (float i = 0.0f; i <= 1; i += Time.deltaTime / 2.0f)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f); //sets loading betwen 0 and 1 not 0 and 0.9 
            yield return null;
        }
        isCoRoutineActive = false;
    }
}
