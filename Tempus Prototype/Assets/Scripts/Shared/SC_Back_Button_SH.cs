using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_Back_Button_SH : MonoBehaviour {

	// Update is called once per frame
	public void BackToMenu () {
        SceneManager.LoadScene("MetaGame");
	}
}
