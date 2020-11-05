using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("fodeu bahia, tem + de 1 inventario");
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    int space = 16;

    public List<Item> items = new List<Item>();



    public bool AddItem(Item item)
    {
        if(items.Count >= space)
        {
            return false;//inventário cheio, item não foi pego
        }
        if(item != null)
        {
            items.Add(item);
        }

        if(onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();//comunicação para update da UI de inventario
        }
        return true;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();//comunicação para update da UI de inventario
        }
    }

}
