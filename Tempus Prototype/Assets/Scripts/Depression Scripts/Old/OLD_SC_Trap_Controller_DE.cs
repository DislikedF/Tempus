//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class OLD_SC_Trap_Controller_DE : MonoBehaviour
//{

//    // This needs improved or a workaround if max_trap_layers is changed - J
//    // Trap setup
//    public UnityEngine.GameObject Trap_1_Layer, Trap_2_Layers, Trap_3_Layers, Trap_4_Layers, Trap_5_Layers;
//    public UnityEngine.GameObject[] traps;
//    public Transform Generate_Point;
//    public Transform Destroy_Point;
//    public float trap_division_distance;

//    private float trap_layers;
//    public int min_trap_layers;
//    public int max_trap_layers;

//    // Use this for initialization
//    void Start()
//    {
//    }

//    void Update()
//    {

//        Generate_Traps();
//        Clear_Behind();
//    }

//    void Generate_Traps()
//    {
//        if (transform.position.x < Generate_Point.position.x)
//        {
//            // Set a new location for the next trap - relative to Trap_Generator_DE object - J
//            transform.position = new Vector3(transform.position.x + trap_division_distance, -0.7f, 2.0f);

//            // Pick how many layers it will have and instantiate it (add 1 to max_trap_layers because 
//            // Random.Range(min, max) is inclusive for min but exclusive for max (1 & 5 gives 1-4 result) - J
//            switch (Random.Range(min_trap_layers, max_trap_layers + 1))
//            {
//                case 1:
//                    Instantiate(Trap_1_Layer, transform.position, transform.rotation);
//                    break;

//                case 2:
//                    Instantiate(Trap_2_Layers, transform.position, transform.rotation);
//                    break;

//                case 3:
//                    Instantiate(Trap_3_Layers, transform.position, transform.rotation);
//                    break;

//                case 4:
//                    Instantiate(Trap_4_Layers, transform.position, transform.rotation);
//                    break;

//                case 5:
//                    Instantiate(Trap_5_Layers, transform.position, transform.rotation);
//                    break;
//            }
//        }


//    }

//    void Clear_Behind()
//    {
//        // Fill the traps array with objects tagged as a "Trap" in the scene - J
//        if (traps == null)
//        {
//            traps = UnityEngine.GameObject.FindGameObjectsWithTag("Trap");
//        }

//        // For each of those, check its x position and destroy those behind the destroy point - J
//        foreach (UnityEngine.GameObject trap in traps)
//        {
//            /*
//            //Justin was here 2017
//              ゴ ゴ ゴ ゴ ゴ ゴ ゴ ゴ ゴ ゴゴ ゴ ゴ ゴ ゴ ゴ ゴ ゴ ゴ ゴゴ ゴ ゴ ゴ ゴ ゴ ゴ ゴ ゴ ゴゴ ゴ ゴ ゴ ゴ ゴ ゴ ゴ 


//                            ＴＨＩＳ ＭＵＳＴ ＢＥ ＴＨＥ ＷＯＲＫ ＯＦ ＡＮ ＥＮＥＭＹ 「ＳＴＡＮＤ」！！
　
//              ゴ ゴ ゴ ゴ ゴ ゴ ゴ ゴ ゴ ゴゴ ゴ ゴ ゴ ゴ ゴ ゴ ゴ ゴ ゴゴ ゴ ゴ ゴ ゴ ゴ ゴ ゴ ゴ ゴゴ ゴ ゴ ゴ ゴ ゴ ゴ
//              */
            
//            // Destroy traps the player has passed - J
//            if (trap.transform.position.x < Destroy_Point.position.x)
//            {
//                UnityEngine.GameObject temp = trap;
//                Destroy(temp, 1);
//            }
//        }

//        // plug memory leaks - J
//        traps = null;
//    }
//}