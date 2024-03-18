using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
		Vector3 yOffset = Vector3.zero;
		for (int i = 0; i < ordersData.order.Length; i++)
		{
			orderPlacing = Instantiate(ordersData.order[i], orderCardSpawnpoint.position + yOffset, Quaternion.identity, orderCardSpawnpoint);      // DisplayCard Spawn


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

			yOffset.y -= 300; // Card offset to spawn each
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
		Debug.LogError(i);
		while (i >= 0)
		{
			if(orderList[i].itemQuantities.Count == 0)
			{
				orderList[i].Clear();
				ClearOrder(orderList[i].gameObject, i);
				// orderList[i].gameObject.SetActive(false);
				// orderList.RemoveAt(i);
			}
			i--;
		}
		
		if(orderList.Count == 0)
		{
			Debug.LogError("WIN WIN WIN");
			UIManager.Instance.levelComp.SetActive(true);
		}		
	}

	private void ClearOrder(GameObject obj, int i)
	{
		// Tweening.TweenMove(obj, 1f, new Vector2(0, 500), (transform as RectTransform).position);
		DOVirtual.DelayedCall(1.2f, () =>
		{
			obj.SetActive(false);
			orderList.RemoveAt(i);
		});
	}
}