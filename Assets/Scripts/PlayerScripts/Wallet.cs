using System;
using UnityEngine;

//Simple wallet class that handles ins and out of a currency
public class Wallet : MonoBehaviour
{
    [SerializeField] int currentAmount = 0;
    public int CurrentAmount => currentAmount;

    //Event called when currentAmount changes
    public Action<int> OnChange;

    public void Add(int amount)
    {
        currentAmount += amount;
        OnChange?.Invoke(currentAmount);
    }

    public bool TryRemove(int amount)
    {
        bool hasAmount = currentAmount - amount >= 0;
        if (hasAmount) {currentAmount -= amount; }
        OnChange?.Invoke(currentAmount);
        return hasAmount;
    }
}
