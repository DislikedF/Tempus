using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Collision_01_BA : MonoBehaviour
{
    //Ryan Fearon

    public SC_Score_01_BA scoring;
    public SC_Lvel_01_BA spawn;
    public AudioSource landSound;
    private GameObject lastHit;
    public GameObject player;
    private SC_PlayerControl_01_BA playerScript;
    private Rigidbody collisionObject;

    // Use this for initialization
    void Start()
    {
 
        playerScript = player.GetComponent<SC_PlayerControl_01_BA>();

    }
    

    // Update is called once per frame
    void Update()
    {

        if (player.GetComponent<Rigidbody>().position.y < collisionObject.position.y)
        {
            if (collisionObject != null)
            {
                Physics.IgnoreCollision(collisionObject.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
            }
        }
        else
        {
            Physics.IgnoreCollision(collisionObject.GetComponent<Collider>(), player.GetComponent<Collider>(), false);

        }
    }

    // Hanlde Collisions
    IEnumerator OnCollisionEnter(Collision col)
    {
        collisionObject = col.gameObject.GetComponent<Rigidbody>();

        switch (col.gameObject.name)
        {
           
            case "SM_leaf_011_BA(Clone)":

                if (col.gameObject != null && col.gameObject != lastHit)
                {
                    col.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;

                    lastHit = col.gameObject;
                    scoring.ScoreUpdate();
                    playerScript.canJump = true;
                    landSound.Play();
                    // spawn.UpdateLevel();
                    yield return new WaitForSeconds(1.5f);
                }
                break;

            case "SM_leaf_022_BA(Clone)":

                if (col.gameObject != null && col.gameObject != lastHit)
                {
                    col.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;

                    lastHit = col.gameObject;
                    scoring.ScoreUpdate();
                    playerScript.canJump = true;
                    landSound.Play();


                }
                break;

            case "SM_leaf_033_BA(Clone)":

                if (col.gameObject != null && col.gameObject != lastHit)
                {
                    col.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;

                    lastHit = col.gameObject;
                    scoring.ScoreUpdate();
                    playerScript.canJump = true;
                    landSound.Play();



                }
                break;

            case "SM_leaf_044_BA(Clone)":

                if (col.gameObject != null && col.gameObject != lastHit)
                {
                    col.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;

                    lastHit = col.gameObject;
                    scoring.ScoreUpdate();
                    playerScript.canJump = true;
                    landSound.Play();


                }
                break;

            case "SM_acorn_1_BA(Clone)":
                playerScript.Die(true);
                break;
            case "SM_acorn_2_BA(Clone)":
                playerScript.Die(true);
                break;
            case "SM_acorn_3_BA(Clone)":
                playerScript.Die(true);
                break;
            case "SM_b_tree_2_DP(Clone)":
                playerScript.Die(true);
                break;
            case "SM_b_tree_right_2_DP(Clone)":
                playerScript.Die(true);
                break;
        }
    }
}
