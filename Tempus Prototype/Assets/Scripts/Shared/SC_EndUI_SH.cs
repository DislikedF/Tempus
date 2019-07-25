using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SC_EndUI_SH : MonoBehaviour
{
    public GameObject endScreen;        //pass in the end screen from the editor
    public GameObject loadingScreen;        //pass in the loading screen
    public GameObject GameUI;       //pass in the game ui
    public GameObject pauseButton;      //pass in the pause button UI


    public GameObject currentScore;       //pass in the text from the editor to be edited in C#
    public GameObject highScore;
    public GameObject noOfEssence;

    bool isCoRoutineActive;     //bool to check if there is a co-routine running

    // Use this for initialization
    void Start ()
    {
        endScreen.SetActive(false);     //initialise to have the end screem hidden
        isCoRoutineActive = false;      //no co-routine will run on intialisation
    }
	
	// Update is called once per frame
	void Update ()
    {
        string currentScene = SceneManager.GetActiveScene().name;       //get current scenes name

        if(endScreen.activeInHierarchy)     //if end screen is displayed, turn off Gameplay UI
        {
            pauseButton.SetActive(false);
            GameUI.SetActive(false);
        }

        updateHighScores(currentScene);     //updates HighScores if players new score is greater than the current highest saved score

        updateText(currentScene);       //update scores displayed
    }

    public void Reset()
    {
        isCoRoutineActive = true;
        string currentScene = SceneManager.GetActiveScene().name;       //get current scenes name
        AsyncOperation restartScene = SceneManager.LoadSceneAsync(currentScene);        //create ASync operation
        StartCoroutine(loadSceneAsync(restartScene));        //restart current scene using co-routine
        resetCurrentScores(currentScene);       //reset current score to 0;
    }

    public void Exit()
    {
        isCoRoutineActive = true;
        string currentScene = SceneManager.GetActiveScene().name;       //get current scenes name
        AsyncOperation Meta = SceneManager.LoadSceneAsync("MetaGame");        //create ASync operation
        StartCoroutine(loadSceneAsync(Meta));        //return to Meta using the co-routine
        resetCurrentScores(currentScene);       //reset current score to 0;
    }

    void updateHighScores(string currentScene)
    {
        switch (currentScene)        //csse statement to determine which scene we are currently in and then update scores accordingly
        {
            case "Denial":
                int denialCurrentScore = PlayerPrefs.GetInt("DenialCurrentScore");
                int denialHighScore = PlayerPrefs.GetInt("DenialHighScore");

                if (denialCurrentScore > denialHighScore)
                {
                    PlayerPrefs.SetInt("DenialHighScore", denialCurrentScore);
                }
                break;

            case "Anger":
                int angerCurrentScore = PlayerPrefs.GetInt("AngerCurrentScore");
                int angerHighScore = PlayerPrefs.GetInt("AngerHighScore");

                if (angerCurrentScore > angerHighScore)
                {
                    PlayerPrefs.SetInt("AngerHighScore", angerCurrentScore);
                }
                break;

            case "Bargaining":
                int bargainingCurrentScore = PlayerPrefs.GetInt("BargainingCurrentScore");
                int bargainingHighScore = PlayerPrefs.GetInt("BargainingHighScore");

                if (bargainingCurrentScore > bargainingHighScore)
                {
                    PlayerPrefs.SetInt("BargainingHighScore", bargainingCurrentScore);
                }
                break;

            case "Depression":
                int depressionCurrentScore = PlayerPrefs.GetInt("DepressionCurrentScore");
                int depressionHighScore = PlayerPrefs.GetInt("DepressionHighScore");

                if (depressionCurrentScore > depressionHighScore)
                {
                    PlayerPrefs.SetInt("DepressionHighScore", depressionCurrentScore);
                }
                break;

            case "Acceptance":
                int acceptanceCurrentScore = PlayerPrefs.GetInt("AcceptanceCurrentScore");
                int acceptanceHighScore = PlayerPrefs.GetInt("AcceptanceHighScore");

                if (acceptanceCurrentScore > acceptanceHighScore)
                {
                    PlayerPrefs.SetInt("AcceptanceHighScore", acceptanceCurrentScore);
                }
                break;
        }
    }

    void updateText(string currentScene)
    {
        switch (currentScene)        //csse statement to determine which scene we are currently in and then edit scores accordingly
        {
            case "Denial":
                currentScore.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("DenialCurrentScore").ToString();
                highScore.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("DenialHighScore").ToString();
                break;

            case "Anger":
                currentScore.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("AngerCurrentScore").ToString();
                highScore.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("AngerHighScore").ToString();
                break;

            case "Bargaining":
                currentScore.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("BargainingCurrentScore").ToString();
                highScore.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("BargainingHighScore").ToString();
                break;

            case "Depression":
                currentScore.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("DepressionCurrentScore").ToString();
                highScore.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("DepressionHighScore").ToString();
                break;

            case "Acceptance":
                currentScore.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("AcceptanceCurrentScore").ToString();
                highScore.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("AcceptanceHighScore").ToString();
                break;
        }

        noOfEssence.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("Essence").ToString();
    }

    void resetCurrentScores(string currentScene)
    {
        switch (currentScene)        //csse statement to determine which scene we are currently in and then reset current score
        {
            case "Denial":
                PlayerPrefs.SetInt("DenialCurrentScore", 0);
                break;

            case "Anger":
                PlayerPrefs.SetInt("AngerCurrentScore", 0);
                break;

            case "Bargaining":
                PlayerPrefs.SetInt("BargainingCurrentScore", 0);
                break;

            case "Depression":
                PlayerPrefs.SetInt("DepressionCurrentScore", 0);
                break;

            case "Acceptance":
                PlayerPrefs.SetInt("AcceptanceCurrentScore", 0);
                break;
        }
    }

    IEnumerator loadSceneAsync(AsyncOperation operation)    //loads scene ascynchronously (use this for Loading Screens)
    {
        //Deactivate Current Screen
        //endScreen.SetActive(false);
        Destroy(endScreen);
        Destroy(GameUI);
        Destroy(pauseButton);

        // Deactivate other screens here

        //Activate Loading Screen
        loadingScreen.SetActive(true);

        Time.timeScale = 1;     //timescale needs to be set to one as their is an object rotation on the loading screen

        for (float i = 0.0f; i <= 1; i += Time.deltaTime / 2.0f)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f); //sets loading betwen 0 and 1 not 0 and 0.9 
            yield return null;
        }
        isCoRoutineActive = false;
    }
}
