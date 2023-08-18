using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalletUI : MonoBehaviour
{
    Wallet wallet;
    [SerializeField] TextMeshProUGUI textAmount;

    public void SetupUI(Wallet wallet)
    {
        this.wallet = wallet;
        textAmount.text = wallet.CurrentAmount.ToString();
        wallet.OnChange += OnWalletAmountChanged;
    }

    public void OnWalletAmountChanged(int currentAmount)
    {
        textAmount.text = wallet.CurrentAmount.ToString();
    }
}
