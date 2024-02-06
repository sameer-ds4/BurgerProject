using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
	public static OrderManager Instance;
		
	[Header("Order Card")]
	private OrderCard currentOrder;
	// private List<OrderCard> orderList;
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
		currentOrder = Instantiate(orderCard, orderCardSpawnpoint.position, Quaternion.identity, orderCardSpawnpoint);      // DisplayCard Spawn
		
		for (int i = 0; i < currentOrder.itemQuantities.Count; i++)
		{
			if (currentOrder.itemQuantities[i].quantity != 0)
				currentOrder.comps.GetChild(i).gameObject.SetActive(true);
				
			currentOrder.itemQuantities[i].numbers.text = currentOrder.itemQuantities[i].quantity.ToString();
		}

		// orderList.Add(currentOrder);
	}

	private void ClearItems(int i)
	{
		if (currentOrder.itemQuantities[i].quantity == 0)
			currentOrder.itemQuantities.RemoveAt(i);
	}

	public void CheckMatch(BurgerPart burgerObject)       //Passing the matched type here to check with the main order
	{
		for (int i = 0; i < currentOrder.itemQuantities.Count; i++)
		{
			if(burgerObject == currentOrder.itemQuantities[i].burgerPart)
			{	
				currentOrder.itemQuantities[i].quantity--;
				currentOrder.itemQuantities[i].numbers.text = currentOrder.itemQuantities[i].quantity.ToString();
				
				ClearItems(i);
					
				Debug.LogError(currentOrder.itemQuantities.Count);
				
				CheckOrderStatus();
			}
		}
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