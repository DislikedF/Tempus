//this script was written by Alan Guild
//this script changes what balloon model is visible in the scene according to the progress bar value

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SC_ChangeBalloon_AC : MonoBehaviour
{
    //all Them balloon Models

    public GameObject Balloon1;
    public GameObject Balloon2;
    public GameObject Balloon3;
    public GameObject Balloon4;
    public GameObject Balloon5;
    public GameObject Balloon6;
    public GameObject Balloon7;
    public GameObject Balloon8;
    public GameObject Balloon9;
    public GameObject Balloon10;
    public GameObject Balloon11;
    public GameObject Balloon12;
    public GameObject Balloon13;
    public GameObject Balloon14;
    public GameObject Balloon15;
    public GameObject Balloon16;
    public GameObject Balloon17;
    public GameObject Balloon18;
    public GameObject Balloon19;
    public GameObject Balloon20;
    public GameObject Balloon21;
    public GameObject Balloon22;
    public GameObject Balloon23;
    public GameObject Balloon24;
    public GameObject Balloon25;

    public GameObject BalloonFlash;

    public Slider fuelBar;

    // Use this for initialization
    void Start ()
    {
        BalloonFlash.SetActive(false);

        Balloon1.SetActive(true);
        Balloon2.SetActive(false);
        Balloon3.SetActive(false);
        Balloon4.SetActive(false);
        Balloon5.SetActive(false);
        Balloon6.SetActive(false);
        Balloon7.SetActive(false);
        Balloon8.SetActive(false);
        Balloon9.SetActive(false);
        Balloon10.SetActive(false);
        Balloon11.SetActive(false);
        Balloon12.SetActive(false);
        Balloon13.SetActive(false);
        Balloon14.SetActive(false);
        Balloon15.SetActive(false);
        Balloon16.SetActive(false);
        Balloon17.SetActive(false);
        Balloon18.SetActive(false);
        Balloon19.SetActive(false);
        Balloon20.SetActive(false);
        Balloon21.SetActive(false);
        Balloon22.SetActive(false);
        Balloon23.SetActive(false);
        Balloon24.SetActive(false);
        Balloon25.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        int balloonVal = (int)(fuelBar.value * 10);

        updateBalloonModel();

       // balloon away to pop flash thingy
       if(balloonVal < 15)
        {
            BalloonFlash.SetActive(true);
        }
        else
        {
            BalloonFlash.SetActive(false);
        }

    }

    void updateBalloonModel()
    {
        int currentVal = (int)(fuelBar.value * 10);

        switch (currentVal)
        {
            case 100:
                Balloon1.SetActive(true);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 96:
                Balloon1.SetActive(false);
                Balloon2.SetActive(true);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 92:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(true);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon20.SetActive(false);
                break;

            case 88:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(true);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 84:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(true);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 80:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(true);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 76:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(true);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 72:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(true);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 68:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(true);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 64:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(true);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 60:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(true);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 56:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(true);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 52:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(true);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 48:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(true);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 44:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(true);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 40:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(true);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 36:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(true);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 32:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(true);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 28:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(true);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 24:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(true);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 20:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(true);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 16:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(true);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 12:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(true);
                Balloon24.SetActive(false);
                Balloon25.SetActive(false);
                break;

            case 8:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(true);
                Balloon25.SetActive(false);
                break;

            case 4:
                Balloon1.SetActive(false);
                Balloon2.SetActive(false);
                Balloon3.SetActive(false);
                Balloon4.SetActive(false);
                Balloon5.SetActive(false);
                Balloon6.SetActive(false);
                Balloon7.SetActive(false);
                Balloon8.SetActive(false);
                Balloon9.SetActive(false);
                Balloon10.SetActive(false);
                Balloon11.SetActive(false);
                Balloon12.SetActive(false);
                Balloon13.SetActive(false);
                Balloon14.SetActive(false);
                Balloon15.SetActive(false);
                Balloon16.SetActive(false);
                Balloon17.SetActive(false);
                Balloon18.SetActive(false);
                Balloon19.SetActive(false);
                Balloon20.SetActive(false);
                Balloon21.SetActive(false);
                Balloon22.SetActive(false);
                Balloon23.SetActive(false);
                Balloon24.SetActive(false);
                Balloon25.SetActive(true);
                break;

        }
    }
}
