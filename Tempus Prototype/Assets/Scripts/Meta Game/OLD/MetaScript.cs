//MetaScript was written by Alan Guild
//The MetaScript is for the Main Menu / Meta Game and contains all the controls and actions for the scene.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TO DO
//- remove Desktop Code
//-implement in-shop code

public class MetaScript : MonoBehaviour
{
    public UnityEngine.GameObject Planet;
    public float rotSpeed;

    public UnityEngine.GameObject star;
 
    public int noOfStars;
   
    private Transform cameraTransform;
    public Transform lookShop;
    public Transform lookMain;
    public Transform lookSettings;

    public AudioSource forwardButton;
    public AudioSource backButton;

    private bool isCoRoutineActive = false;
    Coroutine lastRoutine = null;

    void Start()
    {
        for(int i =0; i<noOfStars; i++)
        {

            UnityEngine.GameObject mainStarClone;
            mainStarClone = Instantiate(star, new Vector3(Random.Range(-20.0f, 20.0f), Random.Range(-10.0f, 10.0f), Random.Range(15.0f, 20.0f)),Quaternion.identity);
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

    void Update()
    {
        //if (SystemInfo.deviceType == DeviceType.Handheld) //Check if Handheld device
        //{
            if (Input.touchCount > 0)        //check for a touch
            {
                RaycastHit HitDestination = new RaycastHit();

                bool Hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).deltaPosition), out HitDestination);      //get touch position for raycast

                switch (HitDestination.transform.gameObject.name)       //switch statement
                {
                    case "Left":
                        Planet.transform.Rotate(Vector3.up, -rotSpeed);     //roatate planet left
                        break;


                    case "Right":
                        Planet.transform.Rotate(Vector3.up, rotSpeed);      //rotate planet right
                        break;

                    case "Denial":
                        SceneManager.LoadScene("Denial");       //load Denial Level      
                        break;

                    case "Anger":
                        SceneManager.LoadScene("Anger");        //load Anger scene
                        break;

                    case "Bargaining":
                        SceneManager.LoadScene("Bargaining");        //load Bargaining scene   
                        break;

                    case "Depression":
                        SceneManager.LoadScene("Depression");        //load Depression scene
                        break;

                    case "Acceptance":
                        SceneManager.LoadScene("Acceptance");         // Load acceptance scene
                        break;

                    case "Shop":        //rotate camera to look at the shop
                        isCoRoutineActive = true;
                        backButton.Play();
                        lastRoutine = StartCoroutine(rotateToShop());
                        break;

                    case "Back":        //rotate camera back to main screen
                        isCoRoutineActive = true;
                        forwardButton.Play();
                        lastRoutine = StartCoroutine(rotateToMain());
                        break;

                    case "Settings":        //rotate camera to look at the shop
                        if (!isCoRoutineActive)
                        {
                            isCoRoutineActive = true;
                            backButton.Play();
                            lastRoutine = StartCoroutine(rotateToSettings());
                        }
                        break;

                }
            }
            
        //}
        //else if (SystemInfo.deviceType == DeviceType.Desktop)       //REMOVE AFTER TESTING ON DESKTOP
        //{
            //if (Input.GetMouseButton(0))        //change to Input.TouchCount > 0;
            //{
            //    RaycastHit HitDestination = new RaycastHit();

            //    bool Hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out HitDestination);      //change Input.mousePosition to Input.GetTouch(0).deltaPosition

            //    if (Hit)
            //    {
            //        switch (HitDestination.transform.gameObject.name)       //switch statement
            //        {
            //            case "Left":
            //                Planet.transform.Rotate(Vector3.up, -rotSpeed);     //roatate planet left
            //                break;


            //            case "Right":
            //                Planet.transform.Rotate(Vector3.up, rotSpeed);      //rotate planet right
            //                break;

            //            case "Denial":
            //                SceneManager.LoadScene("Denial");       //load Denial Level      
            //                break;

            //            case "Anger":
            //                SceneManager.LoadScene("Anger");        //load Anger scene
            //                break;

            //            case "Bargaining":
            //                SceneManager.LoadScene("Bargaining");        //load Bargaining scene   
            //                break;

            //            case "Depression":
            //                SceneManager.LoadScene("Depression");        //load Depression scene
            //                break;

            //            case "Acceptance":
            //                SceneManager.LoadScene("Acceptance");         // Load acceptance scene
            //                break;

            //            case "Shop":
            //                if (!isCoRoutineActive)
            //                {
            //                    isCoRoutineActive = true;
            //                    forwardButton.Play();
            //                    lastRoutine = StartCoroutine(rotateToShop());
            //                }
            //                break;

            //            case "Back":
            //                if (!isCoRoutineActive)
            //                {
            //                    isCoRoutineActive = true;
            //                    backButton.Play();
            //                    lastRoutine = StartCoroutine(rotateToMain());
            //                }
            //                break;

            //            case "Settings":
            //                if (!isCoRoutineActive)
            //                {
            //                    isCoRoutineActive = true;
            //                    backButton.Play();
            //                    lastRoutine = StartCoroutine(rotateToSettings());
            //                }
            //                break;
                       
            //        }
            //    }
            //}
        }
 



    IEnumerator rotateToShop()
    {
        for (float i = 0.0f; i <= 1; i += Time.deltaTime / 2.0f)
        {
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, lookShop.rotation, i);
            yield return null;
        }
        isCoRoutineActive = false;
    }

    IEnumerator rotateToMain()
    {
        for (float i = 0.0f; i <= 1; i += Time.deltaTime / 2.0f)
        {
           
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, lookMain.rotation, i);
            yield return null;
        }
        isCoRoutineActive = false;
    }

    IEnumerator rotateToSettings()
    {
        for (float i = 0.0f; i <= 1; i += Time.deltaTime / 2.0f)
        {
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, lookSettings.rotation, i);
            yield return null;
        }
        isCoRoutineActive = false;
    }
 
    
}

