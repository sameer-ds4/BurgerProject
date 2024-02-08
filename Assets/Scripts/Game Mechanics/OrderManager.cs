using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
	public static OrderManager Instance;

	public OrdersData ordersData;
	
	[Header("Order Card")]
	private OrderCard orderPlacing;
	public List<OrderCard> orderList;
	public Transform orderCardSpawnpoint;

	private void Awake() 
	{
		Instance = this;
	}

	private void Start() 
	{
		PlaceOrder();
	}

	private void PlaceOrder()
	{
		for (int i = 0; i < ordersData.order.Length; i++)
		{
			orderPlacing = Instantiate(ordersData.order[i], orderCardSpawnpoint.position, Quaternion.identity, orderCardSpawnpoint);      // DisplayCard Spawn


			for (int j = orderPlacing.itemQuantities.Count - 1; j >= 0; j--)
			{
				if (orderPlacing.itemQuantities[j].quantity != 0)
				{
					orderPlacing.comps.GetChild(j).gameObject.SetActive(true);
					orderPlacing.itemQuantities[j].numbers.text = orderPlacing.itemQuantities[j].quantity.ToString();
				}
				else
					ClearItems(j, orderPlacing);
			}

			orderList.Add(orderPlacing);
		}
	}

	private void ClearItems(int i, OrderCard orderCard)
	{
		if (orderCard.itemQuantities[i].quantity == 0)
			orderCard.itemQuantities.RemoveAt(i);
	}

	public void CheckMatch(BurgerPart burgerObject)       //Passing the matched type here to check with the main orders
	{
		bool flag = false;
		
		for (int a = 0; a < orderList.Count; a++)
		{

			for (int i = 0; i < orderList[a].itemQuantities.Count; i++)
			{
				if (burgerObject == orderList[a].itemQuantities[i].burgerPart)
				{
					orderList[a].itemQuantities[i].quantity--;
					orderList[a].itemQuantities[i].numbers.text = orderList[a].itemQuantities[i].quantity.ToString();

					ClearItems(i, orderList[a]);

					CheckOrderStatus();

					flag = true;		// Breaking out of both loops as we only deduct one matched component
					break;
				}
			}
			
			if (flag) break;
		}
	}
	

	private void CheckOrderStatus()
	{
		int i = orderList.Count - 1;
		while (i >= 0)
		{
			// Debug.LogError("fnjfvbkn");
			if(orderList[i].itemQuantities.Count == 0)
			{
				Debug.Log("REMOVE order");
				orderList[i].gameObject.SetActive(false);
				orderList.RemoveAt(i);
			}
			// Debug.LogError(orderList[i].itemQuantities.Count);
			i--;
		}
		
		if(orderList.Count == 0)
		{
			Debug.LogError("WIN WIN WIN");
			UIManager.Instance.levelComp.SetActive(true);
		}		
	}
}