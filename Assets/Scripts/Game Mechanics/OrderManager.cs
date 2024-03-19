using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
	public static OrderManager Instance;

	public OrdersData ordersData;
	public OrderCard cardPrefab;
	
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
		// PlaceOrder();
		OrderPlace();
	}

	float t1, t2 = 7;
	bool place;
	private void Update() 
	{
		if(!GameManager.startPlay) return;

		t1 += Time.deltaTime;

		if(t1 > t2 && ! place)
		{
			OrderPlace();
			place = true;
		}
	}

	Vector3 yOffsetL;
	int orderIndex;
	private void OrderPlace()
	{
		orderPlacing = Instantiate(ordersData.order[orderIndex], orderCardSpawnpoint.position+ yOffsetL, Quaternion.identity, orderCardSpawnpoint);

		for (int i = orderPlacing.itemQuantities.Count - 1; i >= 0; i--)
		{
			if(orderPlacing.itemQuantities[i].quantity != 0)
			{
				orderPlacing.comps.GetChild(i).gameObject.SetActive(true);
				orderPlacing.itemQuantities[i].numbers.text = orderPlacing.itemQuantities[i].quantity.ToString();	
			}
			else
			ClearItems(i, orderPlacing);
		}

		orderIndex++;
		orderList.Add(orderPlacing);

		yOffsetL.y -= 300;
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
		bool flag = false;		// Flag to detect single match and exit loop 2
		
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
			if(orderList[i].itemQuantities.Count == 0)
				ClearOrder(orderList[i], i);

			i--;
		}		
	}

	private void ClearOrder(OrderCard obj, int i)
	{
		DOVirtual.DelayedCall(1.2f, () =>
		{
			obj.Clear();
			orderList.RemoveAt(i);
			CheckForWin();
		});
		DOVirtual.DelayedCall(1.3f, ()=>
		{
			RearrangeOrderCards();
		});
	}

	private void RearrangeOrderCards()
	{
		if(orderList.Count > 0)
		{
			orderList[0].Move(0);
		}
	}

	private void CheckForWin()
	{
		if(orderList.Count == 0)
		{
			Debug.LogError("WIN WIN WIN");
			UIManager.Instance.levelComp.SetActive(true);
		}
	}
}