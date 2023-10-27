using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BurgerObject : MonoBehaviour
{
    public BurgerPart burgerPart;
    
    void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), 6, RotateMode.FastBeyond360)
        .SetLoops(-1, LoopType.Restart)
        .SetRelative()
        .SetEase(Ease.Linear);
    }
}

public enum BurgerPart
{ 
    bun,
    lettuce,
    patty,
    cheese
}