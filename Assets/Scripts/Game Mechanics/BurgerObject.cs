using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BurgerObject : MonoBehaviour
{
    public bool isStatic;
    public BurgerPart burgerPart;
    // public Sprite iconPart;
    
    private void OnEnable() 
    {
        Tweening.TweenIn_gameObject(this.gameObject, 0.3f, Vector3.one * 0.3f, Vector3.one * 1);    
    }

    void Start()
    {
        if (isStatic) return;

        // transform.DORotate(new Vector3(0, 360, 0), 6, RotateMode.FastBeyond360)
        // .SetLoops(-1, LoopType.Restart)
        // .SetRelative()
        // .SetEase(Ease.Linear);
    }

    private void OnDestroy() 
    {
        transform.DOKill(false);
    }
}

public enum BurgerPart
{ 
    bun,
    lettuce,
    patty,
    cheese,
    bacon,
    onion,
    pickle,
    bomb
}