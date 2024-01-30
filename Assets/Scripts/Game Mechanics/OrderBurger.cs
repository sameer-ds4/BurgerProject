using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderBurger : MonoBehaviour
{
    [Header("Ordered Burger Components")]
    public List<BurgerPart> burgerParts;
    // public GameObject[] buns;
    // public GameObject[] pattys;
    // public GameObject[] cheese;
    // public GameObject[] salads;

    private void Awake() 
    {
        burgerParts = new();
        for (int i = 0; i < transform.childCount; i++)
        {
            burgerParts.Add(transform.GetChild(i).GetComponent<BurgerObject>().burgerPart);
        }
    }
}
