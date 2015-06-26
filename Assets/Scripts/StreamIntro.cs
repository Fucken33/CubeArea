using UnityEngine;
using System.Collections;

public class StreamIntro : MonoBehaviour
{
    bool videoPlayed = false;

    void Awake()
    {
#if UNITY_ANDROID
        if(!videoPlayed){
            Handheld.PlayFullScreenMovie("battlesoft.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
            videoPlayed = true;
        }
#endif

#if UNITY_IOS
        if(!videoPlayed){
            Handheld.PlayFullScreenMovie("battlesoft.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
            videoPlayed = true;
        }
#endif
        Application.LoadLevel("mainmenu");
    }
}
