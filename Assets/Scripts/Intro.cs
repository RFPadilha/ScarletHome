using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField]
    GameObject[] texts;
    void Start()
    {
        foreach(GameObject text in texts){
            text.SetActive(true);
        }
        StartCoroutine(WaitABit());


    }

    IEnumerator WaitABit(){
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
       

    
}
