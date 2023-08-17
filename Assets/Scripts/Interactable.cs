using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    
    public Action OnInteract;
    public Action OnShow;
    public Action OnHide;

    //Event Triggers from IInteratable interface
    public void HideInteraction() => OnHide?.Invoke();
    public void ShowInteraction() => OnShow?.Invoke();
    public void Interact()        => OnInteract?.Invoke();
}
