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

    private void Start()
    {
    }

    private void InitializeBurgerComps()
    {
        for (int i = 0; i < burgerItemsList.Length; i++)
        {
            burgerItemsList[i] = BurgerRandomizer.Instance.Randomize();    
        }
        // currentBurgerItem = BurgerRandomizer.Instance.Randomize();
        // nextBurgerItem = BurgerRandomizer.Instance.Randomize();
    }

    private void NextBurger()
    {
        burgerItemsList[0] = burgerItemsList[1];
        burgerItemsList[1] = burgerItemsList[2];
        burgerItemsList[2] = BurgerRandomizer.Instance.Randomize();
        UpdateBurgerInfo?.Invoke();
        // currentBurgerItem = nextBurgerItem;
        // nextBurgerItem = BurgerRandomizer.Instance.Randomize();
    }

    
}
