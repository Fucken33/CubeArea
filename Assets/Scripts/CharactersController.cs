using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class CharactersController : MonoBehaviour
{
    public Image[] defaultSkins;
    public GameObject[] skins;
    public Sprite lockSprite;
    public Image skinLabelImage;
    public Text skinLabelText;
    public static Dictionary<string, Image> imageMap;
    Image ytberSkin_img;

    // initialize dictionary "skin_name -> skin" on awake (bc its not serializable :c )
    void Awake()
    {
        imageMap = new Dictionary<string, Image>();
        // defaultSkins must contain imgs for "original" and "plain" skins
        if (defaultSkins.Length < 2)
            Debug.LogError("CharactersController.defaultSkins not initialized properly (lenght < 2)");
        imageMap.Add("Original", defaultSkins[0]);
        imageMap.Add("Plain", defaultSkins[1]);
        if (defaultSkins.Length > 2)
        {
            imageMap.Add("Gangstazomber", defaultSkins[2]);
        }

        // skins must contain the 5 classification skins (bronze, silver, gold, diamond, platinum)
        if (skins.Length < 5)
            Debug.LogError("CharactersController.skins not initialized properly (lenght < 5)");
        imageMap.Add("Bronze",   skins[0].GetComponent<Image>());
        imageMap.Add("Silver",   skins[1].GetComponent<Image>());
        imageMap.Add("Gold",     skins[2].GetComponent<Image>());
        imageMap.Add("Diamond",  skins[3].GetComponent<Image>());
        imageMap.Add("Platinum", skins[4].GetComponent<Image>());
    }

    // Use this for initialization
    void Start()
    {
        int unlockedSkins = 0;
        if (PlayerPrefs.HasKey("unlockedSkins"))
        {
            unlockedSkins = PlayerPrefs.GetInt("unlockedSkins");
        }
        lockSkins(skins.Length, unlockedSkins);
        
        GameObject ytberSkin = GameObject.FindGameObjectWithTag("ytberSkin");
        ytberSkin_img = ytberSkin.GetComponent<Image>();
        ytberSkin_img.enabled = false;
        if(PlayerPrefs.HasKey("ytberSkinActive") &&
            PlayerPrefs.GetString("ytberSkinActive") == "true")
        {
            ytberSkin_img.enabled = true;
        }
    }

    void lockSkins(int skin_number, int unlockedSkins)
    {
        for (int i = skin_number; i > unlockedSkins; i--)
        {
            Image img = skins[i-1].GetComponent<Image>();
            img.sprite = lockSprite;
            skins[i-1].GetComponent<Button>().interactable = false;
        }
    }

    public void setSkinLabel(string skin_name)
    {
        setCurrentSkin(skin_name);
        string name = PlayerPrefs.GetString("currentSkinName");
        Image skinImage = imageMap[name];
        skinLabelText.text = name;
        skinLabelImage.sprite = skinImage.sprite;
    }

    public void setCurrentSkin(string skin_name)
    {
        PlayerPrefs.SetString("currentSkinName", skin_name);
        PlayerPrefs.Save();
    }
}
