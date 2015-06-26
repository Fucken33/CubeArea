using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class TextFromPrefs : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        updateText(); // update text at startup
    }

    // public method that makes text updatable from other scripts
    public void updateText()
    {
        Text txt = GetComponent<Text>();
        if (gameObject.name == "Score")
        {
            string score = PlayerPrefs.GetString("lastScore");
            txt.text = "You survived " + score + " challenges in the Cube Area";
        }
        if (gameObject.name == "HighScore")
        {
            string highScore = PlayerPrefs.GetString("highScore");
            txt.text = "The High Score is: " + highScore;
        }
        if (gameObject.name == "newSkinUnlocked")
        {
            CanvasGroup group = this.transform.parent.GetComponent<CanvasGroup>();
            if(PlayerPrefs.HasKey("newSkinUnlocked") &&
                PlayerPrefs.GetString("newSkinUnlocked") == "true")
            {
                group.alpha = 1;
            }
            else
            {
                group.alpha = 0;
            }
            PlayerPrefs.Save();
        }
    }
}