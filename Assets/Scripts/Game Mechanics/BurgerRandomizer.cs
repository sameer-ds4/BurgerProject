using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerRandomizer: MonoBehaviour
{
    public static BurgerRandomizer Instance;

    public FoodObjects foodObjects;
    public BurgerItem currentBurgerItem;
    public BurgerItem nextBurgerItem;


    // public BurgerObject currentCompenent;
    int x;

    private void Awake()
    {
        Instance = this;
        // foodObjects = GameManager.Instance.foodObjects;
    }

    public BurgerItem Randomize()
    {
        x = Random.Range(0, foodObjects.burgerItems.Length);
        BurgerItem currentCompenent = foodObjects.burgerItems[x];
        return currentCompenent;
    }

}
