using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Visual representation of a current Inventory state
public class InventoryUIController : MonoBehaviour
{
    [SerializeField] RectTransform container;
    [SerializeField] bool       CanUseItems;
    [SerializeField] Vector2    anchorMaxOpen;
    [SerializeField] Vector2    anchorMinOpen;
    [SerializeField] CanvasScaler canvasScaler;

    RectTransform rectTransform;

    //Paired Inventory
    Inventory inventory;

    //All items in the inventory and their correspondent ItemUI
    Dictionary<Item, ItemUI> itemUIs = new Dictionary<Item, ItemUI>();

    public IEnumerable<ItemUI> AllItemUIs => itemUIs.Values;


    Vector2 defaultAnchorMax;
    Vector2 defaultAnchorMin;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultAnchorMax = rectTransform.anchorMax;
        defaultAnchorMin = rectTransform.anchorMin;
    }

    public void SetupUI(Inventory inventory) 
    {
        //Clean slate
        RemoveAllItems();
        UnpairInventory();

        //Pair incoming inventory
        this.inventory = inventory;
        AddAllItems();
        this.inventory.OnItemAdded       += AddItemUI;
        this.inventory.OnItemRemoved     += RemoveItem;
        this.inventory.OnEnableTransfer  += OnBeginTransfer;
        this.inventory.OnDisableTransfer += OnCloseTransfer;
    }

    //Expands ui and activates all itemUIs transfer button(buy/sell)
    public void OnBeginTransfer()
    {
        ActivateTransfer();
        ShowUI();
    }

    //Retuns ui to original size and Deactives all itemUIs transfer button(buy/sell)
    public void OnCloseTransfer()
    {
        DeactivateTransfer();
        HideUI();
    }

    //Expands ui to desired size
    public void ShowUI()
    {
        this.rectTransform.DOAnchorMax(anchorMaxOpen, 0.5f).SetEase(Ease.OutBounce);
        this.rectTransform.DOAnchorMin(anchorMinOpen, 0.5f).SetEase(Ease.OutBounce);
    }

    //Retuns ui size to its original state
    public void HideUI()
    {
        this.rectTransform.DOAnchorMax(defaultAnchorMax, 0.5f).SetEase(Ease.OutBounce);
        this.rectTransform.DOAnchorMin(defaultAnchorMin, 0.5f).SetEase(Ease.OutBounce);
    }

    //Goes through every item in the paired inventory and adds an ItemUI per item
    public void AddAllItems()
    {
        for (int i = 0; i < inventory.Items.Count; i++) AddItemUI(inventory.Items[i]);
    }

    //Returns all ItemUIs instance to their correspondant ItemUIPool
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
        itemUI.transform.localScale = Vector3.one;
        if (inventory.LinkedTransferable != null) 
            itemUI.ActivateTransfer(inventory.TransferType, inventory.OnTransfer);
        if (this.CanUseItems && item is IUsableItem usableItem) 
            itemUI.SetAction(usableItem);
    }

    //Removes item correspondant ItemUI, and recicles it to their pool
    public void RemoveItem(Item item)
    {
        if (itemUIs.Remove(item, out var itemUI)) 
        {
            itemUI.DeActivateAction();
            itemUI.DeActivateTransfer();
            ItemUIPool.Instance.PutObject(itemUI);
        }
    }

    //Activates Transfer button in all current Item UIs 
    public void ActivateTransfer()
    {
        foreach (var item in AllItemUIs) item.ActivateTransfer(inventory.TransferType, inventory.OnTransfer);
    }

    //Deactivates Transfer button in all current Item UIs 
    public void DeactivateTransfer()
    {
        foreach (var item in AllItemUIs) item.DeActivateTransfer();
    }

    //Stops listening to all paired inventory events and erases its reference
    public void UnpairInventory()
    {
        if(this.inventory != null)
        {
            this.inventory.OnItemAdded       -= AddItemUI;
            this.inventory.OnItemRemoved     -= RemoveItem;
            this.inventory.OnEnableTransfer  -= OnBeginTransfer;
            this.inventory.OnDisableTransfer -= OnCloseTransfer;
            this.inventory = null;
        }
    }
}
