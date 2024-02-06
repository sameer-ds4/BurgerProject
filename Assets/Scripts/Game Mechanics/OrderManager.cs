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


			for (int j = 0; j < orderPlacing.itemQuantities.Count; j++)
			{
				if (orderPlacing.itemQuantities[j].quantity != 0)
					orderPlacing.comps.GetChild(j).gameObject.SetActive(true);

				orderPlacing.itemQuantities[j].numbers.text = orderPlacing.itemQuantities[j].quantity.ToString();
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

					Debug.LogError(currentOrder.itemQuantities.Count);

					// CheckOrderStatus();

					// break;
					goto Lable;
				}
			}
		}
	Lable:;
		
	}
	

	private void CheckOrderStatus()
	{
		if(currentOrder.itemQuantities.Count == 0)
		{
			Debug.LogError("WIN WIN WIN");
			UIManager.Instance.levelComp.SetActive(true);
		}
	}
}