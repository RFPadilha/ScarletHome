using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] GameObject pickupText;
    bool pickupAllowed;

    void Start()
    {
        pickupText.gameObject.SetActive(false);
    }

    void Update()
    {
        if(pickupAllowed && Input.GetKeyDown(KeyCode.E))
        {
            PickItUp();
        }
    }
    void PickItUp()
    {
        Destroy(gameObject);
        Debug.Log("Got Key Item! Stuff must have changed on the scenery!");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickupAllowed = true;
            pickupText.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickupAllowed = false;
            pickupText.gameObject.SetActive(false);
        }
    }
}
