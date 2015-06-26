using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class ScoreCounter : MonoBehaviour
{
    private int score;

    void Start()
    {
        score = 0;
    }

    public int getScore(){ return score; }

    public void updateScore()
    {
        score++;
        GetComponent<Text>().text = "" + score;
    }

    public void saveScore()
    {
        string score_txt = GetComponent<Text>().text;
        PlayerPrefs.SetString("lastScore", score_txt);

        bool hasKey = PlayerPrefs.HasKey("highScore");
        if (!hasKey)
        {
            PlayerPrefs.SetString("highScore", score_txt);
        }
        else
        {
            string highScore_txt = PlayerPrefs.GetString("highScore");
            float highScore_f = float.Parse(highScore_txt);
            float score_f = float.Parse(score_txt);

            if (score_f > highScore_f)
            {
                PlayerPrefs.SetString("highScore", score_txt);
            }
        }
        PlayerPrefs.Save();
    }
}
