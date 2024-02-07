using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
	public static OrderManager Instance;

	public OrdersData ordersData;
	
	[Header("Order Card")]
	private OrderCard currentOrder;
	private OrderCard orderPlacing;
	public List<OrderCard> orderList;
	public OrderCard orderCard;
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
					orderPlacing.itemQuantities.RemoveAt(j);
			}

			orderList.Add(orderPlacing);
		}
	}

	private void ClearItems(int i)
	{
		if (currentOrder.itemQuantities[i].quantity == 0)
			currentOrder.itemQuantities.RemoveAt(i);
	}

	public void CheckMatch(BurgerPart burgerObject)       //Passing the matched type here to check with the main order
	{
		bool flag = false;
		
		for (int a = 0; a < orderList.Count; a++)
		{
			currentOrder = orderList[a];

			for (int i = 0; i < currentOrder.itemQuantities.Count; i++)
			{
				if (burgerObject == currentOrder.itemQuantities[i].burgerPart)
				{
					currentOrder.itemQuantities[i].quantity--;
					currentOrder.itemQuantities[i].numbers.text = currentOrder.itemQuantities[i].quantity.ToString();

					ClearItems(i);

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
			Debug.LogError("fnjfvbkn");
			if(orderList[i].itemQuantities.Count == 0)
			{
				Debug.Log("REMOVE order");
				orderList[i].gameObject.SetActive(false);
				orderList.RemoveAt(i);
			}
			Debug.LogError(orderList[i].itemQuantities.Count);
			i--;
		}
		
		if(orderList.Count == 0)
		{
			Debug.LogError("WIN WIN WIN");
			UIManager.Instance.levelComp.SetActive(true);
		}		
		
	}
}