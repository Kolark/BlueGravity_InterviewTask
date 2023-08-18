using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Defines an inventory class that implements an ITransferable of type Item
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