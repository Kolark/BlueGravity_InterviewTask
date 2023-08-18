using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] List<ItemDefinition> sellableItems; //Items that will fill the inventory
    [SerializeField] Inventory inventory;
    [SerializeField] InventoryUIController inventoryUI;

    Interactable interactable;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteract += OnInteract;
        interactable.OnHide     += CloseShop;
    }

    private void Start()
    {
        //From the sellable items list we first fill its inventory
        foreach (var item in sellableItems) 
            inventory.Add(item.GetItem());
    }

    void OnInteract()
    {
        //Pair the inventory ui to our inventory data
        inventoryUI.SetupUI(inventory);

        //Begin Transfer between players inventory and this ones
        PlayerController.Instance.BeginTransfer(this.inventory);
    }

    //Closes transfer and unpairs inventoryUI from inventory
    void CloseShop()
    {
       this.inventory.CloseTransfer();
       this.inventoryUI.UnpairInventory();
    }
}