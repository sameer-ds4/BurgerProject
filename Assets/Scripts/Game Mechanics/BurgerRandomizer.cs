using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerRandomizer: MonoBehaviour
{
    public static BurgerRandomizer Instance;

    public FoodObjects foodObjects;
    public BurgerItem currentBurgerItem;
    public BurgerItem nextBurgerItem;

    public List<BurgerObject> burgerComponents;

    // public BurgerObject currentCompenent;
    int x;

    private void Awake()
    {
        Instance = this;
    }

    private void Start() 
    {
        currentBurgerItem = Randomize();
        nextBurgerItem = Randomize();
    }
    public BurgerItem Randomize()
    {
        x = Random.Range(0, foodObjects.burgerItems.Length);
        BurgerItem currentCompenent = foodObjects.burgerItems[x];
        return currentCompenent;
    }

    private void NextBurgerComps()
    {
        currentBurgerItem = nextBurgerItem;
        nextBurgerItem = Randomize();
    }
}
