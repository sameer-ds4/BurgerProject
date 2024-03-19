using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OrderCard : MonoBehaviour
{
	public Image mainBurger;
	public List<ItemQuantity> itemQuantities;
	public Transform comps;

	public void Clear()
	{
		transform.DOLocalMoveY(1000, 1).OnComplete(() =>
		{ 
			gameObject.SetActive(false); 
		});
	}

	public void Move(float pos)
	{
		transform.DOLocalMoveY(pos, 1);
	}
}

[System.Serializable]
public class ItemQuantity
{
	public BurgerPart burgerPart;
	public Image itemImg;
	public Text numbers;
	public int quantity;
}