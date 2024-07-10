using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderCard : MonoBehaviour
{
	public Image customerImg;
	public List<ItemQuantity> itemQuantities;
	public Transform comps;
	public GameObject orderInfo;
	public TextMeshProUGUI orderName;

	// [SerializeField] private float maxTime;

	public void Clear()
	{
		// transform.DOLocalMoveY(1000, 1).OnComplete(() =>
		// { 
		// 	gameObject.SetActive(false); 
		// });

		transform.DOLocalMoveX(1000, 1).OnComplete(() =>
		{ 
			gameObject.SetActive(false); 
		});
	}

	public void Move(float pos)
	{
		transform.DOLocalMoveY(pos, 1);
	}

	// private void Update() 
	// {
	// 	maxTime -= Time.deltaTime;
		
	// 	if(maxTime < 0)
	// 	{
			
	// 		Tweening.AlphaFadeOut(gameObject, 0.7f);
	// 		// Tweening.ScaleChange(orderInfo, 1, Vector3.one, Vector3.one * 0);
    //         // timerCirc.fillAmount -= 1/maxTime * Time.deltaTime;  
	// 	}	
	// }

	// private void Timer()
	// {

	// }
}

[System.Serializable]
public class ItemQuantity
{
	public BurgerPart burgerPart;
	public Image itemImg;
	// public Text numbers;
	public TextMeshProUGUI numbers;
	public int quantity;
}