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
                overlay.SetActive(true);
            }
            else if (overlay.activeSelf)
            {
                overlay.SetActive(false);
            }
        }
    }
}
