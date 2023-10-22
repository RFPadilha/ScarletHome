using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour
{
    public static FadeControl instance = null;

    [SerializeField] Image fadeBlack;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void FadeToBlack(float speed)
    {
        StartCoroutine(Fade(1, speed));
    }
    public void FadeIn(float speed)
    {
        StartCoroutine(Fade(0, speed));
    }
    public IEnumerator Fade(int value, float speed)//1 for fade to black, 0 to fade in
    {
        if(value == 1)
        {
            while (fadeBlack.color.a < 1)
            {
                fadeBlack.color = new Color(fadeBlack.color.r, fadeBlack.color.g, fadeBlack.color.g, fadeBlack.color.a + (Time.deltaTime * speed));
                yield return 0;
            }
        }else
        {
            while (fadeBlack.color.a > 0)
            {
                fadeBlack.color = new Color(fadeBlack.color.r, fadeBlack.color.g, fadeBlack.color.g, fadeBlack.color.a - (Time.deltaTime * speed));
                yield return 0;
            }
        }
        
    }
    public IEnumerator QuickFade(float speed)
    {
        StartCoroutine(Fade(1, speed));
        yield return new WaitForSeconds(1);
        StartCoroutine(Fade(0, speed));
    }
}
