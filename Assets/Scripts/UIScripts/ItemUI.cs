using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    Item item;
    [SerializeField] Image itemImage;

    [SerializeField] Button   transferButton;
    [SerializeField] TextMeshProUGUI transferTypeText;
    [SerializeField] TextMeshProUGUI priceText;

    [SerializeField] Button actionButton;
    [SerializeField] TextMeshProUGUI actionText;

    public Action OnTransfer;
    public Action OnAction;

    private void Awake()
    {
        transferButton.onClick.AddListener(TransferButtonPressed);
        actionButton.onClick.AddListener(ActionButtonPressed);
    }

    public void SetItemUI(Item item, string actionText = null)
    {
        actionButton.gameObject.SetActive(actionText != null);
        this.actionText.text = actionText;
        itemImage.enabled    = true;
        itemImage.sprite     = item.UIsprite;
        this.item = item;
    }

    public void SetItemUI(Item item, TransferType transferType, int amount, string actionText = null)
    {
        SetItemUI(item, actionText);
        transferButton.gameObject.SetActive(true);
        transferTypeText.text = transferType.ToString();
        priceText.text        = amount.ToString();
    }

    public void TransferButtonPressed() => OnTransfer?.Invoke();
    public void ActionButtonPressed()   => OnAction.Invoke();

    private void OnDestroy()
    {
        transferButton.onClick.RemoveAllListeners();
        actionButton.onClick.RemoveAllListeners();
    }
}

public enum TransferType
{
    Buy,
    Sell
}