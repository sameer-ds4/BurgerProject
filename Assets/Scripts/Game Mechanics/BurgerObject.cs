using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        transform.DORotate(new Vector3(0, 360, 0), 6, RotateMode.FastBeyond360)
        .SetLoops(-1, LoopType.Restart)
        .SetRelative()
        .SetEase(Ease.Linear);
    }

}
