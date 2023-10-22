using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleColor : MonoBehaviour
{
    [SerializeField] GameObject cycleText;//shows text to cycle
    [SerializeField] LabPuzzle controller;
    public int color;//0 to 6, in order of the rainbow
    public SpriteRenderer spriteRenderer;
    bool cycleAllowed;


    void Start()
    {
        cycleText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cycleAllowed && Input.GetKeyDown(KeyCode.E))//checks overlapping with flask and key pressed
        {
            controller.NextColor(this);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cycleAllowed = true;
            cycleText.gameObject.SetActive(true);
        }
    }//overlapping with pickup conditions
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cycleAllowed = false;
            cycleText.gameObject.SetActive(false);
        }
    }
}
