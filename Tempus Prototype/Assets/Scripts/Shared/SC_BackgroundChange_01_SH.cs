using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BackgroundChange_01_SH : MonoBehaviour {

    public GameObject backGround;
    public GameObject backGround2;
    public GameObject backGround3;
    public GameObject backGround4;
    public GameObject backGround5;
    public GameObject backGround6;
    public GameObject backGround7;
    public GameObject backGround8;
    private SpriteRenderer sr1;
    private SpriteRenderer sr2;
    private SpriteRenderer sr3;
    private SpriteRenderer sr4;
    private SpriteRenderer sr5;
    private SpriteRenderer sr6;
    private SpriteRenderer sr7;
    private SpriteRenderer sr8;


    private float alpha1;
    private float alpha2;
    private float alpha3;
    private float alpha4;
    private float alpha5;
    private float alpha6;
    private float alpha7;
    private float alpha8;

    private bool revert;

    // Use this for initialization
    void Start () {

        revert = false;

        sr1 = backGround.GetComponent<SpriteRenderer>();
        sr2 = backGround2.GetComponent<SpriteRenderer>();
        sr3 = backGround3.GetComponent<SpriteRenderer>();
        sr4 = backGround4.GetComponent<SpriteRenderer>();
        sr5 = backGround5.GetComponent<SpriteRenderer>();
        sr6 = backGround6.GetComponent<SpriteRenderer>();
        sr7 = backGround7.GetComponent<SpriteRenderer>();
        sr8 = backGround8.GetComponent<SpriteRenderer>();

        alpha1 = 1.0f;
        alpha2 = 1.0f;
        alpha3 = 1.0f;
        alpha4 = 1.0f;
        alpha5 = 1.0f;
        alpha6 = 1.0f;
        alpha7 = 1.0f;
        alpha8 = 1.0f;
        alpha8 = 1.0f;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void FixedUpdate()
    {
        if (revert == false)
        {
            ChangeBackground();
        }
        if(revert == true)
        {
            RevertBackground();
        }
    }

    void ChangeBackground()
    {
        if (sr1.color.a > 0.0f)
        {

            sr1.color = new Color(sr1.color.r, sr1.color.g, sr1.color.b, alpha1);
            alpha1 = alpha1 - 0.004f;
        }

        if (sr1.color.a <= 0.0f && sr2.color.a >= 0.0f)
        {

            sr2.color = new Color(sr2.color.r, sr2.color.g, sr2.color.b, alpha2);
            alpha2 = alpha2 - 0.004f;
        }

        if (sr2.color.a <= 0.0f && sr3.color.a >= 0.0f)
        {

            sr3.color = new Color(sr3.color.r, sr3.color.g, sr3.color.b, alpha3);
            alpha3 = alpha3 - 0.004f;
        }

        if (sr3.color.a <= 0.0f && sr4.color.a >= 0.0f)
        {

            sr4.color = new Color(sr4.color.r, sr4.color.g, sr4.color.b, alpha4);
            alpha4 = alpha4 - 0.004f;
        }

        if (sr4.color.a <= 0.0f && sr5.color.a >= 0.0f)
        {

            sr5.color = new Color(sr5.color.r, sr5.color.g, sr5.color.b, alpha5);
            alpha5 = alpha5 - 0.004f;
        }

        if (sr5.color.a <= 0.0f && sr6.color.a >= 0.0f)
        {

            sr6.color = new Color(sr6.color.r, sr6.color.g, sr6.color.b, alpha6);
            alpha6 = alpha6 - 0.004f;
        }

        if (sr6.color.a <= 0.0f && sr7.color.a >= 0.0f)
        {

            sr7.color = new Color(sr7.color.r, sr7.color.g, sr7.color.b, alpha7);
            alpha7 = alpha7 - 0.004f;
            
        }
        if(sr7.color.a <= 0.0f)
        {
            revert = true;
        }

        
    }
    void RevertBackground()
    {
        if (sr6.color.a <= 0.0f && sr8.color.a >= 1.0f)
        {

            sr7.color = new Color(sr7.color.r, sr7.color.g, sr7.color.b, alpha7);
            alpha7 = alpha7 + 0.004f;
        }
        if (sr5.color.a <= 0.0f && sr7.color.a >= 1.0f)
        {

            sr6.color = new Color(sr6.color.r, sr6.color.g, sr6.color.b, alpha6);
            alpha6 = alpha6 + 0.004f;
        }
        if (sr4.color.a <= 0.0f && sr6.color.a >= 1.0f)
        {

            sr5.color = new Color(sr5.color.r, sr5.color.g, sr5.color.b, alpha5);
            alpha5 = alpha5 + 0.004f;
        }
        if (sr3.color.a <= 0.0f && sr5.color.a >= 1.0f)
        {

            sr4.color = new Color(sr4.color.r, sr4.color.g, sr4.color.b, alpha4);
            alpha4 = alpha4 + 0.004f;
        }
        if (sr2.color.a <= 0.0f && sr4.color.a >= 1.0f)
        {

            sr3.color = new Color(sr3.color.r, sr3.color.g, sr3.color.b, alpha3);
            alpha3 = alpha3 + 0.004f;
        }
        if (sr1.color.a <= 0.0f && sr3.color.a >= 1.0f)
        {

            sr2.color = new Color(sr2.color.r, sr2.color.g, sr2.color.b, alpha2);
            alpha2 = alpha2 + 0.004f;
        }
        if (sr1.color.a <= 1.0f && sr2.color.a >= 1.0f)
        {

            sr1.color = new Color(sr1.color.r, sr1.color.g, sr1.color.b, alpha1);
            alpha1 = alpha1 + 0.004f;
           
        }
        if(sr1.color.a >= 1)
        {
            revert = false;
        }
    }
}
