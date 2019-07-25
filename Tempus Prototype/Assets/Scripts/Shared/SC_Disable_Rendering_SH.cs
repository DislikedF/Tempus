using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Disables rendering on an object, making it invisible - J
public class SC_Disable_Rendering_SH : MonoBehaviour {

    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
    }
}
