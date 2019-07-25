using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Obstacle_AN : MonoBehaviour {

    public Camera camera;
    public int fadeDist;

    private Renderer rend;
    private Material[] tempMat;
    private Color tempColor;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        tempMat = rend.materials;
    }
	
	// Update is called once per frame
	void Update () {
		if(transform.position.z - camera.transform.position.z < fadeDist)
        {
            for (int i = 0; i < rend.materials.Length; i++)
            {
                if (rend.materials[i].HasProperty("_Color"))
                {
                    tempColor = tempMat[i].color;
                    tempColor.a = (transform.position.z - camera.transform.position.z);
                    tempMat[i].color = tempColor;
                }
            }

            rend.materials = tempMat;
        }
	}
}
