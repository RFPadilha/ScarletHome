using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;//resume o jogo efetivamente
        gameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;//pausa o jogo efetivamente
        gameIsPaused = true;
    }

    public void OptionsMenu()
    {
        Debug.Log("Options button clicked, levar pra tela de opções");
    }

    public void SaveMenu()
    {
        Debug.Log("Save button clicked, levar pra tela de saves");
    }

    public void QuitGame()
    {
        Debug.Log("Quit button clicked, perguntar se quita pro menu ou do jogo");
        //Application.Quit();
    }

}
