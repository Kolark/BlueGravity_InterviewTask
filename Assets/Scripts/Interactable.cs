using System;
using UnityEngine;

//Default IInteractable handler, it provides callbacks to when its being interacted with
public class Interactable : MonoBehaviour, IInteractable
{
    //Interactable Events
    public Action OnInteract;
    public Action OnShow;
    public Action OnHide;

    //Event Triggers from IInteratable interface
    public void HideInteraction() => OnHide?.Invoke();
    public void ShowInteraction() => OnShow?.Invoke();
    public void Interact()        => OnInteract?.Invoke();
}
