using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour
{

    public AudioSource bgMusic;
    public Image asterisk;

    // Use this for initialization
    void Start()
    {
        // control showing the asterisk for "new skin unlocked"
        if (PlayerPrefs.HasKey("newSkinUnlocked") &&
                 PlayerPrefs.GetString("newSkinUnlocked") == "true")
        {
            asterisk.enabled = true;
            PlayerPrefs.SetString("newSkinUnlocked", "false");
        }
        else
        {
            asterisk.enabled = false;
        }
    }

}
