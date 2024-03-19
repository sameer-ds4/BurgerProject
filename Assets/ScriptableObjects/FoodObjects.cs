using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for 3D food components placing and matching in the game

[CreateAssetMenu(fileName = "FoodObjects", menuName = "ScriptableObjects/FoodObjects", order = 2)]  
public class FoodObjects : ScriptableObject
{
    public BurgerItem[] burgerItems;
}


[System.Serializable]
public class BurgerItem
{
    public BurgerObject burgerObject;
    public Sprite image;
}