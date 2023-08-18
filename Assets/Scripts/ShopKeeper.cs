using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] List<SellableItem> sellableItems;
    [SerializeField] Inventory inventory;
    [SerializeField] InventoryUIController inventoryUI;

    Interactable interactable;
    bool opened = false;
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteract += OnInteract;
        interactable.OnHide     += CloseShop;
        inventoryUI.SetupUI(inventory);
        foreach (var sellableItem in sellableItems)
        {
            for (int i = 0; i < sellableItem.Quantity; i++)
            {
                inventory.Add(sellableItem.Item);
            }
        }
    }

    void OnInteract()
    {
        PlayerController.Instance.BeginTransfer(this.inventory);
    }

    void CloseShop()
    {
        ShopManagerUI.Instance.HideUI();
    }
}

[System.Serializable]
public class SellableItem 
{
    public Item Item;
    public int Quantity;
}
