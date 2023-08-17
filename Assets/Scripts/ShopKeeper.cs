using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] List<SellableItem> sellableItems; 
    Interactable interactable;
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteract += OpenShop;
        interactable.OnHide     += CloseShop;
    }

    void OpenShop()
    {
        if (!ShopManagerUI.Instance.Opened)
        {
            ShopManagerUI.Instance.ShowShop(sellableItems);
        }
    }

    void CloseShop()
    {
        ShopManagerUI.Instance.CloseShop();
    }
}

[System.Serializable]
public class SellableItem 
{
    public Item Item;
    public int Price;
    public int Quantity;
}
