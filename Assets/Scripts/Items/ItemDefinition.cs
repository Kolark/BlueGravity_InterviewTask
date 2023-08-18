using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Base Item", menuName = "Items/Create Base Item")]
public class ItemDefinition : ScriptableObject
{
    public Sprite UIsprite;
    public Color Color;
    public int Price;
    public virtual Item GetItem() => new Item(UIsprite, Color, Price);
}


public class Item
{
    public Sprite UIsprite;
    public Color Color;
    public int Price;

    public Item(Sprite uIsprite, Color color, int price)
    {
        UIsprite = uIsprite;
        Color = color;
        Price = price;
    }
}

