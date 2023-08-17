using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] GameObject container;

    bool opened = false;
    public bool Opened => opened;

    List<ItemUI> inventoryItems = new List<ItemUI>();

    public void ShowItems(List<Item> items)
    {
        for (int i = 0; i < items.Count; i++) AddItem(items[i]);
        opened = true;
    }

    public void RemoveAllItems()
    {
        foreach (var itemUI in inventoryItems)
        {
            ItemUIPool.Instance.PutObject(itemUI);
        }
    }

    public void AddItem(Item item)
    {
        var itemUI = ItemUIPool.Instance.GetObject();
        inventoryItems.Add(itemUI);
        itemUI.SetItemUI(item);
        itemUI.transform.SetParent(container.transform);
    }

    public void RemoveItem(ItemUI itemUI)
    {
        if (inventoryItems.Remove(itemUI)) 
        {
            ItemUIPool.Instance.PutObject(itemUI);
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
