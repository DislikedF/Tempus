using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Gravity_SH : MonoBehaviour
{
    public float gravity_y;

    // And because why not
    public float gravity_x;
    public float gravity_z;

    private Vector3 set_gravity_3D;

	// Use this for initialization
	void Awake () {
        set_gravity_3D.Set(gravity_x, gravity_y, gravity_z);
        Physics.gravity = set_gravity_3D;
	}

}
