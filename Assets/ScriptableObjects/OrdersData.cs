using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OrdersData", menuName = "ScriptableObjects/OrdersData", order = 4)]
public class OrdersData : ScriptableObject
{
    public OrderCard[] order;
}


[System.Serializable]
public class Order
{
    public string orderName;
    public Recipe[] quantities;
}

[System.Serializable]
public class Recipe
{
    public BurgerPart burgerPart;
    public int quantity;
}