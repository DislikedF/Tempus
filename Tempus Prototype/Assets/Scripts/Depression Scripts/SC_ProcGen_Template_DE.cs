using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ProcGen_Template_DE : MonoBehaviour
{


    // Max of 8 objects, if anyone knows a more elegant way to do this let me know - J
    public UnityEngine.GameObject   obj_type_1,
                                    obj_type_2,
                                    obj_type_3,
                                    obj_type_4,
                                    obj_type_5,
                                    obj_type_6,
                                    obj_type_7,
                                    obj_type_8;

    private List<UnityEngine.GameObject> obj_type_list;

    // For referring to level origin or the player character-attached forward generate limit point
    public Transform Reference_Object; 
    public Transform Generate_Limit_Point;


    // bools for editor controls
    public bool Random_Scaling;
    public bool Object_Bunching;
    public bool Random_Y_Rotation_Variance;
    public bool Random_Position_Variance;
    //  Randomised object pool or single object? - J
    public bool Only_Spawn_One_Object;

    //  Minimum division between each object - J
    public float division_distance;

    //  Offset from the relative object's position and rotation  - J
    public float x_offset;
    public float y_offset;
    public float z_offset;

    public float x_rotation;
    public float y_rotation;
    public float z_rotation;

    // For randomised placement, set values to +/- a value (ie 10, -10) as it is just added to the position at the moment - J
    public int x_range_min;
    public int x_range_max;
    public int y_range_min;
    public int y_range_max;
    public int z_range_min;
    public int z_range_max;

    // Random scaling range
    public float random_scaling_min;
    public float random_scaling_max;
    private float random_scaling_final;

    // For bunching objects, to have it be modifiable in editor - J
    public int max_object_group_size;
    public int percent_objects_grouped;
    public float object_bunch_division_x;
    //public float object_bunch_division_y;
    //public float object_bunch_division_z;
    private int total_bunch_size;

    // Rotation storage for instantiate function - J
    Quaternion spawn_rotation;

    // How fast the reference object's transform moves per frame (must be considerably faster than the player) - J
    public float iterate_speed;

    // Buffer values for storing positions - J
    private Vector3 temp_location;
    private Vector3 previous_obj_position;

    // Can be throttled if causing issues - J
    public int Function_Frame_Throttle;

    private void Start()
    {
        // Reorientate objects for all instances
        spawn_rotation = Quaternion.Euler(x_rotation, y_rotation, z_rotation);

        // Set initial transform for spawn point to equal our reference object
        transform.position = Reference_Object.position;

        // Set initial baseline "previous" to start point
        previous_obj_position = transform.position;

        // Properly define list
        obj_type_list = new List<UnityEngine.GameObject>();

        // Fill it
        if (obj_type_1 != null) { obj_type_list.Add(obj_type_1); }
        if (obj_type_2 != null) { obj_type_list.Add(obj_type_2); }
        if (obj_type_3 != null) { obj_type_list.Add(obj_type_3); }
        if (obj_type_4 != null) { obj_type_list.Add(obj_type_4); }
        if (obj_type_5 != null) { obj_type_list.Add(obj_type_5); }
        if (obj_type_6 != null) { obj_type_list.Add(obj_type_6); }
        if (obj_type_7 != null) { obj_type_list.Add(obj_type_7); }
        if (obj_type_8 != null) { obj_type_list.Add(obj_type_8); }

        // Set initial speed of iteration (can be played around with as it is framerate-dependent)
        iterate_speed = 1.0f;

    }

    //  Update is called once per frame
    void Update()
    {
        //  First check if we're throttling the functions for performance
        //  Then Generate at a suitable rate

        if (Function_Frame_Throttle != 0)
        {
            if (Time.frameCount % Function_Frame_Throttle == 0)
            {
                Generate();
            }

        }
        else
        {
            Generate();
        }
    }

    void Generate()
    {
        //  Constantly generate objects limited by minimum divisions up to a max distance ahead of player - J
        if (transform.position.x < Generate_Limit_Point.position.x)
        {
            //  Increment a new location for the spawner
            transform.position = new Vector3(transform.position.x + (iterate_speed), y_offset, z_offset);

            //  Check if the new location is far enough away from the previous spawned object's X and instantiate
            if ((transform.position.x - previous_obj_position.x) >= division_distance)
            {
                // Create an object to refer to
                GameObject newObject;

                // If we want to randomise Y-axis rotation, do it
                if (Random_Y_Rotation_Variance == true)
                {
                    // rotation * quaternion == local axis -----  quaternion * rotation == world axis - J
                    spawn_rotation = spawn_rotation * Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
                }

                // Check if randomised placement is enabled and set a location accordingly
                if (Random_Position_Variance == true)
                {
                    // Add random factor to positions (shouldn't change if left 0)
                    temp_location = new Vector3(previous_obj_position.x + division_distance + Random.Range(x_range_min, x_range_max),
                                            transform.position.y + Random.Range(y_range_min, y_range_max),
                                            transform.position.z + Random.Range(z_range_min, z_range_max));

                }
                else
                {
                    temp_location = new Vector3(previous_obj_position.x + division_distance, transform.position.y, transform.position.z);
                }


                // Instantiate the object with a reference, either just the first one in the pool or randomly pick one
                if (Only_Spawn_One_Object == false)
                {
                    newObject = Instantiate(obj_type_list[Random.Range(0, obj_type_list.Count)]) as GameObject;
                    newObject.transform.position = (newObject.transform.position + temp_location);
                    newObject.transform.rotation = (newObject.transform.rotation * spawn_rotation);
                }
                else
                {
                    newObject = Instantiate(obj_type_list[0]) as GameObject;
                    newObject.transform.position = (newObject.transform.position + temp_location);
                    newObject.transform.rotation = (newObject.transform.rotation * spawn_rotation);
                }

                // If random scaling is enabled, scale it (must be done after instantiating)
                if (Random_Scaling == true)
                {
                    random_scaling_final = Random.Range(random_scaling_min, random_scaling_max);
                    newObject.transform.localScale = newObject.transform.localScale * random_scaling_final;
                }

                // Save last position as new previous
                previous_obj_position = temp_location;

                // If bunching of generated objects is enabled, bunch occasionally
                if (Object_Bunching == true)
                {
                    Generate_Bunch(temp_location);
                }

                // Clear the buffer Transform vector and reset any changes to rotation, and clear buffer GameObject - J
                temp_location = new Vector3(0, 0, 0);
                transform.rotation = Quaternion.identity;
                newObject = null;
            }           
        }
    }

    void Generate_Bunch(Vector3 temp_location)
    {

        GameObject newObject;

        // Randomly roll 0-99, if rolled number is higher than input % chance, create a bunch - J
        if (Random.Range(0, 100) > (99 - percent_objects_grouped))
        {
            /// Randomly roll for the current bunch size
            /// Minimum in range is 1 so if we get this far, we might as well 
            /// actually generate the extra objects rather than have a "bunch" size of 1 - J
            total_bunch_size = Random.Range(1, max_object_group_size) + 1; 

            /// Generate the right amount of objects with enough distance between each 
            /// (currently still random or always 1st - could be improved to always bunch the same object (ie 3 logs in a row, 3 snow piles, etc.)
            /// Start at 1 because our already-spawned anchor object counts as the first in the bunch - J
            /// I love ///'s - J
            for (int i = 1; i < total_bunch_size; i++)
            {
                // Add editor value division between bunched objects, and a little random Z variance to seem less uniform
                temp_location = temp_location + new Vector3(object_bunch_division_x, 0, Random.Range(-0.5f, 0.5f));

                // Instantiate the object with a reference, either just the first one in the pool or randomly pick one - J
                if (Only_Spawn_One_Object == false)
                {
                    newObject = Instantiate(obj_type_list[Random.Range(0, obj_type_list.Count)]) as GameObject;
                    newObject.transform.position = (newObject.transform.position + temp_location);
                    newObject.transform.rotation = (newObject.transform.rotation * spawn_rotation);
                    Debug.Log("Bunch Spawn!");
                }
                else
                {
                    newObject = Instantiate(obj_type_list[0]) as GameObject;
                    newObject.transform.position = (newObject.transform.position + temp_location);
                    newObject.transform.rotation = (newObject.transform.rotation * spawn_rotation);
                    Debug.Log("Bunch Spawn!");
                }

                // If random scaling is enabled, scale it (must be done after instantiating) (currently still random - could be improved to always keep bunched objects to identical scale) - J              
                if (Random_Scaling == true)
                {
                    random_scaling_final = Random.Range(random_scaling_min, random_scaling_max);
                    newObject.transform.localScale = newObject.transform.localScale * random_scaling_final;
                }

                // Clear buffer gameobject
                newObject = null;
            }

            // Save last generated object position as previous for next frame - J
            previous_obj_position = temp_location;
        }
    }
}

