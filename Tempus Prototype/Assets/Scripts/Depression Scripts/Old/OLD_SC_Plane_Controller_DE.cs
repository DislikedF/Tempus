//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class OLD_SC_Plane_Controller_DE : MonoBehaviour {

//    public UnityEngine.GameObject Surface_Plane, Floor_Plane;
//    public UnityEngine.GameObject[] planes;

//    public Transform Generate_Point;
//    public Transform Destroy_Point;


//    //  Width of planes at x-scaling = 3 : 30 units - J
//    public float division_distance;

//    /*  Bit more complicated, if changing the z-length of planes, I always aim to have 2 unity units 
//        (grid squares) worth of platform sticking out from player's position towards the camera - J  */
//    public float surface_plane_z_offset;

//    //  set this to whatever value of y you want floor planes to sit at below the player  - J
//    public float floor_plane_y_offset;

//    //  Update is called once per frame
//    void Update()
//    { 
//        Generate_Planes();
//        Clear_Planes();
//    }

//    void Generate_Planes()
//    {
//        //  Constantly generate planes limited by minimum divisions up to a max distance ahead of player - J
//        if (transform.position.x < Generate_Point.position.x)
//        {
//            //  Set a new location for the next surface plane - relative to Player_Character_DE object 
//            //  (must offset Z and X, and reset Y to origin) and generate it - J
//            transform.position = new Vector3(transform.position.x + division_distance, 0, surface_plane_z_offset);
//            Instantiate(Surface_Plane, transform.position, transform.rotation);

//            //  Modify Y & Z position for the floor plane beneath the player and generate that too 
//            //  Resets Z to origin to be under the player - J
//            transform.position = new Vector3(transform.position.x, floor_plane_y_offset, 6);
//            Instantiate(Floor_Plane, transform.position, transform.rotation);

//        }
//    }

//    void Clear_Planes()
//    {
//        if (planes == null)
//        {
//            planes = UnityEngine.GameObject.FindGameObjectsWithTag("Scenery_DE");
//        }

//        foreach (UnityEngine.GameObject plane in planes)
//        {
//            if (plane.transform.position.x < Destroy_Point.position.x)
//            {
//                UnityEngine.GameObject temp = plane;
//                Destroy(temp, 1);
//            }
//        }

//        // plug memory leaks
//        planes = null;
//    }
//}