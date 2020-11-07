using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    SoundManager soundManager = SoundManager.instance;
    bool startGame = false;
    public FadeControl fade;
    private void Start()
    {
        fade = FindObjectOfType<FadeControl>();
    }

    public void newGame()
    {
        fade.Fade();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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
