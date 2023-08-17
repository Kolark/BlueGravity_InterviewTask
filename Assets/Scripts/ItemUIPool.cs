using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        CreateObjects(amount);
    }

    public void CreateObjects(int amount)
    {
        pool = new Queue<ItemUI>(amount);
        for (int i = 0; i < amount; i++)
        {
            PutObject(CreateObject());
        }
    }

    public ItemUI GetObject()
    {
        ItemUI pooleable = pool.Dequeue();
        pooleable.SetActive();
        pooleable.transform.SetParent(null);
        return pooleable;
    }

    public void PutObject(ItemUI @object)
    {
        @object.transform.SetParent(spawnPosition);
        @object.transform.localPosition = Vector3.zero;
        @object.DeActivate();
        pool.Enqueue(@object);
    }

    private ItemUI CreateObject()
    {
        return Instantiate(itemUIPrefab.gameObject, spawnPosition.position, Quaternion.identity).GetComponent<ItemUI>();
    }
}
