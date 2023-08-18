using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] List<Item> sellableItems;
    [SerializeField] Inventory inventory;
    [SerializeField] InventoryUIController inventoryUI;

    Interactable interactable;
    bool opened = false;
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteract += OnInteract;
        interactable.OnHide     += CloseShop;
        inventoryUI.SetupUI(inventory, null);
    }

    private void Start()
    {
        foreach (var item in sellableItems)
        {
            inventory.Add(Instantiate(item, this.transform));
        }
    }

    void OnInteract()
    {
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
