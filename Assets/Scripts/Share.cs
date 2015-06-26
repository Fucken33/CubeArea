using UnityEngine;
using System.Collections;

public class Share : MonoBehaviour
{
    private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
    private const string TWEET_LANGUAGE = "en";

    public void twitter(string textToDisplay)
    {
        Application.OpenURL(TWITTER_ADDRESS +
                    "?text=" + WWW.EscapeURL(textToDisplay) +
                    "&amp;lang=" + WWW.EscapeURL(TWEET_LANGUAGE));
    }

    public void scoreToTwitter()
    {
        string score = PlayerPrefs.GetString("lastScore");
        string tweet = "I scored "+score+" points on Cube Area. Can you beat the record? See the ranking on cubearea.com/ranking";
        twitter(tweet);
    }
}
