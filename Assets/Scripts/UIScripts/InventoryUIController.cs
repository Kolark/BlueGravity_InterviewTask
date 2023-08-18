using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] GameObject container;
    //List<ItemUI> inventoryItems = new List<ItemUI>();
    Dictionary<Item, ItemUI> itemUIs = new Dictionary<Item, ItemUI>();
    Inventory inventory;
    bool opened = false;
    public bool Opened => opened;
    public IEnumerable<ItemUI> AllItemUIs => itemUIs.Values; 

    public void SetupUI(Inventory inventory, Func<Item, bool> canTransfer) 
    {
        this.inventory = inventory;
        AddAllItems();
        this.inventory.OnItemAdded     += AddItemUI;
        this.inventory.OnItemRemoved   += RemoveItem;
        this.inventory.OnEnableTransfer += OnBeginTransfer;
        this.inventory.OnDisableTransfer += OnCloseTransfer;
    }

    public void OnBeginTransfer()
    {
        ActivateTransfer();
    }

    public void OnCloseTransfer()
    {
        DeactivateTransfer();
    }

    public void AddAllItems()
    {
        for (int i = 0; i < inventory.Items.Count; i++) AddItemUI(inventory.Items[i]);
        opened = true;
    }

    public void RemoveAllItems()
    {
        foreach (var itemUI in AllItemUIs)
        {
            ItemUIPool.Instance.PutObject(itemUI);
        }
    }

    public void AddItemUI(Item item)
    {
        var itemUI = ItemUIPool.Instance.GetObject();
        itemUIs[item] = itemUI;
        itemUI.SetItemUI(item);
        itemUI.transform.SetParent(container.transform);
        if (inventory.LinkedTransferable != null) 
            itemUI.ActivateTransfer(inventory.TransferType, inventory.OnTransfer);
        if (inventory.CanUseItems && item is IUsableItem usableItem) 
            itemUI.SetAction(usableItem.actionName, usableItem.Use);
    }

    public void RemoveItem(Item item)
    {
        if (itemUIs.Remove(item, out var itemUI)) 
        {
            ItemUIPool.Instance.PutObject(itemUI);
        }
    }

    public void ActivateTransfer()
    {
        foreach (var item in AllItemUIs) item.ActivateTransfer(inventory.TransferType, inventory.OnTransfer);
    }

    public void DeactivateTransfer()
    {
        foreach (var item in AllItemUIs) item.DeActivateTransfer();
    }
}
