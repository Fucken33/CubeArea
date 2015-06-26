using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fader : MonoBehaviour
{
    //public float fadeSpeed = 1;
    public float fadeDuration = 1.5f;

    void Start() { Clear(); }

    public void Clear() { StartCoroutine("FadeToClear"); }
    public void Opaque() { StartCoroutine("FadeToOpaque"); }

    public IEnumerator FadeToClear()
    {
        Time.timeScale = 0.001f;
        Image img = GetComponentInChildren<Image>();
        img.CrossFadeAlpha(0, fadeDuration, true); // (targetAlpha, duration, ignoreTimeScale)
        yield return new WaitForSeconds(fadeDuration * Time.timeScale);
        Time.timeScale = 1f;
    }

    public IEnumerator FadeToOpaque()
    {
        Time.timeScale = 0.001f;
        Image img = GetComponentInChildren<Image>();
        img.CrossFadeAlpha(0.6f, fadeDuration, true); // (targetAlpha, duration, ignoreTimeScale)
        yield return new WaitForSeconds(fadeDuration * Time.timeScale);
        Time.timeScale = 1f;
    }
}
