using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;
    public OrdersData ordersData;
    public Transform orderBurgerSpawnpoint;
    private List<Order> orders;
    private OrderBurger burgerOrder;
    private Order currentOrder;

    public OrderCard orderCard;
    public List<OrderCard> orderCards;
    // private Dictionary<string, GameObject[]> currentOrder;

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

    }

    private void PlaceOrderSH()
    {
        // currentOrder.orderBurger = Instantiate(ordersData.orderBurger[0], orderBurgerSpawnpoint.position, orderBurgerSpawnpoint.rotation, orderBurgerSpawnpoint);
        // currentOrder.orderCard = Instantiate(ordersData.)
        orders.Add(currentOrder);
        UIManager.Instance.PlaceOrderCard();
    }

    private void PlaceOrderdd()
    {
        // burgerOrder = Instantiate(ordersData.orderBurger[0], orderBurgerSpawnpoint.position, orderBurgerSpawnpoint.rotation, orderBurgerSpawnpoint);
        UIManager.Instance.PlaceOrderCard();
    }




    public void CheckMatch(BurgerPart burgerObject)       //Passing the matched type here to check with the main order
    {
        for (int i = 0; i < burgerOrder.burgerParts.Count; i++)
        {
            if(burgerObject == burgerOrder.burgerParts[i])
            {
                burgerOrder.burgerParts.RemoveAt(i);
                CheckOrderStatus();
                break;
            }
        }
    }

    private void CheckOrderStatus()
    {
        if(burgerOrder.burgerParts.Count == 0)
        {
            Debug.LogError("WIN WIN WIN");
            UIManager.Instance.levelComp.SetActive(true);
        }
    }
}