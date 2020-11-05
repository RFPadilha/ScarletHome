using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    Item item;

    public void Add(Item newItem)
    {
        if (newItem != null)
        {
            item = newItem;
            item.keyItem = newItem.keyItem;
            icon.sprite = item.icon;
            icon.enabled = true;
        }
        else return;
        
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void UseItem()
    {
        item.Use();
    }

    


}
