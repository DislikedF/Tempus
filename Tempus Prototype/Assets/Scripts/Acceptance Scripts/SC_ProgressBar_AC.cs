//this script was written by Alan Guild
//this script controls the progress bar

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SC_ProgressBar_AC : MonoBehaviour
{
    public Slider progressBar;     //public slider passed in from editor

    float percentageComplete;       //percentage completed variable

    public Rigidbody Balloon;
  
    public Transform planetTransform;
	
	// Update is called once per frame
	void Update ()
    {
        percentageComplete = (100 / planetTransform.position.y) * Balloon.position.y;
        progressBar.value = percentageComplete;
	}
}
