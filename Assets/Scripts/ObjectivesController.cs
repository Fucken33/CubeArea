using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ObjectivesController : MonoBehaviour
{
 
    public Image[] skinsImages;
    public Sprite[] skinsSprites;

    // Use this for initialization
    void Start()
    {

        int skins = 0;
        if (PlayerPrefs.HasKey("unlockedSkins"))
        {
            skins = PlayerPrefs.GetInt("unlockedSkins");
        }
        else return;
        showUnlocked(skins);
        changeText(skins);
    }

    void showUnlocked(int unlockedSkins)
    {
        for (int i = 0; i < unlockedSkins; i++)
        {
            Image img = skinsImages[i];
            Sprite sprite = skinsSprites[i];

            img.sprite = sprite;
        }
    }

    void changeText(int unlockedSkins)
    {
        for (int i = 0; i < unlockedSkins; i++)
        {
            Image img = skinsImages[i];
            GameObject panel = img.gameObject.transform.parent.gameObject;
            Text txt = panel.GetComponentInChildren<Text>();
            txt.fontStyle = FontStyle.Bold;
            txt.color = Color.white;
        }
    }
}
