using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple pool for ItemUIs to be used accross differente InventoryUI's
public class ItemUIPool : MonoBehaviour, IPool<ItemUI>
{

    [SerializeField] Transform spawnPosition;
    [SerializeField] ItemUI itemUIPrefab;

    private Queue<ItemUI> pool;

    private static ItemUIPool instance = null;
    public static ItemUIPool Instance => instance;

    [SerializeField] int amount;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
        pool = new Queue<ItemUI>(amount);
        CreateObjects(amount);
    }

    public void CreateObjects(int amount)
    {
        for (int i = 0; i < amount; i++) PutObject(CreateObject());
    }

    public ItemUI GetObject()
    {
        //If it fails to Dequeue we make sure to increase the pool size in increments of 5
        if (pool.TryDequeue(out var itemUI))
        {
            itemUI.SetActive();
            itemUI.transform.SetParent(null);
            return itemUI;
        }
        else
        {
            CreateObjects(5);
            return GetObject();
        }
    }

    public void PutObject(ItemUI itemUI)
    {
        itemUI.transform.SetParent(spawnPosition);
        itemUI.transform.localPosition = Vector3.zero;
        itemUI.DeActivate();
        pool.Enqueue(itemUI);
    }

    private ItemUI CreateObject() => Instantiate(itemUIPrefab.gameObject, spawnPosition.position, Quaternion.identity).GetComponent<ItemUI>();
}
