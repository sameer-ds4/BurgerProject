using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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