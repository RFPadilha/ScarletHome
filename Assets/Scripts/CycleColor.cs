using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleColor : MonoBehaviour
{
    LabPuzzle puzzle;
    [SerializeField] GameObject cycleText;//shows text to cycle
    bool cycleAllowed;
    void Start()
    {
        puzzle = FindObjectOfType<LabPuzzle>();
        cycleText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cycleAllowed && Input.GetKeyDown(KeyCode.E))//checks overlapping with flask and key pressed
        {
            puzzle.trigger = this.gameObject.tag;
            puzzle.Cycle();
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
