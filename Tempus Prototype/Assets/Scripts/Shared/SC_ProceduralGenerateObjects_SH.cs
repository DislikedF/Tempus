using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ProceduralGenerateObjects_SH : MonoBehaviour
{
    public GameObject parent;

    public float noOfclones;

    public float minX;
    public float maxX;

    public float minY;
    public float maxY;
    
    public float minZ;
    public float maxZ;

    public bool random;

	// Use this for initialization
	void Start ()
    {
        Quaternion spawnRotation = Quaternion.Euler(0.0f, 270.0f, 0.0f); 
       
        Vector3 gamepos = new Vector3(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z);

        if (random)
        {
            for (int i = 0; i < noOfclones; i++)
            {
                GameObject clone;
                clone = Instantiate(parent, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ)), Quaternion.identity);
                clone.gameObject.tag = "Clone";
                clone.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < noOfclones; i++)
            {
                GameObject clone;
                clone = Instantiate(parent, new Vector3((gamepos.x += maxX), (gamepos.y += maxY), (gamepos.z += maxZ)), spawnRotation);
                clone.gameObject.tag = "Clone";
                clone.SetActive(true);
            }
        }
	}

}
