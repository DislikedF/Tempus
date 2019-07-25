using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Wall_Break_Script_AN : MonoBehaviour {

    // Determine how fast the player must go to break through this wall
    public float break_Speed;
    public UnityEngine.GameObject destroyedVersion;

    // Use this for initialization
    void Start ()
    {
        destroyedVersion.transform.localScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Destruction()
    {
        UnityEngine.GameObject destroyedClone;
        destroyedClone = Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
        destroyedClone.gameObject.tag = "Broken_AN";
    }
}
