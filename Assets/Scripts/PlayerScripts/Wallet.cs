using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] int currentAmount = 0;
    public int CurrentAmount => currentAmount;

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
