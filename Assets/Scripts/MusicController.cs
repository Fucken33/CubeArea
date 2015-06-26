using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        AudioSource bgMusic = gameObject.GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("MusicToggle"))
        {
            int playmusic = PlayerPrefs.GetInt("MusicToggle");
            if (playmusic == 0)
            {
                bgMusic.enabled = false;
            }
            else
            {
                bgMusic.enabled = true;
            }
        }
    }
}
