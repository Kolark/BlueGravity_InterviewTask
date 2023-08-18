using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, ITransferable<Item>
{
    [SerializeField] TransferType transferType;

    public TransferType TransferType => transferType;

    private List<Item> items = new List<Item>();
    public List<Item> Items => items;

    public Func<Item, bool> transferCheck = null;
    public Func<Item, bool> defaultTransferCheck = (I) => true;

    private bool canTransfer = false;
    public bool CanTransfer => canTransfer;

    private ITransferable<Item> linkedTransferable = null;
    public ITransferable<Item> LinkedTransferable { get => linkedTransferable; set => linkedTransferable = value; }

    public Action<Item> OnItemAdded;
    public Action<Item> OnItemRemoved;
    public Action OnEnableTransfer;
    public Action OnDisableTransfer;

    public void Add(Item item)
    {
        items.Add(item);
        OnItemAdded?.Invoke(item);
    }

    public void Remove(Item item)
    {
        if (items.Remove(item)) OnItemRemoved?.Invoke(item);
    }


    public void EnableTransfer(ITransferable<Item> transferable, Func<Item, bool> transferRequirement = null)
    {
        OnEnableTransfer?.Invoke();
        linkedTransferable = transferable;
        this.transferCheck = transferRequirement ?? defaultTransferCheck;
    }

    public void OnTransfer(Item item)
    {
        if (this.transferCheck(item))
        {
            Remove(item);
            LinkedTransferable.Add(item);
        }
    }

    public void CloseTransfer()
    {
        if (linkedTransferable != null) 
        {
            var l = linkedTransferable;
            linkedTransferable = null;
            OnDisableTransfer?.Invoke();
            l.CloseTransfer();
        } 
    }
}

//InventoryUI architecture goes into Inventory

//now InventoryUI only listens to changes in Inventory
//InventoryUI cares only about inventory

//When you interact you begin a transfer between two inventories, my inv -> sell, other inv-> buy

//When you begin a transfer there are some requirements that must be fullfiled

//Wallet, Wallet UI

//Begin transfer in shop keeper, the one whose whole deal is to transfer ! interact -> shop keeper -> player.beginTransfer -> 2 ITransferables -> 2 inve who then alert their correspondent UIs that a transfer is starting, one of those is the player, or maybe begin transfer receives a parameter whether its buy or sell, in which case the player will choose sell
//Then back to the player who begins the transfer between him and something else
//Always between player and some ITransferable stuff

//Transfer in and transfer out also in interface