using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerRandomizer: MonoBehaviour
{
    public static BurgerRandomizer Instance;

    public List<GameObject> burgerComponents;

    public GameObject currentCompenent;
    int x;

    private void Awake()
    {
        Instance = this;
    }

    public void Randomize()
    {
        x = Random.Range(0, burgerComponents.Count);
        currentCompenent = burgerComponents[x];
    }
}
