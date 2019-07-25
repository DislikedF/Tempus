//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;   // Very important - J


//// A custom countdown timer that only counts down and displays when we want it to - J
//public class OLD_SC_Timer_DE : MonoBehaviour {

//    public float total_time;
//    public float time_left;
//    public Text Timer_Text;
//    public bool timer_running = false;
//    public bool time_up = false;

//    // 
//    public Slider Dark_Water_Slider;
//    static float lowest_fill = 0.2f;
//    float percent_fill = lowest_fill;
//    //
//    float tick_down = 0;


//    // Initialise
//	void Start () {
//        Timer_Text = GetComponent<Text>();
//	}
	
//	void Update () {

//        // Only run when we want - J
//        if (timer_running == true)
//        {
//            // Tick down via delta time - J
//            time_left -= Time.deltaTime;
//            percent_fill = 1 - (time_left / total_time);

//            if (percent_fill < lowest_fill)
//            {
//                percent_fill = lowest_fill;
//            }

//            // Check if time's run out  - J
//            if (time_left <= 0.0f)
//            {
//                time_left = 0.0f;
//                Dark_Water_Slider.value = percent_fill;
//                Time_Up();
//                time_up = true;
//            }
//            else
//            {
//                // Example/my depression trap related text rounded to 2 decimal places - J
//                Timer_Text.text = "Tap to dodge! : " + time_left.ToString("F2");
//                Dark_Water_Slider.value = percent_fill;

//            }
//        }
//        else if (time_up == false)
//        {   
//            // Text for when the timer isn't running - J
//            Timer_Text.text = "";
//            tick_down += Time.deltaTime;

//            percent_fill = 1 - (tick_down / total_time);

//            if (percent_fill < lowest_fill)
//            {
//                percent_fill = lowest_fill;
//            }
//            Dark_Water_Slider.value = percent_fill;
//        }
//    }

//    // Can be called by trigger with time limit variable being passed in - J
//    public void Begin_Countdown(float Timer_Limit)
//    {
//        time_left = Timer_Limit;
//        total_time = Timer_Limit;
//        timer_running = true;
//    }
    
//    // Can be called by trigger to stop the countdown - J
//    public void Stop_Countdown()
//    {
//        timer_running = false;
//        tick_down = time_left;
//    }

//    // Or when the timer itself runs out, tell it what to do now - J
//    public void Time_Up()
//    {
//        Handheld.Vibrate();
//        Handheld.Vibrate();
//        Dark_Water_Slider.value = percent_fill;

//        timer_running = false;

//        // call more things here to happen, this is messy - J
//        if (SC_Main_DE.Main_Instance1.Current_Gamestate == SC_Main_DE.Gamestate.PLAYING)
//        {
//            SC_Main_DE.Main_Instance1.Current_Gamestate = SC_Main_DE.Gamestate.GAME_OVER;
//        }

//    }
//}
