using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_FollowScript_SH : MonoBehaviour
{
    public UnityEngine.GameObject ToFollow;
    public UnityEngine.GameObject Follower;

    public float offsetX;
    public float offsetY;
    public float offsetZ;

    public bool freezeX;
    public bool freezeY;
    public bool freezeZ;
    public bool Bargaining;

    private float smoothTime = 0.5F;
    private Vector3 velocity = Vector3.zero;

    // Use this for initialization
    void Start ()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Bargaining)
        {
            if (ToFollow.GetComponent<Rigidbody>().velocity.y >= 0.0f)
            {
                Vector3 targetPosition = new Vector3(Follower.transform.position.x, ToFollow.transform.position.y + offsetY, ToFollow.transform.position.z + offsetZ);
                Follower.transform.position = Vector3.SmoothDamp(Follower.transform.position, targetPosition, ref velocity, smoothTime);
                
            }
        }
        else if (!freezeX && !freezeY && !freezeZ)       //freeze none
        {
            Follower.transform.position = new Vector3(ToFollow.transform.position.x + offsetX, ToFollow.transform.position.y + offsetY, ToFollow.transform.position.z + offsetZ);
        }
        else if (freezeX && !freezeY && !freezeZ)      //freeze X
        {
            Follower.transform.position = new Vector3(Follower.transform.position.x, ToFollow.transform.position.y + offsetY, ToFollow.transform.position.z + offsetZ);
        }
        else if (!freezeX && freezeY && !freezeZ)        //freeze Y
        {
            Follower.transform.position = new Vector3(ToFollow.transform.position.x + offsetX, Follower.transform.position.y, ToFollow.transform.position.z + offsetZ);
        }
        else if (!freezeX && !freezeY && freezeZ)        //freeze Z
        {
            Follower.transform.position = new Vector3(ToFollow.transform.position.x + offsetX, ToFollow.transform.position.y + offsetY, Follower.transform.position.z);
        }
        else if (freezeX && freezeY && !freezeZ)     //freeze X & Y
        {
            Follower.transform.position = new Vector3(Follower.transform.position.x, Follower.transform.position.y, ToFollow.transform.position.z + offsetZ);
        }
        else if (freezeX && !freezeY && freezeZ)     //freeze X & Z
        {
            Follower.transform.position = new Vector3(Follower.transform.position.x, ToFollow.transform.position.y + offsetY, Follower.transform.position.z);
        }
        else if (!freezeX && freezeY && freezeZ)     //freeze Y & Z
        {
            Follower.transform.position = new Vector3(ToFollow.transform.position.x + offsetX, Follower.transform.position.y, Follower.transform.position.z);
        }
        
    }
}
