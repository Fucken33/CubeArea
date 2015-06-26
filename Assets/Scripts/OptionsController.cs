using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class OptionsController : MonoBehaviour
{

    public Toggle SFXToggle;
    public Toggle MusicToggle;

    // Use this for initialization
    void Start()
    {
        bool SFXToggleValue;
        bool MusicToggleValue;
        if (PlayerPrefs.HasKey("SFXToggle") && PlayerPrefs.GetInt("SFXToggle") == 0)
            SFXToggleValue = false;
        else 
            SFXToggleValue = true;

        if (PlayerPrefs.HasKey("MusicToggle") && PlayerPrefs.GetInt("MusicToggle") == 0)
            MusicToggleValue = false;
        else
            MusicToggleValue = true;

        SFXToggle.isOn = SFXToggleValue;
        MusicToggle.isOn = MusicToggleValue;

        SFXToggle.onValueChanged.AddListener(onSFXToggle);
        MusicToggle.onValueChanged.AddListener(onMusicToggle);
    }

    public void onSFXToggle(bool value)
    {
        int toggle;
        if (value)
        {
            toggle = 1;
        }
        else
        {
            toggle = 0;
        }
        PlayerPrefs.SetInt("SFXToggle", toggle);
        PlayerPrefs.Save();
    }

    public void onMusicToggle(bool value)
    {
        int toggle;
        if (value)
        {
            toggle = 1;
        }
        else
        {
            toggle = 0;
        }
        PlayerPrefs.SetInt("MusicToggle", toggle);
        PlayerPrefs.Save();
    }

    public void removeData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
