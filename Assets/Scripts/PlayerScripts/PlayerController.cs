using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    IInteractable currentInteraction;
    Rigidbody2D rb;
    bool IsMoving = false;

    Vector2 currentDirection = Vector2.right;
    Vector2 currentInput = Vector2.zero;

    [SerializeField] PlayerClothesManager clothesManager;
    [SerializeField] Inventory inventory;
    [SerializeField] InventoryUIController inventoryUI;
    [SerializeField] Wallet wallet;
    [SerializeField] WalletUI walletUI;

    public PlayerClothesManager ClothingController => clothesManager;
    public static PlayerController Instance => instance;
    private static PlayerController instance = null;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(this);

        rb = GetComponent<Rigidbody2D>();
        inventoryUI.SetupUI(inventory);
        walletUI.SetupUI(wallet);
    }

    void Update()
    {
        GetInputValues();
        SetClothingAnimValues();
        Move();
        CheckInteractions();
    }
    
    //Updates current frame input values, and set IsMoving, currentDirection values according to them
    void GetInputValues()
    {
        if (IsMoving) { currentDirection = currentInput; }

        currentInput.x = Input.GetAxisRaw("Horizontal");
        currentInput.y = Input.GetAxisRaw("Vertical");
        IsMoving = currentInput.magnitude != 0;
    }

    //Give the clothingController the current values for direction and if it's moving
    void SetClothingAnimValues()
    {
        clothesManager.SetDirection(currentDirection);
        clothesManager.SetMoving(IsMoving);
    }

    //Simple movement done through velocity
    void Move() => rb.velocity = currentInput.normalized * 5;

    //Checks for new interaction
    void CheckInteractions()
    {
        //Casts a ray in the current direction
        var rayHit = Physics2D.Raycast(transform.position, currentDirection.normalized, 1f, LayerMask.GetMask("Default"));

        //Checks for an IInteractable object in the case of a raycast hit
        var newInteraction = rayHit ? rayHit.collider.GetComponent<IInteractable>() : null ;

        //Everytime we approach or walk away from an IInteractable, we make sure to show or hide its availability
        if(newInteraction != currentInteraction)
        {
            if(newInteraction != null) newInteraction.ShowInteraction();
            else currentInteraction.HideInteraction();

            currentInteraction = newInteraction;
        }

        //Checks if we can, and want to interact with said IInteractable 
        if (currentInteraction != null && Input.GetKeyDown(KeyCode.E)) currentInteraction.Interact();
    }

    public void BeginTransfer(Inventory inventory)
    {
        this.inventory.EnableTransfer(inventory, SellItem);
        inventory.EnableTransfer(this.inventory, CheckBalance);
    }

    //Checks if wallet has enough balance in order to buy the given item
    bool CheckBalance(Item item) => wallet.TryRemove(item.Price);

    //Returns balance to the wallet, and checks if the item is of clothing type so that it can be unequiped
    bool SellItem(Item item)
    {
        wallet.Add(item.Price);
        if (item is ClothingItem clothingItem) clothesManager.Unequip(clothingItem.ClothingType);
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, currentDirection.normalized);
    }
}
