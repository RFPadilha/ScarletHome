using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pickup : MonoBehaviour
{
    [SerializeField] GameObject pickupText;//shows text to pickup

    bool pickupAllowed;
    Interactable textBox;//used for textbox when picked up
    public Item item;//used for inventory management

    SoundManager soundManager;
    

    void Start()
    {
        soundManager = SoundManager.instance;
        pickupText.gameObject.SetActive(false);
        textBox = GetComponent<Interactable>();//initialization
        if (item != null){
            item.keyItem = GetComponent<Interactable>();
        }
    }

    void Update()
    {
        if(pickupAllowed && Input.GetKeyDown(KeyCode.E))//checks overlapping with pickup and key pressed
        {
            PickItUp();
        }
    }
    void PickItUp()
    {
        bool wasPickedUp = Inventory.instance.AddItem(item);//true if item can be picked up, adds to inventory
        textBox.TriggerDialogue();//triggers textbox
        if (gameObject.CompareTag("Finish"))//if it's the interaction that ends the game:
        {
            if (!soundManager.IsPlaying("Match"))//detects if sound is already playing
            {
                soundManager.PlayOneShot("Match");//plays sound once
                StartCoroutine(EndSequence());//starts routine to end the game
            }
        }
        if (wasPickedUp && !gameObject.CompareTag("Finish"))//if interaction happened:
        {
            Destroy(gameObject);//removes object from scene
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickupAllowed = true;
            pickupText.gameObject.SetActive(true);
        }
    }//overlapping with pickup conditions
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickupAllowed = false;
            pickupText.gameObject.SetActive(false);
        }
    }

    IEnumerator EndSequence()
    {
        soundManager.PlaySound("Burning");
        FadeControl.instance.FadeToBlack(3);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(4);
    }
}
