using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour
{
    public string level = "game";
    public void loadLevel()
    {
        StartCoroutine(onLoadLevel());
    }

    IEnumerator onLoadLevel()
    {
        Fader fader = GameObject.FindObjectOfType<Fader>();
        if (fader)
            yield return StartCoroutine(fader.FadeToOpaque());
        else
            yield return null;
        Application.LoadLevel(level);
    }

    public void exit()
    {
        Application.Quit();
    }
}
