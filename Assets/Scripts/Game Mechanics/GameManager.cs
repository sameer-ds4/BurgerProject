using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public BurgerObject everMatter;

	[Header("Particle System Data")]
	public ParticleSystemData _ParticleSystemData;

	[Header("Game Component Data")]
	public FoodObjects foodObjects;
	// public GridData gridData;

	[Header("Managers")]
	public GridManager gridManager;
	public OrderManager orderManager;
	public AudioManager audioManager;

	[Header("Scene Data")]
	public Transform foodParent;
	[HideInInspector] public BurgerItem[] burgerItemsList;

	// Static Members
	public static bool startPlay;
	public static int difficultyIndex;
	
	// EVENTS 
	public delegate void UpdateUI();
	public static event UpdateUI UpdateBurgerInfo;

	private void Awake()
	{
		Instance = this;
	}
	
	private void OnEnable() 
	{
		PlayerInput.RandomBurger += NextBurger;
		ForceMaxFPS();
	}

	private void OnDisable() 
	{
		PlayerInput.RandomBurger -= NextBurger;
	}

	private void Start() 
	{
		ResetParams();
		InitializeAudio();
	}

	private void ResetParams()
	{
		startPlay = false;
		ScoreManager.scoreMain = 0;
		difficultyIndex = 0;
	}

	private void InitializeAudio()
	{
		audioManager.PlayMusic("BG");
	}

	public void TutorialInitialize()
	{
		difficultyIndex = 0;
		gridManager.gridSize = Vector2Int.one * 5;
		gridManager.gameObject.SetActive(true);

		orderManager.maxOrders = 1;
		orderManager.gameObject.SetActive(true);

		for (int i = 0; i < burgerItemsList.Length; i++)
		{
			burgerItemsList[i] = foodObjects.burgerItems[0];
		}
	}

	public void EnableManagers()
	{
		gridManager.gameObject.SetActive(true);
		orderManager.gameObject.SetActive(true);

		InitializeBurgerComps();
	}

	public void InitializeBurgerComps()
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

	private void ForceMaxFPS()
	{
		Application.targetFrameRate = 120;
	}

	public void FlipPower()
	{
		// burgerItemsList[0].burgerObject = everMatter;
		// burgerItemsList[0].image = null;

		burgerItemsList[0] = foodObjects.burgerItems[foodObjects.burgerItems.Length - 1];
	}
}