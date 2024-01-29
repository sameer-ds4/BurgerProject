using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;
    public OrderBurger burgerOrder;
    // private Dictionary<string, GameObject[]> currentOrder;

    private void Awake() 
    {
        Instance = this;
    }

    private void OrderArrange()
    {
        // currentOrder
        
    }

    public void CheckMatch(BurgerPart burgerObject)       //Passing the matched type here to check with the main order
    {
        for (int i = 0; i < burgerOrder.burgerParts.Count; i++)
        {
            if(burgerObject == burgerOrder.burgerParts[i])
            {
                Debug.LogError("nvfkjvjnkd");
                burgerOrder.burgerParts.RemoveAt(i);
                break;
            }
        }
    }
}