using System;

//Implements and Item that can be "used", meaning it has an effect/action
public interface IUsableItem
{
    string actionName { get; }
    void Use();
    Action<IUsableItem> OnUse { get; set; }
}