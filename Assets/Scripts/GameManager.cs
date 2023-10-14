using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        StartCoroutine(StartLoad());
    }
    IEnumerator StartLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        while (!operation.isDone)
        {
            yield return null;
        }
    }
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName(sceneName).buildIndex);
        FadeControl.instance.FadeIn(1);
    }
}
