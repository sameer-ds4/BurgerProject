using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderCard : MonoBehaviour
{
    public Image mainBurger;
    public ItemQuantity[] itemQuantities;

}

[System.Serializable]
public class ItemQuantity
{
    public BurgerPart burgerPart;
    public Image itemImg;
    public Text numbers;
}