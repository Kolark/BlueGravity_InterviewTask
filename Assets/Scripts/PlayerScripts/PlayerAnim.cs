using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Handles all clothes worn by the player and its different animations states
public class PlayerAnim : MonoBehaviour
{
    [SerializeField] AnimController playerAnimController;
    public Dictionary<ClothingType , ClothingItem> currentEquiped = new Dictionary<ClothingType, ClothingItem>();
    public Dictionary<ClothingType, AnimController> animControllers = new Dictionary<ClothingType, AnimController>();

    public IEnumerable<AnimController> currentEquipedAnims => animControllers.Values;
    public void SetDirection(Vector2 dir)
    {
        foreach (var a in currentEquipedAnims)
        {
            a.SetDirection(dir);
        }
        playerAnimController.SetDirection(dir);
    }

    public void SetMoving(bool IsMoving) 
    { 
        foreach (var a in currentEquipedAnims) 
            a.SetMoving(IsMoving);
        playerAnimController.SetMoving(IsMoving);
    }

    public void Equip(ClothingItem item)
    {
        if(currentEquiped.TryGetValue(item.ClothingType, out var currentItem))
        {
            currentItem.Use();//By using it again it will unequip it
        }
        currentEquiped[item.ClothingType] = item;
        var animController = Instantiate(item.Anim, this.transform);
        animController.SetColor(item.Color);
        animControllers[item.ClothingType] = animController;

    }

    public void Unequip(ClothingType clothingType)
    {
        if (currentEquiped.Remove(clothingType, out var item)) 
        {
            animControllers.Remove(clothingType, out var instance);
            Destroy(instance.gameObject);
        };
    }
}
