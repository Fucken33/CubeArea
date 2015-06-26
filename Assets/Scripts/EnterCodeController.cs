using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnterCodeController : MonoBehaviour
{
    public InputField input;

    void Start()
    {
        input.onEndEdit.AddListener(checkCode);
    }

    public void checkCode(string code)
    {
        if (code == "GANGSTA")
        {
            PlayerPrefs.SetString("ytberSkinActive", "true");
            PlayerPrefs.Save();
        }
    }

}
