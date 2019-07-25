//This Script was written by Alan Guild
//This Script is a generalised Game End Screen script to be shared between all mini games.

//Needs To Work: 
// - Author of main level script to set currentScore PlayerPref for their scene.
// - Activate EndScreen GameObject and set Time.timescale to 0; (pauses game time)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SC_EndScreen_SH : MonoBehaviour
{
    public GameObject endScreen;        //pass in the end screen from the editor
    public GameObject loadingScreen;        //pass in the loading screen

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

            updateHighScores(currentScene);     //updates HighScores if players new score is greater than the current highest saved score

            updateText(currentScene);       //update scores displayed

            if (Input.touchCount == 1)
            {
                Touch touch0 = Input.GetTouch(0);

                if (touch0.phase == TouchPhase.Ended && touch0.deltaPosition.x < 0.5 && touch0.deltaPosition.x > -0.5 && touch0.deltaPosition.y < 0.5 && touch0.deltaPosition.y > -0.5)     //parameters to make it a tap not an accidental swipe
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);     //Create Raycast from camera to location of touch
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit) && isCoRoutineActive == false)    //check if its hit anything and that a co-routine isnt running
                    {
                        switch(hit.transform.gameObject.name)
                        {
                            case "replayButton":
                                isCoRoutineActive = true;
                                //string currentScene = SceneManager.GetActiveScene().name;       //get current scenes name
                                AsyncOperation restartScene = SceneManager.LoadSceneAsync(currentScene);        //create ASync operation
                                StartCoroutine(loadSceneAsync(restartScene));        //restart current scene using co-routine
                               // Time.timeScale = 1;
                                resetCurrentScores(currentScene);       //reset current score to 0;
                                break;

                            case "backButton":
                                isCoRoutineActive = true;
                                AsyncOperation Meta = SceneManager.LoadSceneAsync("MetaGame");        //create ASync operation
                                StartCoroutine(loadSceneAsync(Meta));        //return to Meta using the co-routine
                               // Time.timeScale = 1;     //set time back to normal
                                resetCurrentScores(currentScene);       //reset current score to 0;
                                break;
                        }
                    }
                }
            }

            
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
                PlayerPrefs.SetInt("BargainingCurrentScore",0);
                break;

            case "Depression":
                PlayerPrefs.SetInt("DepressionCurrentScore", 0);
                break;

            case "Acceptance":
                PlayerPrefs.SetInt("AcceptanceCurrentScore",0);
                break;
        }
    }

    IEnumerator loadSceneAsync(AsyncOperation operation)    //loads scene ascynchronously (use this for Loading Screens)
    {
        //Deactivate Current Screen
        endScreen.SetActive(false);

        //Activate Loading Screen
        loadingScreen.SetActive(true);

        for (float i = 0.0f; i <= 1; i += Time.deltaTime / 2.0f)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f); //sets loading betwen 0 and 1 not 0 and 0.9 
            yield return null;
        }
        isCoRoutineActive = false;
    }
}
