using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pickup : MonoBehaviour
{
    [SerializeField] GameObject pickupText;//shows text to pickup
    bool pickupAllowed;
    public Interactable textBox;//used for textbox when picked up
    SoundManager soundManager = SoundManager.instance;
    FadeControl fade;
    public Item item;//used for inventory management

    void Start()
    {
        pickupText.gameObject.SetActive(false);
        textBox = GetComponent<Interactable>();//initialization
        if (item != null){
            item.keyItem = GetComponent<Interactable>();
        }
        fade = FindObjectOfType<FadeControl>();
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
        bool wasPickedUp = Inventory.instance.AddItem(item);
        //add to inventory
        textBox.TriggerDialogue();//triggers textbox
        if (wasPickedUp)
        {
            Destroy(gameObject);//removes object from scene
        }
        if (gameObject.CompareTag("Finish"))
        {
            if (!soundManager.IsPlaying("Match"))
            {
                soundManager.PlayOneShot("Match");
                StartCoroutine(EndSequence());
            }
            
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
        fade.Fade();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
