using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] GameObject container;
    List<ItemUI> inventoryItems = new List<ItemUI>();

    Inventory inventory;
    bool opened = false;
    public bool Opened => opened;

    public void SetupUI(Inventory inventory) 
    {
        this.inventory = inventory;
        AddAllItems();
        this.inventory.OnItemAdded   += AddItemUI;
        this.inventory.OnItemRemoved += RemoveItem;
    }


    public void AddAllItems()
    {
        for (int i = 0; i < inventory.Items.Count; i++) AddItemUI(inventory.Items[i]);
        opened = true;
    }

    public void RemoveAllItems()
    {
        foreach (var itemUI in inventoryItems)
        {
            ItemUIPool.Instance.PutObject(itemUI);
        }
    }

    public void AddItemUI(Item item)
    {
        var itemUI = ItemUIPool.Instance.GetObject();
        item.itemUI = itemUI;
        inventoryItems.Add(itemUI);
        itemUI.SetItemUI(item);
        itemUI.transform.SetParent(container.transform);
    }

    public void RemoveItem(Item item)
    {
        if (inventoryItems.Remove(item.itemUI)) 
        {
            ItemUIPool.Instance.PutObject(item.itemUI);
        }
    }

    public void ActivateTransfer(TransferType transferType, Action<ItemUI> transferHandler)
    {
        foreach (var item in inventoryItems) item.ActivateTransfer(transferType, transferHandler);
    }

    public void DeactivateTransfer()
    {
        foreach (var item in inventoryItems) item.DeActivateTransfer();
    }

    public void ActivateAction()
    {
        foreach (var item in inventoryItems) item.ActivateAction("Use");
    }

    public void DeactivateAction()
    {
        foreach (var item in inventoryItems) item.DeActivateAction();
    }
}
