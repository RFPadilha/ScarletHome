using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public Image blackScreen;
    public int alpha;
    void Start()
    {
        
        blackScreen.canvasRenderer.SetAlpha(alpha);
        if (alpha == 1)
        {
            blackScreen.enabled = true;
            blackScreen.CrossFadeAlpha(0, 2, false);
        }
        else blackScreen.enabled = false;
    }

    public void fadeOut()
    {
        blackScreen.enabled = true;
        blackScreen.CrossFadeAlpha(1, 2, false);
    }
    
}
