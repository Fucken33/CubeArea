using UnityEngine;
using System.Collections;

public class ResetScore : MonoBehaviour {

	public void deleteSavedScore()
    {
        PlayerPrefs.SetString("lastScore", "0");
        PlayerPrefs.SetString("highScore", "0");
        GameObject[] labels = GameObject.FindGameObjectsWithTag("EndText");
        foreach(GameObject label in labels)
        {
        	label.GetComponent<TextFromPrefs>().updateText();
        }
    }
}
