using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SkinGameController : MonoBehaviour
{
    public Sprite[] skins;
    Dictionary<string, Sprite> dict;

    void Awake()
    {
        dict = new Dictionary<string, Sprite>();
        if(skins.Length < 7)
            Debug.LogError("Sprite array not initialized properly (lenght < 7)");
        dict.Add("Original", skins[0]);
        dict.Add("Plain",    skins[1]);
        dict.Add("Bronze",   skins[2]);
        dict.Add("Silver",   skins[3]);
        dict.Add("Gold",     skins[4]);
        dict.Add("Diamond",  skins[5]);
        dict.Add("Platinum", skins[6]);
        dict.Add("Gangstazomber", skins[7]);        
    }

    // Use this for initialization
    void Start() { setPlayerSkin(); }
    
    void setPlayerSkin()
    {
        if (dict.Count < 7)
            Debug.LogError("Cannot get sprite dictionary from characters controller");
        string skin_name = "Original";
        if (PlayerPrefs.HasKey("currentSkinName"))
        {
            skin_name = PlayerPrefs.GetString("currentSkinName");
        }
        Sprite skin_img;
        bool getOk = dict.TryGetValue(skin_name, out skin_img);
        if (getOk)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<SpriteRenderer>().sprite = skin_img;
        }
        else
        {
            Debug.LogError("Cannot get value from key in imgs dictionary");
        }
    }
}
