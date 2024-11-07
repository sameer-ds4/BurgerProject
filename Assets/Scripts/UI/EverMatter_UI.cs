using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EverMatter_UI : MonoBehaviour
{
    public GameObject bgAlpha;
    public Image[] images;
    public Button[] buttons;

    [SerializeField]
    List<BurgerPart> burgerPart_index;
    [SerializeField] private GameManager gameManager;
    [SerializeField]
    Vector3 spawnPoint;
    Vector2Int _slot;


    public delegate void Matchmaking_EM(int x, int y);
	public static event Matchmaking_EM CheckMatch_EM;

	public delegate void RemoveMatch_EM();
	public static event RemoveMatch_EM MatchRemoval_EM;

    private void OnEnable() 
    {
        bgAlpha.SetActive(true);
        GameManager.startPlay = false;    
    }

    public void AssignData(List<BurgerPart> burgerType, Vector3 position, Vector2Int slot)
    {
        burgerPart_index = burgerType;
        spawnPoint = position;
        _slot = slot;
        AssignImages();
    }

    public void AssignImages()
    {
        for (int i = 0; i < burgerPart_index.Count; i++)
        {
            // int index = gameManager.foodObjects.burgerItems[(int)burgerPart_index[i]]
            int index = (int)burgerPart_index[i];
            images[i].sprite = gameManager.foodObjects.burgerItems[index].image;
        }
    }

    public void buttonAction(int i)
    {
        buttons[i].onClick.AddListener(() => SpawnComp((int)burgerPart_index[i]));
    }

    public void SpawnComp(int i)
    {
        BurgerObject burgerSpawned = Instantiate(gameManager.foodObjects.burgerItems[(int)burgerPart_index[i]].burgerObject, spawnPoint, Quaternion.identity, GameManager.Instance.foodParent);
        GridManager.gridFormed[_slot.x, _slot.y] = burgerSpawned;

        CheckMatch_EM?.Invoke(_slot.x, _slot.y);
        MatchRemoval_EM?.Invoke();

        GameManager.startPlay = true;
        bgAlpha.SetActive(false);
        gameObject.SetActive(false);
    }
}
