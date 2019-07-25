using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SC_CloudAnim_DN : SC_Shared_DN
{
    #region Cloud Animation Variables
    // MH - Create dynamic movement of the clouds in the scene
    private float xSway;
    private float pi;
    private bool swaySwap;
    #endregion

    #region Start
    void Start ()
    {
        SetupCloudAnimation();
	}

    void SetupCloudAnimation()
    {
        xSway = 0.0f;
        pi = 3.14159265f;
        swaySwap = true;
    }
    #endregion

    #region Update
    // Update is called once per frame
    void Update ()
    {
        CloudAnimate();
    }

    void CloudAnimate()
    {
        // MH - Increase the degree value over time to insert into the sine wave function
        xSway += 0.01f * Time.deltaTime;

        // MH - Transform 
        transform.position = new Vector3(
            GentleMotion(xSway) + transform.position.x, // x
            transform.position.y,                       // y
            transform.position.z);	                    // z
    }
    float GentleMotion(float x)
    {
        float output;
        double adjVal = x * 100;

        output = ((float)Math.Sin(adjVal)) / 50.0f;

        return output;
    }
    #endregion
}
