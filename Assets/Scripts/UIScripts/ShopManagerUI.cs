using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ShopManagerUI : MonoBehaviour
{
    [SerializeField] GameObject container;
    [SerializeField] ItemUI itemPrefab;

    bool opened = false;
    public bool Opened => opened;

    public  static ShopManagerUI Instance => instance;
    private static ShopManagerUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void ShowShop(List<SellableItem> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            var item = Instantiate(itemPrefab, container.transform);
            item.SetItemUI(items[i].Item, TransferType.Buy, items[i].Price);
        }
        opened = true;
    }

    public void CloseShop()
    {
        for (int i = 0; i < container.transform.childCount; i++)
        {
            Destroy(container.transform.GetChild(i).gameObject);
        }
        opened = false;
    }

}
