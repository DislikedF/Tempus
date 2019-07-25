using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_Lvel_01_BA : MonoBehaviour
{

    public GameObject[] leaf;
    public GameObject[] fallingLeaf;
    public GameObject[] acorns;
    public GameObject treeLeft;
    public GameObject treeRight;
    public GameObject player;
	private GameObject lastLeaf;
	private GameObject lastFallingLeafObj;
    public Camera mainCamera;
    private Vector3 lastLeafPosition;
	private Vector3 fallingLastLeaf;
    private Vector3 acornPosition;
    private Vector3 treePositionLeft;
    private Vector3 treePositionRight;
    private float lastLeafX;
	private float leafSpeedMax;
    private float leafSpeedMin;
    private int numberOfTrees;
	private int floatingRandLeaf;
    private int dangerRand;
    private float maxY;
    private float maxX;
    private float minX;
    private Vector3 positionMid;
    private Vector3 positionLeft;
    private Vector3 positionRight;
    // Use this for initialization
    void Start()
    {
        SetUp();

    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 1)
        {
            maxY = lastLeafPosition.y + Random.Range(4.0f, 9.0f);

            CleanUp();

            dangerRand =  (Random.Range(0, 1000));
            if(dangerRand == 18)
            {
                acorn();
                
            }
            if(dangerRand >50 && dangerRand < 60)
            {
                if (numberOfTrees <2)
                {
                    treePositionLeft.y = player.GetComponent<Rigidbody>().position.y + 30;
                    treePositionRight.y = treePositionLeft.y;
                    Instantiate(treeLeft, treePositionLeft, treeLeft.transform.rotation);
                    Instantiate(treeRight, treePositionRight, treeRight.transform.rotation);
                }
            }


            if (floatingRandLeaf > 20)
            {
                fallingLeavesStart();
                floatingRandLeaf = 0;
            }
            floatingRandLeaf++;

            //if (randLeaf > 140)
            //{
            //    UpdateLevel();
            //    randLeaf = 0;

            //}
            //randLeaf++;

            GameObject[] objects1 = GameObject.FindGameObjectsWithTag("Leafs");
            if (objects1 != null)
            {
                

                if (objects1.Length < 10)
                {
                   UpdateLevel();
                }
                //foreach (GameObject leaf in objects1)
                //{
                //    float speed = GetRandomSpeed();
                //    leaf.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, speed, 0.0f);
                //}

            }

            GameObject[] objects2 = GameObject.FindGameObjectsWithTag("FallingLeaf");
            if (objects2 != null)
            {
                foreach (GameObject leaf in objects2)
                {
                    float speed = GetRandomSpeedFloat();
                    leaf.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, speed, 0.0f);
                    
                }
            }
        }

    }
    // Update is called once per frame
    void Update()
    {


 //       maxY = player.transform.position.y + 30.0f;

	//	if (floatingRandLeaf > 80)
	//	{
	//		fallingLeavesStart();
	//		floatingRandLeaf = 0;
	//	}
	//	floatingRandLeaf++;

 //       if (randLeaf > 190)
 //       {
 //           UpdateLevel();
 //           randLeaf = 0;
            
 //       }
 //       randLeaf++;

 //       GameObject[] objects1 = GameObject.FindGameObjectsWithTag("Leafs");
 //       if (objects1 != null)
 //       {
 //           foreach (GameObject leaf in objects1)
 //           {
 //               float speed = GetRandomSpeed();
 //               leaf.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, speed, 0.0f);
 //           }

 //       }

	//	GameObject[] objects2 = GameObject.FindGameObjectsWithTag("FallingLeaf");
	//	if (objects2 != null)
	//	{
	//		foreach (GameObject leaf in objects2)
	//		{
	//			float speed = GetRandomSpeed();
	//			leaf.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, speed, 0.0f);
	//		}
 //   	}
	}

    private float GetRandomSpeed()
    {
        return (Random.Range((leafSpeedMin), leafSpeedMax));
    }


    private float GetRandomSpeedFloat()
    {
        return (Random.Range((leafSpeedMin), leafSpeedMax - 5.0f));
    }

    // Set up values and create first platforms
    void SetUp()
    {
        leafSpeedMin = -0.1f;
        leafSpeedMax = -3.0f;
        numberOfTrees = 0;
        maxY = player.transform.position.y + 20.0f;
        maxX = 7.0f;
        minX = -1.0f;
		floatingRandLeaf = 0;
        dangerRand = 0;
        lastLeafPosition = new Vector3(6.3f, 10.0f, 25.0f);
		fallingLastLeaf = new Vector3(5.3f, 25.0f, 29.5f);
        acornPosition = new Vector3(6.5f, 25.0f, player.GetComponent<Rigidbody>().position.z);
        treePositionLeft = new Vector3(-2.1f, player.GetComponent<Rigidbody>().position.y, player.GetComponent<Rigidbody>().position.z);
        treePositionRight = new Vector3(11.8f, player.GetComponent<Rigidbody>().position.y, player.GetComponent<Rigidbody>().position.z);

        lastLeafX = lastLeafPosition.x;
        SetLeafPosition();

        //for (int x = 0; x < 10; x++)
        //{
        //   UpdateLevel();

        //}
        
   

    }

    public void CleanUp()
    {
        // This Must be improved!!!

        float playerPos = player.transform.position.y;

        GameObject[] objects1 = GameObject.FindGameObjectsWithTag("Leafs");
        GameObject[] objects2 = GameObject.FindGameObjectsWithTag("FallingLeaf");
        GameObject objects3 = GameObject.FindGameObjectWithTag("Ground");
        GameObject[] objects4 = GameObject.FindGameObjectsWithTag("DangerTree");
      

        if (objects2 != null)
        {
            foreach (GameObject leaf in objects2)
            {
                if ((leaf.transform.position.y + 10.0f) < playerPos)
                {
                    GameObject holder = leaf;
                    Destroy(holder, 1);
                }
            }
            }

        if (objects1 != null)
        {

            foreach (GameObject leaf in objects1)
            {
                if ((leaf.transform.position.y + 10.0f) < playerPos)
                {
                    GameObject holder = leaf;
                    Destroy(holder, 1);
                }
            }

        }

        if (objects3 != null)
        {

            if ((objects3.transform.position.y + 10.0f) < playerPos)
            {
                GameObject holder = objects3;
                Destroy(holder, 1);
            }

        }
       

        if (objects4 != null)
        {
            numberOfTrees = objects4.Length;
            foreach (GameObject tree in objects4)
            {
                if ((tree.transform.position.y + 10.0f) < playerPos)
                {
                    GameObject holder = tree;
                    Destroy(holder, 1);
                }
            }
        }

    }

    void PickPos()
    {
        int pos = Random.Range(0, 3);

        switch (pos)
        {
            case 0:
                lastLeafPosition.x = positionLeft.x + Random.Range(3.2f, 5.5f);
                break;

            case 1:
                lastLeafPosition.x = positionMid.x + Random.Range(-3.5f, 3.5f);
                break;

            case 2:
                lastLeafPosition.x = positionRight.x + Random.Range(-5.5f, -3.2f);
                break;
        }
    }
    // Generate random leaf
    public void UpdateLevel()
    {
       

        switch (Random.Range(0, 4))
        {
            //case 0:
            //    if (leaf[0] != null)
            //    {
            //        Instantiate(leaf[0], lastLeafPosition, Quaternion.identity);
            //        lastLeafPosition.x = (Random.Range((lastLeafPosition.x), lastLeafPosition.x + 9.2f));
            //        lastLeafPosition.y = (maxY);
            //        lastLeaf = leaf[0];
            //    }
            //    break;
            case 0:
                if (leaf[0] != null)
                {
                    PickPos();
                    Instantiate(leaf[0], lastLeafPosition, leaf[0].transform.rotation);
                 
                    lastLeafPosition.y = (maxY);
                   
                    lastLeafX = lastLeafPosition.x;
                    lastLeaf = leaf[0];
                }
                break;
            case 1:
                if (leaf[1] != null)
                {
                    PickPos();
                    Instantiate(leaf[1], lastLeafPosition, leaf[1].transform.rotation);
                    lastLeafPosition.y = (maxY);
                    if (lastLeafPosition.x >= (lastLeafX - 1.0f) && lastLeafPosition.x <= (lastLeafX + 1.0f))
                    {
                        if (lastLeafPosition.x >= (lastLeafX - 1.0f))
                        {
                            lastLeafPosition.x = lastLeafPosition.x - 1.5f;
                        }

                        if (lastLeafPosition.x <= (lastLeafX + 1.0f))
                        {
                            lastLeafPosition.x = lastLeafPosition.x + 1.5f;
                        }
                    }
                    lastLeafX = lastLeafPosition.x;
                    lastLeaf = leaf[1];
                }
                break;
            case 2:
                if (leaf[2] != null)
                {
                    PickPos();
                    Instantiate(leaf[2], lastLeafPosition, leaf[2].transform.rotation);
                    lastLeafPosition.y = (maxY);
                    if (lastLeafPosition.x >= (lastLeafX - 1.0f) && lastLeafPosition.x <= (lastLeafX + 1.0f))
                    {
                        if (lastLeafPosition.x >= (lastLeafX - 1.0f))
                        {
                            lastLeafPosition.x = lastLeafPosition.x - 1.5f;
                        }

                        if (lastLeafPosition.x <= (lastLeafX + 1.0f))
                        {
                            lastLeafPosition.x = lastLeafPosition.x + 1.5f;
                        }
                    }

                    lastLeafX = lastLeafPosition.x;
                    lastLeaf = leaf[2];
                }
                break;
            case 3:
                if (leaf[3] != null)
                {
                    PickPos();
                    Instantiate(leaf[3], lastLeafPosition, leaf[3].transform.rotation);
                   // lastLeafPosition.x = (Random.Range((minX), maxX));
                    lastLeafPosition.y = (maxY);
                    if (lastLeafPosition.x >= (lastLeafX - 1.0f) && lastLeafPosition.x <= (lastLeafX + 1.0f))
                    {
                        if (lastLeafPosition.x >= (lastLeafX - 1.0f))
                        {
                            lastLeafPosition.x = lastLeafPosition.x - 1.5f;
                        }

                        if (lastLeafPosition.x <= (lastLeafX + 1.0f))
                        {
                            lastLeafPosition.x = lastLeafPosition.x + 1.5f;
                        }
                    }

                    lastLeafX = lastLeafPosition.x;
                    lastLeaf = leaf[3];
                }
                break;
        }

        

    }

	public void fallingLeavesStart()
	{
		switch (Random.Range(0, 4))
		{

		case 0:
			if (fallingLeaf[0] != null)
			{
				//leaf [0].transform.localScale -= new Vector3 (0.4f,0.4f, 0.4f);
				Instantiate(fallingLeaf[0],  fallingLastLeaf, Quaternion.identity);
				fallingLastLeaf.x = (Random.Range((minX), maxX));
				fallingLastLeaf.y = (maxY);
				lastFallingLeafObj = fallingLeaf[0];
				//lastFallingLeafObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
			break;
		case 1:
			if (fallingLeaf[1] != null)
			{
				//leaf[1].transform.localScale -= new Vector3 (0.4f,0.4f, 0.4f);
				Instantiate(fallingLeaf[1],  fallingLastLeaf, Quaternion.identity);
				fallingLastLeaf.x = (Random.Range((minX), maxX));
				fallingLastLeaf.y = (maxY);
				lastFallingLeafObj = fallingLeaf[1];
			//	lastFallingLeafObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
			break;
		case 2:
			if (fallingLeaf[2] != null) {
				//	leaf[2].transform.localScale -= new Vector3 (0.4f,0.4f, 0.4f);
				Instantiate (fallingLeaf[2], fallingLastLeaf, Quaternion.identity);
				fallingLastLeaf.x = (Random.Range ((minX), maxX));
				fallingLastLeaf.y = (maxY);
				lastFallingLeafObj = fallingLeaf[2];
				//lastFallingLeafObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
			break;

		case 3:
			if (fallingLeaf[3] != null)
			{
				//leaf[3].transform.localScale -= new Vector3 (0.4f,0.4f, 0.4f);
				Instantiate(fallingLeaf[3],  fallingLastLeaf, Quaternion.identity);
				fallingLastLeaf.x = (Random.Range((minX), maxX));
				fallingLastLeaf.y = (maxY);
				lastFallingLeafObj = fallingLeaf[3];
				//	lastFallingLeafObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
			break;
		}

        
	}

    void acorn()
    {
        switch (Random.Range(0, 3))
        {

            case 0:


                Instantiate(acorns[0], acornPosition, Quaternion.identity);
                acornPosition.x = (Random.Range((minX), maxX));
                acornPosition.y = (maxY + 15.0f);
                //lastFallingLeafObj = fallingLeaf[0];


                break;
            case 1:


                Instantiate(acorns[1], acornPosition, Quaternion.identity);
                acornPosition.x = (Random.Range((minX), maxX));
                acornPosition.y = (maxY + 15.0f);
                //lastFallingLeafObj = fallingLeaf[0];


                break;
            case 2:


                Instantiate(acorns[2], acornPosition, Quaternion.identity);
                acornPosition.x = (Random.Range((minX), maxX));
                acornPosition.y = (maxY + 15.0f);
                //lastFallingLeafObj = fallingLeaf[0];


                break;
        }

    }

    void SetLeafPosition()
    {
        positionLeft = new Vector3(0.0f,0.0f,lastLeafPosition.z);
        positionLeft = mainCamera.ViewportToWorldPoint(positionLeft);


        positionRight = new Vector3(1.0f, 0.0f, lastLeafPosition.z);
        positionRight = mainCamera.ViewportToWorldPoint(positionRight);

        positionMid = new Vector3(0.5f, 0.0f, lastLeafPosition.z);
        positionMid = mainCamera.ViewportToWorldPoint(positionMid);

    }
}
