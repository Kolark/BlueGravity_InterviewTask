using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary> Class that defines and allows us to controll all of the ui components that an Item may have, this includes its image, buttons and other </summary>
public class ItemUI : MonoBehaviour, IPooleable
{
    Item item; //Item related to this UI handler

    public Item Item => item;

    [SerializeField] Image itemImage;

    [SerializeField] Button transferButton; //It refers to the buy/sell buttons
    [SerializeField] Button actionButton;   //It defines a general purpose button

    //Text UI components asociated with the previous buttons
    [SerializeField] TextMeshProUGUI transferTypeText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI actionText;

    //Events that other scripts can listen to when transfer and action buttons are pressed
    public Action<ItemUI> OnTransfer;
    public Action<ItemUI> OnAction;

    //Sets the image of the item, and the reference to the item linked to this instance
    public void SetItemUI(Item item)
    {
        itemImage.enabled    = true;
        itemImage.sprite     = item.UIsprite;
        this.item = item;
    }

    //Activates the transfer button with its correspondent transfer type
    public void ActivateTransfer(TransferType transferType, Action<ItemUI> onTransfer)
    {
        transferButton.gameObject.SetActive(true);
        transferTypeText.text = transferType.ToString();
        priceText.text = item.Price.ToString();
        OnTransfer    += onTransfer;
    }

    //No longer allows it to be transferred(bought or sold)
    public void DeActivateTransfer()
    {
        transferButton.gameObject.SetActive(false);
        transferTypeText.text = null;
        priceText.text        = null;
    }

    //Activates the button action
    public void ActivateAction(string useText)
    {
        actionButton.gameObject.SetActive(true);
        actionText.text = useText;
    }

    //Deactivates the button action
    public void DeActivateAction()
    {
        actionButton.gameObject.SetActive(false);
    }

    //Event handlers
    public void TransferButtonPressed() => OnTransfer?.Invoke(this);
    public void ActionButtonPressed()   => OnAction.Invoke(this);

    #region IPooleable
    public void SetActive() 
    {
        gameObject.SetActive(true);
    }

    //Leaves it was first instantiated
    public void DeActivate() 
    {
        transferButton.onClick.RemoveAllListeners();
        actionButton.onClick.RemoveAllListeners();

        itemImage.enabled = false;
        itemImage.sprite  = null;
        transferTypeText.text = null;
        priceText.text        = null;
        actionText.text       = null;
        gameObject.SetActive(false);
    }
    #endregion

    private void OnDestroy()
    {
        transferButton.onClick.RemoveAllListeners();
        actionButton.onClick.RemoveAllListeners();
    }
}

//Current defined type of transfers(for this demo)
public enum TransferType
{
    Buy,
    Sell
}