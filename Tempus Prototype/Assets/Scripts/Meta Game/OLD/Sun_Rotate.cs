//Sun_Rotate was written by Alan Guild
// The Sun_Rotate Script makes the directional light, that is acting as a sun, rotate around the planet
// creating the effect of a Day/Night Cycle. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun_Rotate : MonoBehaviour
{
    public float speed;
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(Vector3.back, speed * Time.deltaTime);
	}
}
