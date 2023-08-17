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

    public static PlayerController Instance => instance;

    private static PlayerController instance = null;

    [SerializeField] PlayerAnim clothingController;
    [SerializeField] InventoryUIController inventoryUIController;

    public InventoryUIController InventoryUIController => inventoryUIController;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
        CheckInteractions();
    }

    void Move()
    {
        if (IsMoving) { currentDirection = currentInput; }

        currentInput.x = Input.GetAxisRaw("Horizontal");
        currentInput.y = Input.GetAxisRaw("Vertical");
        IsMoving       = currentInput.magnitude != 0;

        clothingController.SetDirection(currentDirection);
        clothingController.SetMoving(IsMoving);

        rb.velocity = currentInput.normalized * 5;
    }



    void CheckInteractions()
    {
        var rayHit = Physics2D.Raycast(transform.position, currentDirection.normalized, 1f, LayerMask.GetMask("Default"));

        var newInteraction = rayHit ? rayHit.collider.GetComponent<IInteractable>() : null ;

        if(newInteraction != currentInteraction)
        {
            if(newInteraction != null) newInteraction.ShowInteraction();
            else currentInteraction.HideInteraction();

            currentInteraction = newInteraction;
        }

        if (currentInteraction != null && Input.GetKeyDown(KeyCode.E))
        {
            currentInteraction.Interact();
        }
    }

    public void Add(Item item) 
    {
        inventoryUIController.AddItem(item);
        //inventoryUIController.ActivateTransfer(TransferType.Sell, )
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, currentDirection.normalized);
    }
}
