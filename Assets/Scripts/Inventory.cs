using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, ITransferable<Item>
{
    private List<Item> items = new List<Item>();
    public List<Item> Items => items;

    private bool canTransfer = false;
    public bool CanTransfer => canTransfer;

    public bool canTransferIN;
    public bool canTransferOUT;

    public Action<Item> OnItemAdded;
    public Action<Item> OnItemRemoved;

    public void Add(Item item)
    {
        items.Add(item);
        OnItemAdded?.Invoke(item);
    }

    public void Remove(Item item)
    {
        if (items.Remove(item)) OnItemRemoved?.Invoke(item);
    }
}

//InventoryUI architecture goes into Inventory

//now InventoryUI only listens to changes in Inventory
//InventoryUI cares only about inventory

//When you interact you begin a transfer between two inventories, my inv -> sell, other inv-> buy

//When you begin a transfer there are some requirements that must be fullfiled

//Wallet, Wallet UI