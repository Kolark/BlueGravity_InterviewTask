using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ShopManagerUI : MonoBehaviour
{
    [SerializeField] InventoryUIController shopInventory;

    public  static ShopManagerUI Instance => instance;
    private static ShopManagerUI instance;

    bool opened = false;
    public bool Opened => opened;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void HideUI()
    {
        //Do tween stuff here
        shopInventory.RemoveAllItems();
    }
}
