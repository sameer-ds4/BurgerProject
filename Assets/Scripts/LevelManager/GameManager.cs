using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Particle System Data")]
    public ParticleSystemData _ParticleSystemData;

    [Header("Game Component Data")]
    public FoodObjects foodObjects;
    public GridData gridData;

    [Header("Scene Data")]
    public Transform levelSpawnPoint;
    public Transform foodParent;
    public Transform orderBurgerSpawnPoint;

    [Header("Burger Data")]
    public BurgerItem[] burgerItemsList;
    public BurgerItem currentBurgerItem;
    public BurgerItem nextBurgerItem;

    public delegate void UpdateUI();
    public static event UpdateUI UpdateBurgerInfo;
    private void Awake()
    {
        Instance = this;
        InitializeBurgerComps();

    }
    private void OnEnable() 
    {
        PlayerInput.RandomBurger += NextBurger;
    }

    private void OnDisable() 
    {
        PlayerInput.RandomBurger -= NextBurger;
    }

    private void InitializeBurgerComps()
    {
        for (int i = 0; i < burgerItemsList.Length; i++)
        {
            burgerItemsList[i] = Randomize();    
        }
        // currentBurgerItem = BurgerRandomizer.Instance.Randomize();
        // nextBurgerItem = BurgerRandomizer.Instance.Randomize();
    }

    private void NextBurger()
    {
        burgerItemsList[0] = burgerItemsList[1];
        burgerItemsList[1] = burgerItemsList[2];
        burgerItemsList[2] = Randomize();
        UpdateBurgerInfo?.Invoke();
        // currentBurgerItem = nextBurgerItem;
        // nextBurgerItem = BurgerRandomizer.Instance.Randomize();
    }

    public BurgerItem Randomize()
    {
        int x = Random.Range(0, foodObjects.burgerItems.Length - 3);
        BurgerItem currentCompenent = foodObjects.burgerItems[x];
        return currentCompenent;
    }

    // Set target burger from comps
    private void SpawnBurgerOrder()
    {
        int x = 4;
        Instantiate(foodObjects.burgerItems[0].burgerObject, orderBurgerSpawnPoint.position, Quaternion.identity, orderBurgerSpawnPoint);
        //Spawn target burger at the top

        for (int i = 0; i < x; i++)
        {
            BurgerObject orderBurger = Instantiate(foodObjects.burgerItems[Random.Range(1, foodObjects.burgerItems.Length)].burgerObject, orderBurgerSpawnPoint.position, Quaternion.identity, orderBurgerSpawnPoint);
            // orderBurgerSpawnPoint.transform.position += orderBurger.transform.position
        }
    }
}
 
[System.Serializable]
class OrderBvurger
{
    public GameObject[] buns;
    public GameObject[] pattys;
    public GameObject[] cheese;
    public GameObject[] salads;
}