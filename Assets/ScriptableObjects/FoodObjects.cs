using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// Used for 3D food components placing and matching in the game

[CreateAssetMenu(fileName = "FoodObjects", menuName = "ScriptableObjects/FoodObjects", order = 2)]
public class FoodObjects : ScriptableObject
{
    [Description("Make sure to assign in order Bun - Lettuce - Patty - Cheese - Bacon - Pickle - Bomb - EverMatter")]
    public BurgerItem[] burgerItems;
}


[System.Serializable]
public class BurgerItem
{
    public BurgerObject burgerObject;
    public Sprite image;
}