//Sun_Rotate was written by Alan Guild
// The Sun_Rotate Script makes the directional light, that is acting as a sun, rotate around the planet
// creating the effect of a Day/Night Cycle. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Sun_Rotate_ME : MonoBehaviour
{
    public bool back;
    public bool up;

    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (back && !up)
        {
            transform.Rotate(Vector3.back, speed * Time.deltaTime);
        }
        else if(up && !back)
        {
            transform.Rotate(Vector3.up, speed * Time.deltaTime);
        }
        else if(up && back)
        {
            transform.Rotate(Vector3.back, speed * Time.deltaTime);
            transform.Rotate(Vector3.up, speed * Time.deltaTime);
        }
    }
}
