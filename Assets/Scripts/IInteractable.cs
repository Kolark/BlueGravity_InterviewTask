//Objects that implement this interface are detected by the player
public interface IInteractable
{
    void ShowInteraction(); //Called when approaching an IInteractable
    void HideInteraction(); //Called when you're no longer in range to interact
    void Interact();        //Called when player is in range and wants to interact
}

