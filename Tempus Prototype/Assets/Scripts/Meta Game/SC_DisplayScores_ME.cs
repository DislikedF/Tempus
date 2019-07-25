using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_DisplayScores_ME : MonoBehaviour
{
    public GameObject Denial;
    public GameObject Anger;
    public GameObject Bargaining;
    public GameObject Depression;
    public GameObject Acceptance;

	void Update()
    {
        Denial.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("DenialHighScore").ToString();     //Display Denial Score as a String
        Anger.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("AngerHighScore").ToString();     //Display Anger Score as a String
        Bargaining.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("BargainingHighScore").ToString();     //Display Bargaining Score as a String
        Depression.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("DepressionHighScore").ToString();     //Display Depression Score as a String
        Acceptance.GetComponent < TextMesh >().text = PlayerPrefs.GetInt("AcceptanceHighScore").ToString();     //Display Acceptance Score as a String
	}

}
