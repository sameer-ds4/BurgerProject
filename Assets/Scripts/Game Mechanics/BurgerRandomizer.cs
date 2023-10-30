using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerRandomizer: MonoBehaviour
{
    public static BurgerRandomizer Instance;

    public FoodObjects foodObjects;
    public BurgerItem currentBurgerItem;
    public BurgerItem nextBurgerItem;


    private void Awake()
    {
        Instance = this;
    }

    public BurgerItem Randomize()
    {
        int x = Random.Range(0, foodObjects.burgerItems.Length);
        BurgerItem currentCompenent = foodObjects.burgerItems[x];
        return currentCompenent;
    }

}
