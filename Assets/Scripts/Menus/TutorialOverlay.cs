using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOverlay : MonoBehaviour
{
    public GameObject overlay;

    private void Start()
    {
        overlay.SetActive(true);
    }
    private void Update()
    {
        Overlay();
    }
    void Overlay()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {

            if (!overlay.activeSelf)
            {
                Debug.Log("Should have opened overlay");
                overlay.SetActive(true);
            }
            else if (overlay.activeSelf)
            {
                Debug.Log("Should have closed overlay");
                overlay.SetActive(false);
            }
        }
    }
}
