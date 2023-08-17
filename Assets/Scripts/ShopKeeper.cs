using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] List<SellableItem> sellableItems; 
    Interactable interactable;
    bool opened = false;
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
            ShopManagerUI.Instance.OpenUI(sellableItems.Select(i => i.Item).ToList());
        }
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
