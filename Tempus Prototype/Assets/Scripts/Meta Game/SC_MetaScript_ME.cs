//MetaScript was written by Alan Guild
//The MetaScript is for the Main Menu / Meta Game and contains all the controls and actions for the scene.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SC_MetaScript_ME : MonoBehaviour
{

    public UnityEngine.GameObject Planet;
    public float rotSpeed;

    private Vector3 fp;     //initial touch position
    private Vector3 lp;     //last touch position
    private float dragDistance;     //min drag for a swipe

    public UnityEngine.GameObject star;
    public int noOfStars;       //number of stars to be rendered in the background

    private Vector2 currentTouch;
    private Vector2 endTouch;
 
    public GameObject sureReset;        //reset Scores screens 
    public GameObject scoreReset;
    public GameObject scoresMain;

    public GameObject settingsMain;     //variable to reset entire game
    public GameObject settingsure;
    public GameObject gamereset;

    private Transform cameraTransform;      //Camera Rotation Variables
    private string currentLook = "Planet";
    public Transform lookShop;
    public Transform lookMain;
    public Transform lookSettings;
    public Transform lookScores;

    public AudioSource buttonClick;       //Audio Variables
    public AudioSource swipeSwoosh;        

    public GameObject metaScreen;       //loading screen variables
    public GameObject loadingScreen;
    public Slider loadingBar;


    private bool isCoRoutineActive = false;
    Coroutine lastRoutine = null;

    void Start()
    {
        if(Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }

        Screen.orientation = ScreenOrientation.Portrait;

        dragDistance = Screen.height * 15 / 100; //15% of screen size

        for (int i = 0; i < noOfStars; i++)
        {

            UnityEngine.GameObject mainStarClone;
            mainStarClone = Instantiate(star, new Vector3(Random.Range(-20.0f, 20.0f), Random.Range(-10.0f, 10.0f), Random.Range(15.0f, 20.0f)), Quaternion.identity);
            mainStarClone.gameObject.tag = "Clone";
            mainStarClone.SetActive(true);

            UnityEngine.GameObject shopStarClone;
            shopStarClone = Instantiate(star, new Vector3(Random.Range(-20.0f, -15.0f), Random.Range(-10.0f, 10.0f), Random.Range(-20.0f, 20.0f)), Quaternion.identity);
            shopStarClone.gameObject.transform.Rotate(new Vector3(0.0f, -90.0f, 0.0f));
            shopStarClone.gameObject.tag = "Clone";
            shopStarClone.SetActive(true);

            UnityEngine.GameObject settingStarClone;
            settingStarClone = Instantiate(star, new Vector3(Random.Range(-20.0f, 20.0f), Random.Range(-10.0f, 10.0f), Random.Range(-20.0f, -15.0f)), Quaternion.identity);
            settingStarClone.gameObject.tag = "Clone";
            settingStarClone.SetActive(true);
        }

        cameraTransform = Camera.main.transform;
    }

    void Update()   //was fixed update
    {
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);   //get Touch Data
            if(touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                lp = touch.position;
                if (Mathf.Abs(lp.x - fp.x) > dragDistance)
                {
                    if ((lp.x > fp.x))
                    {
                        //right rotation
                        if (!swipeSwoosh.isPlaying)     //play swoosh audio on swipe
                        {
                            swipeSwoosh.Play();
                        }

                        Planet.transform.Rotate(0.0f, -touch.deltaPosition.x / 10.0f, 0.0f);       //Rotate Planet
                    }
                    else
                    {
                        //left rotation
                        if (!swipeSwoosh.isPlaying)     //play swoosh audio on swipe
                        {
                            swipeSwoosh.Play();
                        }

                        Planet.transform.Rotate(0.0f, -touch.deltaPosition.x / 12.5f, 0.0f);
                    }
                }
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                lp = touch.position;

                if(Mathf.Abs(lp.x - fp.x) < dragDistance)
                {
                    //tap
                    GetCommand();
                }
            }
        }

        //if (Input.touchCount == 1)        //check for a touch
        //{
        //    Touch touch0 = Input.GetTouch(0);   //get Touch Data

        //    if (touch0.phase == TouchPhase.Began)        //get position of current touch
        //    {
        //        currentTouch = touch0.position;
        //    }

        //    //chack if Tap or Swipe
        //    if (touch0.phase == TouchPhase.Moved && touch0.position != currentTouch && currentLook == "Planet")        //Swipe
        //    {
        //        if (!swipeSwoosh.isPlaying)     //play swoosh audio on swipe
        //        {
        //            swipeSwoosh.Play();
        //        }

        //        Planet.transform.Rotate(0.0f, -touch0.deltaPosition.x / 10.0f, 0.0f);        //Rotate Planet
        //    }
        //    else if(touch0.phase == TouchPhase.Ended && touch0.position == currentTouch)
        //    {
        //        GetCommand();
        //    }
        //}
    }

    void GetCommand()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            switch (hit.transform.gameObject.name)       //switch statement
            {

                case "Denial":
                    isCoRoutineActive = true;       //load Denial Level   
                    AsyncOperation Denial = SceneManager.LoadSceneAsync("Denial");
                    StartCoroutine(loadSceneAsync(Denial));
                    break;

                case "Anger":
                    isCoRoutineActive = true;       //load Anger scene
                    AsyncOperation Anger = SceneManager.LoadSceneAsync("Anger");
                    StartCoroutine(loadSceneAsync(Anger));
                    break;

                case "Bargaining":
                    isCoRoutineActive = true;       //load Bargaining scene 
                    AsyncOperation Bargaining = SceneManager.LoadSceneAsync("Bargaining");
                    StartCoroutine(loadSceneAsync(Bargaining));
                    break;

                case "Depression":
                    isCoRoutineActive = true;       //load Depression scene
                    AsyncOperation Depression = SceneManager.LoadSceneAsync("Depression");
                    StartCoroutine(loadSceneAsync(Depression));
                    break;

                case "Acceptance":
                    isCoRoutineActive = true;       // Load acceptance scene
                    AsyncOperation Acceptance = SceneManager.LoadSceneAsync("Acceptance");
                    StartCoroutine(loadSceneAsync(Acceptance));
                    break;

                case "Shop":        //rotate camera to look at the shop
                    isCoRoutineActive = true;
                    buttonClick.Play();
                    currentLook = "Shop";
                    lastRoutine = StartCoroutine(rotateToShop());
                    break;

                case "HighScore":       //rotate camera to look at High Score Page
                    isCoRoutineActive = true;
                    buttonClick.Play();
                    currentLook = "Scores";
                    lastRoutine = StartCoroutine(rotateToHighScores());
                    break;

                case "Back":        //rotate camera back to main screen
                    isCoRoutineActive = true;
                    buttonClick.Play();
                    currentLook = "Planet";
                    lastRoutine = StartCoroutine(rotateToMain());
                    break;

                case "Settings":        //rotate camera to look at the settings and make sure settings is on 
                    isCoRoutineActive = true;
                    buttonClick.Play();
                    currentLook = "Settings";
                    lastRoutine = StartCoroutine(rotateToSettings());
                    break;

                case "ScoreResetButton":        //button to reset score
                    buttonClick.Play();
                    scoresMain.SetActive(false);
                    scoreReset.SetActive(false);
                    sureReset.SetActive(true);
                    break;

                case "ScoreYes":
                    buttonClick.Play();
                    resetScores();      //call to reset score function

                    sureReset.SetActive(false);
                    scoresMain.SetActive(false);
                    scoreReset.SetActive(true);
                    break;

                case "ScoreNo":
                    buttonClick.Play();
                    sureReset.SetActive(false);
                    scoresMain.SetActive(true);
                    break;

                case "ScoreOk":
                    buttonClick.Play();
                    sureReset.SetActive(false);
                    scoreReset.SetActive(false);
                    scoresMain.SetActive(true);
                    break;

                case "GameResetButton":     //button to reset game
                    buttonClick.Play();
                    settingsMain.SetActive(false);
                    gamereset.SetActive(false);
                    settingsure.SetActive(true);
                    break;

                case "GameYes":
                    buttonClick.Play();
                    resetGame();      //call to reset game function

                    settingsure.SetActive(false);
                    settingsMain.SetActive(false);
                    gamereset.SetActive(true);
                    break;

                case "GameNo":
                    buttonClick.Play();
                    settingsure.SetActive(false);
                    settingsMain.SetActive(true);
                    break;

                case "GameOk":
                    buttonClick.Play();
                    gamereset.SetActive(false);
                    scoreReset.SetActive(false);
                    settingsMain.SetActive(true);
                    break;
            }
        }
    }

    void resetScores()      //reset player prefs relevant with scores
    {
        PlayerPrefs.SetInt("DenialHighScore", 0);
        PlayerPrefs.SetInt("AngerHighScore", 0);
        PlayerPrefs.SetInt("BargainingHighScore", 0);
        PlayerPrefs.SetInt("DepressionHighScore", 0);
        PlayerPrefs.SetInt("AcceptanceHighScore", 0);
    }

    void resetGame()        //reset all player prefs
    {
        PlayerPrefs.SetInt("DenialCurrentScore", 0);
        PlayerPrefs.SetInt("DenialHighScore", 0);
        PlayerPrefs.SetInt("AngerCurrentScore", 0);
        PlayerPrefs.SetInt("AngerHighScore", 0);
        PlayerPrefs.SetInt("BargainingCurrentScore", 0);
        PlayerPrefs.SetInt("BargainingHighScore", 0);
        PlayerPrefs.SetInt("DepressionCurrentScore", 0);
        PlayerPrefs.SetInt("DepressionHighScore", 0);
        PlayerPrefs.SetInt("AcceptanceCurrentScore", 0);
        PlayerPrefs.SetInt("AcceptanceHighScore", 0);
        PlayerPrefs.SetInt("AC_PlanetReached", 0);
        PlayerPrefs.SetInt("AC_noOfCannisters", 0);
        PlayerPrefs.SetInt("Essence", 0);
    }

    IEnumerator loadSceneAsync(AsyncOperation operation)    //loads scene ascynchronously
    {
        metaScreen.SetActive(false);
        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f); //sets loading betwen 0 and 1 not 0 and 0.9 
            loadingBar.value = progress;
            yield return null;
        }

        isCoRoutineActive = false;
    }

    IEnumerator rotateToShop()
    {
        for (float i = 0.0f; i <= 1; i += Time.deltaTime * 2f)
        {
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, lookShop.rotation, i);
            yield return null;
        }
        isCoRoutineActive = false;
        StopCoroutine(rotateToShop());
    }

    IEnumerator rotateToMain()
    {
        for (float i = 0.0f; i <= 1; i += Time.deltaTime * 2f)
        {

            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, lookMain.rotation, i);
            yield return null;
        }
        isCoRoutineActive = false;
        StopCoroutine(rotateToMain());
    }

    IEnumerator rotateToSettings()
    {
        for (float i = 0.0f; i <= 1; i += Time.deltaTime * 2f)
        {
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, lookSettings.rotation, i);
            yield return null;
        }
        isCoRoutineActive = false;
        StopCoroutine(rotateToSettings());
    }

    IEnumerator rotateToHighScores()
    {
        for(float i = 0.0f; i <=1; i+= Time.deltaTime * 2f)
        {
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, lookScores.rotation, i);
            yield return null;
        }
        isCoRoutineActive = false;
        StopCoroutine(rotateToHighScores());
    }


}
