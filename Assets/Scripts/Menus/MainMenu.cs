using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    SoundManager soundManager = SoundManager.instance;
    bool startGame = false;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4) soundManager.PlaySound("Burning");
    }

    public void newGame()
    {
        FadeControl.instance.FadeToBlack(3);
        soundManager.PlaySound("Thunder");
        StartCoroutine(Play());
    }
    public void quitGame()
    {
        Application.Quit();
    }

    public void OpenLink()
    {
        Application.OpenURL("https://linktr.ee/madcakeproductions");
    }
    public void Restart()
    {
        SceneManager.LoadScene(3);
    }
    IEnumerator Play()
    {
        yield return StartCoroutine(WaitThunder());
        if (startGame)
        {
            soundManager.StopSound("Thunder");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
    IEnumerator WaitThunder()
    {
        yield return new WaitForSeconds(3);

        startGame = true;
    }
}
