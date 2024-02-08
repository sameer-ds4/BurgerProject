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
	public Transform foodParent;

	[HideInInspector] public BurgerItem[] burgerItemsList;
	// public BurgerItem currentBurgerItem;
	// public BurgerItem nextBurgerItem;

	public static bool startPlay;


	// EVENTS 
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

	private void NextBurger()       // Rotating burger Components coming up UI backend
	{
		burgerItemsList[0] = burgerItemsList[1];
		burgerItemsList[1] = burgerItemsList[2];
		burgerItemsList[2] = Randomize();
		UpdateBurgerInfo?.Invoke();
		// currentBurgerItem = nextBurgerItem;
		// nextBurgerItem = BurgerRandomizer.Instance.Randomize();
	}

	public BurgerItem Randomize()       // Randomizing burger component
	{
		int x = Random.Range(0, foodObjects.burgerItems.Length - 3);
		BurgerItem currentCompenent = foodObjects.burgerItems[x];
		return currentCompenent;
	}

}
 //56083 315338 50000 20000 441463  294231