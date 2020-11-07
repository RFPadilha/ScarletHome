using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    SoundManager soundManager = SoundManager.instance;
    private void Start()
    {
        if (!soundManager.IsPlaying("Burning"))
        {
            soundManager.PlaySound("Burning");
        }
    }
}
