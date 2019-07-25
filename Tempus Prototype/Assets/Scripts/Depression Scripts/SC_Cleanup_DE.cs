using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Cleanup_DE : MonoBehaviour {

    public string object_tag_1;
    public string object_tag_2;
    public string object_tag_3;
    public string object_tag_4;
    public string object_tag_5;

    public Transform Destroy_Limit_Point;

    // Can be throttled if causing issues - J
    public int Function_Frame_Throttle;

    private List<UnityEngine.GameObject> obj_destruct_list;
    private List<string> tags_to_destroy;

    // Use this for initialization
    void Start()
    {
        obj_destruct_list = new List<UnityEngine.GameObject>();
        tags_to_destroy = new List<string>();

        if (object_tag_1.Length != 0) { tags_to_destroy.Add(object_tag_1); }
        if (object_tag_2.Length != 0) { tags_to_destroy.Add(object_tag_2); }
        if (object_tag_3.Length != 0) { tags_to_destroy.Add(object_tag_3); }
        if (object_tag_4.Length != 0) { tags_to_destroy.Add(object_tag_4); }
        if (object_tag_5.Length != 0) { tags_to_destroy.Add(object_tag_5); }


        // Function_Frame_Throttle = 10;
    }
    // Update is called once per frame
    void Update()
    {

        // Throttle the function to not run every frame
        if (Function_Frame_Throttle != 0)
        {
            if (Time.frameCount % Function_Frame_Throttle == 0)
            {
                Clear();
            }
        }
        else
        { 
            Clear();
        }
    }

    void Clear()
    {

        // Find objects with the correct object tag in scene, and delete the ones behind the destroy point
        // Any objects being attached to this script must have a tag (ie 'Scenery_DE')
        // This is a string you input into the script inspector directly


        foreach (string str in tags_to_destroy)
        {
            obj_destruct_list.AddRange(GameObject.FindGameObjectsWithTag(str));
        }

        foreach (UnityEngine.GameObject obj in obj_destruct_list)
        {
            if (obj.transform.position.x < Destroy_Limit_Point.position.x)
            {
                UnityEngine.GameObject temp = obj;
                Destroy(temp, 1);
            }
        }

        // plug leaks
        obj_destruct_list.Clear();
    }
}
