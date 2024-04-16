using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Used for Order card spawning and populating

[CreateAssetMenu(fileName = "OrdersData", menuName = "ScriptableObjects/OrdersData", order = 4)]
public class OrdersData : ScriptableObject
{
	public Order[] orders;
	// public Recipe[] items;
	// [SerializeField] public OrderCard[] order;

	public int GetOrderIndex()
	{
		return Random.Range(0, orders.Length);
	}
}


[System.Serializable]
public class Order
{
	public string orderName;
	public Recipe[] parts;
}

[System.Serializable]
public class Recipe
{
	public BurgerPart burgerPart;
	public int quantity;
}