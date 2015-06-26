using UnityEngine;
using System.Collections;

public class IntersticialController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        initAd();
    }

    void initAd()
    {
        string[] testDeviceIDs = new string[] { "5F4C090B19AD61E77B6E486313BA3142", 
                                                "C8F954CBD1FABF8E63499AB635B62DD6" };
        EasyGoogleMobileAds.GetInterstitialManager().SetTestDevices(true, testDeviceIDs);
        string[] keywords = new string[] { "videojuegos",
                                           "flappy", "bird", "flappy bird", 
                                           "geometry" , "dash", "geometry dash",
                                           "retro", "plataformas", "juegos", "indie",
                                           "games", "videogames", "rpg", "unity", "unity3d"};
        EasyGoogleMobileAds.GetInterstitialManager().SetKeywords(keywords);

        EasyGoogleMobileAds.GetInterstitialManager().PrepareInterstitial("ca-app-pub-5935337486447963/1634183337");
    }

    public void showAd()
    {
        EasyGoogleMobileAds.GetInterstitialManager().ShowInterstitial();
    }
}
