using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
	public static OrderManager Instance;

	public OrdersData ordersData;
	public GridData gridData;
	public OrderCard cardPrefab;
	
	[Header("Order Card")]
	private OrderCard orderPlacing;
	public List<OrderCard> orderList;
	public Transform cardParent;
	public Transform[] cardSpawnPoints;

	///// Orders count managing vars
	public int maxOrders;
	int ordersSpawned;	//Get SpawnIndex
	int burgerIndex;	// Getting burger number to spawn from data

	private void Awake() 
	{
		Instance = this;
	}

	private void Start() 
	{
		if(SaveDataHandler.Instance.saveData.tutorial) return;
		maxOrders = GetMaxOrders();
		OrderPlace();
	}

	//Timers for Oders
	float t1, t2 = 7;


	private void Update() 
	{
		if(!GameManager.startPlay) return;
		if(ordersSpawned > maxOrders) return;

		t1 += Time.deltaTime;

		if(t1 > t2)
		{
			OrderPlace();
			t1 = 0;
			// x++;
			// place = true;
		}
	}

	private int GetMaxOrders()
	{
		int index = GameManager.difficultyIndex;
		int count = gridData.levelDifficulty[index].ordersRange.y;
		return gridData.levelDifficulty[index].ordersRange[ Random.Range(0, count)];
	}
	
	
	public void OrderPlace()
	{
		burgerIndex = ordersData.GetOrderIndex();

		// orderCardSpawnpoint.eulerAngles = new Vector3(90, 0, 0);

		orderPlacing = Instantiate(cardPrefab, cardSpawnPoints[ordersSpawned].position, new Quaternion(0, 0, 0, 0), cardParent);
		// Debug.LogError((orderCardSpawnpoint.transform as RectTransform).position);
		// Debug.LogError((orderCardSpawnpoint.transform as RectTransform).eulerAngles);

		orderPlacing.orderName.text = ordersData.orders[burgerIndex].orderName;

		for (int i = orderPlacing.itemQuantities.Count - 1; i >= 0; i--)
		{
			if(ordersData.orders[burgerIndex].parts[i].quantity == 0)
			{
				ClearItems(i, orderPlacing);
			}
			else
			{
				orderPlacing.comps.GetChild(i).gameObject.SetActive(true);
				orderPlacing.itemQuantities[i].quantity = ordersData.orders[burgerIndex].parts[i].quantity;
				// orderPlacing.itemQuantities[i].numbers.text = ordersData.orders[orderIndex].parts[i].quantity.ToString();
				orderPlacing.itemQuantities[i].numbers.text = ordersData.orders[burgerIndex].parts[i].quantity.ToString();
			}
		}

		ordersSpawned++;

		orderList.Add(orderPlacing);
	}


		// orderIndex++;

		// yOffsetL += new Vector3(0, -300, 0);
		// yOffsetL.anchoredPosition = new Vector3(0,,0);


	// private void PlaceOrder()
	// {
	// 	Vector3 yOffset = Vector3.zero;
	// 	for (int i = 0; i < ordersData.order.Length; i++)
	// 	{
	// 		orderPlacing = Instantiate(ordersData.order[i], orderCardSpawnpoint.position + yOffset, Quaternion.identity, orderCardSpawnpoint);      // DisplayCard Spawn


	// 		for (int j = orderPlacing.itemQuantities.Count - 1; j >= 0; j--)
	// 		{
	// 			if (orderPlacing.itemQuantities[j].quantity != 0)
	// 			{
	// 				orderPlacing.comps.GetChild(j).gameObject.SetActive(true);
	// 				orderPlacing.itemQuantities[j].numbers.text = orderPlacing.itemQuantities[j].quantity.ToString();
	// 			}
	// 			else
	// 				ClearItems(j, orderPlacing);
	// 		}

	// 		orderList.Add(orderPlacing);

	// 		yOffset.y -= 300; // Card offset to spawn each
	// 	}
	// }

	private void ClearItems(int i, OrderCard orderCard)
	{
		if (orderCard.itemQuantities[i].quantity == 0)
			orderCard.itemQuantities.RemoveAt(i);
	}

	[HideInInspector]
	public int orderNo, componentNo, quantity;
	[HideInInspector]
	public ItemQuantity itemQuantity;

	public /*bool*/ GameObject CheckMatch(BurgerPart burgerObject)
	{
		for (int i = 0; i < orderList.Count; i++)
		{
			for (int j = 0; j < orderList[i].itemQuantities.Count; j++)
			{
				if(burgerObject == orderList[i].itemQuantities[j].burgerPart)
				{
					if(orderList[i].itemQuantities[j].quantity == 0) return null;

					orderList[i].itemQuantities[j].quantity--;
					
					// orderList[i].itemQuantities[j].numbers.text = orderList[i].itemQuantities[j].quantity.ToString();

					itemQuantity = orderList[i].itemQuantities[j];
					orderNo = i;
					componentNo = j;

					// quantity = orderList[i].itemQuantities[j].quantity;
					// DOVirtual.DelayedCall(2.5f, () =>
					// {
					// 	UpdateDisplayCount();
					// });

					// Vector3 icon = orderList[i].itemQuantities[j].itemImg.transform.position;
					GameObject icon = orderList[i].itemQuantities[j].itemImg.gameObject;

					// ClearItems(j, orderList[i]);

					// CheckOrderStatus();

					return icon;
					// return true;
				}
			}
		}
		return null;
		// return Vector3.one;
		// return false;
	}
	

	private void CheckOrderStatus()
	{
		int i = orderList.Count - 1;

		while (i >= 0)
		{
			if(orderList[i].itemQuantities.Count == 0)
				ClearOrder_LS(orderList[i], i);

			i--;
		}		
	}

	public void UpdateDisplayCount()
	{
		for (int i = 0; i < orderList[orderNo].itemQuantities.Count; i++)
		{
			if(orderList[orderNo].itemQuantities[i].burgerPart == itemQuantity.burgerPart)
			{
				orderList[orderNo].itemQuantities[i].numbers.text = itemQuantity.quantity.ToString();
			}
		}

		ClearItems(componentNo, orderList[orderNo]);
		CheckOrderStatus();
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

	private void ClearOrder_LS(OrderCard obj, int i)
	{
		DOVirtual.DelayedCall(1.2f, () =>
		{
			obj.Clear();
			orderList.RemoveAt(i);
			CheckForWin();
		});
		// DOVirtual.DelayedCall(1.3f, ()=>
		// {
		// 	// RearrangeOrderCards();
		// });
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
			UIManager.Instance.LevelComplete();
		}
	}
}



	// public void CheckMatch_old(BurgerPart burgerObject)       //Passing the matched type here to check with the main orders
	// {
	// 	bool flag = false;		// Flag to detect single match and exit loop 2
		
	// 	for (int a = 0; a < orderList.Count; a++)
	// 	{

	// 		for (int i = 0; i < orderList[a].itemQuantities.Count; i++)
	// 		{
	// 			if (burgerObject == orderList[a].itemQuantities[i].burgerPart)
	// 			{
	// 				orderList[a].itemQuantities[i].quantity--;

	// 				// UpdateDisplayCount(a, i);	

	// 				ClearItems(i, orderList[a]);

	// 				CheckOrderStatus();

	// 				flag = true;		// Breaking out of both loops as we only deduct one matched component
	// 				break;
	// 			}
	// 		}
			
	// 		if (flag) break;
	// 	}
	// }