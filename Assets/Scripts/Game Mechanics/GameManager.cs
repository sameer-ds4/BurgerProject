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
	}

	private void NextBurger()       // Rotating burger Components coming up UI backend
	{
		burgerItemsList[0] = burgerItemsList[1];
		burgerItemsList[1] = burgerItemsList[2];
		burgerItemsList[2] = Randomize();
		UpdateBurgerInfo?.Invoke();
	}

	public BurgerItem Randomize()       // Randomizing burger component
	{
		int x = Random.Range(0, foodObjects.burgerItems.Length - 1);
		BurgerItem currentCompenent = foodObjects.burgerItems[x];
		return currentCompenent;
	}

}