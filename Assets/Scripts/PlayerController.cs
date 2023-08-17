using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    bool IsMoving = false;

    Vector2 currentDirection = Vector2.right;
    Vector2 currentInput = Vector2.zero;
    AnimController bodyAnim;

    public static PlayerController Instance => instance;

    private static PlayerController instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        rb       = GetComponent<Rigidbody2D>();
        bodyAnim = GetComponentInChildren<AnimController>();
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

        bodyAnim.SetDirection(currentDirection);
        bodyAnim.SetMoving(IsMoving);

        rb.velocity = currentInput.normalized * 5;
        Debug.Log("Move");
    }

    IInteractable currentInteraction;

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, currentDirection.normalized);
    }
}

public class ClothingManager
{
    public List<AnimController> animControllers = new List<AnimController>();
    public void SetDirection(Vector2 dir)
    {
        foreach (var a in animControllers)
        {
            a.SetDirection(dir);
        }
    }

    public void SetMoving(bool IsMoving) { foreach (var a in animControllers) a.SetMoving(IsMoving); }

    public void Equip()
    {

    }

    public void Unequip()
    {

    }
}

public class Inventory
{
    public List<Item> items = new List<Item>();

    public void Add(Item item)
    {
        items.Add(item);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }
}