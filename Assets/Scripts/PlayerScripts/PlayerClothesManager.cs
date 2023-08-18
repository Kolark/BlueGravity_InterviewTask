using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Handles all clothes worn by the player and its different animations states according to the player movement
public class PlayerClothesManager : MonoBehaviour
{
    [SerializeField] PlayerAnimController playerAnimController; //the player body itself

    //Curent clothing pieces worn by the player
    public Dictionary<ClothingType , ClothingItem> currentEquiped = new Dictionary<ClothingType, ClothingItem>();
    public Dictionary<ClothingType, PlayerAnimController> animControllers = new Dictionary<ClothingType, PlayerAnimController>();

    public IEnumerable<PlayerAnimController> currentEquipedAnims => animControllers.Values;

    //Sets direction parameters to all current clothes and the player body itself
    public void SetDirection(Vector2 dir)
    {
        foreach (var a in currentEquipedAnims) a.SetDirection(dir);
        playerAnimController.SetDirection(dir);
    }

    //Sets moving parameters to all current clothes and the player body itself
    public void SetMoving(bool IsMoving) 
    { 
        foreach (var a in currentEquipedAnims) a.SetMoving(IsMoving);
        playerAnimController.SetMoving(IsMoving);
    }

    //Equips clothing item, this also means instatiating its prefab
    public void Equip(ClothingItem item)
    {
        if(currentEquiped.TryGetValue(item.ClothingType, out var currentItem)) currentItem.Use();//By using it again it will unequip it
        
        currentEquiped[item.ClothingType]  = item;
        var animController                 = Instantiate(item.Anim, this.transform);
        animControllers[item.ClothingType] = animController;
        animController.SetColor(item.Color);

    }

    //Removes piece of clothing and destroys its prefab instance
    public void Unequip(ClothingType clothingType)
    {
        if (currentEquiped.Remove(clothingType, out var item)) 
        {
            item.IsEquipped = false;
            animControllers.Remove(clothingType, out var instance);
            Destroy(instance.gameObject);
        }
    }
}
