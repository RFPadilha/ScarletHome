
using UnityEngine;

[CreateAssetMenu(fileName = "Key Item", menuName = "Inventory")]
public class Item : ScriptableObject
{
    new public string name = "Shiny Coin";
    public Sprite icon = null;
    public Interactable keyItem;

    public virtual void Use()
    {
        keyItem.TriggerDialogue();
    }
    
}
