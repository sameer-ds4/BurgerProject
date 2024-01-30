using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OrdersData", menuName = "ScriptableObjects/OrdersData", order = 4)]
public class Orders : ScriptableObject
{
    public OrderBurger[] orderBurger;
}
