using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;//objeto que segura os slots no inventario
    Inventory inventory;//referencia ao sistema de inventario
    InventorySlot[] slots;//slots em si
    public GameObject inventoryUI;
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;//chama a update da UI toda vez que o callback acontece
        inventoryUI.SetActive(false);
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();//referencia aos slots
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.B)) && !inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(true);
        }else if ((Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Escape)) && inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(false);
        }
    }

    void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)//para todos os slots
        {
            if (i < inventory.items.Count)//se ainda houverem items para adicionar
            {
                slots[i].Add(inventory.items[i]);//adiciona
            }
            else slots[i].ClearSlot();//senão remove
        }
    }
}
