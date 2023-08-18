using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] List<ItemDefinition> sellableItems;
    [SerializeField] Inventory inventory;
    [SerializeField] InventoryUIController inventoryUI;

    Interactable interactable;
    bool opened = false;
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteract += OnInteract;
        interactable.OnHide     += CloseShop;
    }

    private void Start()
    {
        foreach (var item in sellableItems)
        {
            inventory.Add(item.GetItem());
        }
    }

    void OnInteract()
    {
        inventoryUI.SetupUI(inventory);
        PlayerController.Instance.BeginTransfer(this.inventory);
    }

    void CloseShop()
    {
        this.inventory.CloseTransfer();
    }
}

[System.Serializable]
public class SellableItem 
{
    public Item Item;
    public int Quantity;
}
