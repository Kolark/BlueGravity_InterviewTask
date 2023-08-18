using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] GameObject container;
    [SerializeField] bool CanUseItems;

    Dictionary<Item, ItemUI> itemUIs = new Dictionary<Item, ItemUI>();
    Inventory inventory;
    bool opened = false;
    public bool Opened => opened;
    public IEnumerable<ItemUI> AllItemUIs => itemUIs.Values;
    public RectTransform rectTransform;
    public Vector2 defaultSizeDelta;
    [SerializeField] Vector2 showSizeDelta;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultSizeDelta = rectTransform.sizeDelta;
    }

    public void SetupUI(Inventory inventory) 
    {
        RemoveAllItems();
        this.inventory = inventory;
        AddAllItems();
        this.inventory.OnItemAdded       += AddItemUI;
        this.inventory.OnItemRemoved     += RemoveItem;
        this.inventory.OnEnableTransfer  += OnBeginTransfer;
        this.inventory.OnDisableTransfer += OnCloseTransfer;
    }

    public void OnBeginTransfer()
    {
        ActivateTransfer();
        ShowUI();
    }

    public void OnCloseTransfer()
    {
        DeactivateTransfer();
        HideUI();
    }

    public void ShowUI()
    {
        this.rectTransform.DOSizeDelta(new Vector2(Screen.width * showSizeDelta.x, Screen.height * showSizeDelta.y), 0.5f).SetEase(Ease.OutBounce);
    }

    public void HideUI()
    {
        this.rectTransform.DOSizeDelta(this.defaultSizeDelta, 0.5f).SetEase(Ease.OutBounce);
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
        if (this.CanUseItems && item is IUsableItem usableItem) 
            itemUI.SetAction(usableItem);
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
