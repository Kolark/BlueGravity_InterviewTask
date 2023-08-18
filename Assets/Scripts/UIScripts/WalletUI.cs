using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Simple UI handler for wallet class
public class WalletUI : MonoBehaviour
{
    Wallet wallet;
    [SerializeField] TextMeshProUGUI textAmount;

    //Pairs wallet events to its methods
    public void SetupUI(Wallet wallet)
    {
        this.wallet = wallet;
        textAmount.text = wallet.CurrentAmount.ToString();
        wallet.OnChange += OnWalletAmountChanged;
    }

    public void OnWalletAmountChanged(int currentAmount) => textAmount.text = wallet.CurrentAmount.ToString();
}
