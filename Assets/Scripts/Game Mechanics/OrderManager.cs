using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;
    public Orders ordersData;
    public Transform orderBurgerSpawnpoint;
    private OrderBurger burgerOrder;
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
        burgerOrder = Instantiate(ordersData.orderBurger[0], orderBurgerSpawnpoint.position, orderBurgerSpawnpoint.rotation, orderBurgerSpawnpoint);
    }

    public void CheckMatch(BurgerPart burgerObject)       //Passing the matched type here to check with the main order
    {
        for (int i = 0; i < burgerOrder.burgerParts.Count; i++)
        {
            if(burgerObject == burgerOrder.burgerParts[i])
            {
                burgerOrder.burgerParts.RemoveAt(i);
                break;
            }
        }
    }
}