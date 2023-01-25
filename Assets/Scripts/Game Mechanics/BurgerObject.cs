using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerObject : MonoBehaviour
{
    [System.Serializable]
    public class Burger
    {
        public BurgerPart burgerPart;

        public bool combineSame;
        public bool combineNext;
        public int row, column;
    }

    public Burger burger;
    // Start is called before the first frame update
    void Start()
    {
        //burger.row = GameManager.Instance.Burger.Values(this.gameObject);
    }

}
