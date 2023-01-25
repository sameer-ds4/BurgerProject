using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tweening : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //---------------------------------------------------------------------------- Tweeming UI ----------------------------------------------------------------------------------------

    void TweenerIn1(GameObject obj_tween, float timeduration)
    {
        obj_tween.SetActive(true);
        obj_tween.GetComponent<CanvasGroup>().alpha = 0;
        (obj_tween.transform as RectTransform).localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //(obj_tween.transform as RectTransform).localPosition = new Vector3(0, -1000, 0);
        (obj_tween.transform as RectTransform).DOScale(new Vector3(1, 1, 1), timeduration).SetEase(Ease.Linear);
        //(obj_tween.transform as RectTransform).DOAnchorPos(new Vector2(0, 0), 0.3f, false).SetEase(Ease.InQuint);
        obj_tween.GetComponent<CanvasGroup>().DOFade(1, timeduration);
    }

    void TweenerOut1(GameObject obj_tween, float timeduration)
    {
        obj_tween.GetComponent<CanvasGroup>().alpha = 1;
        (obj_tween.transform as RectTransform).localScale = new Vector3(1f, 1f, 1f);
        //(obj_tween.transform as RectTransform).localPosition = new Vector3(0, 0, 0);
        (obj_tween.transform as RectTransform).DOScale(new Vector3(0.5f, 0.5f, 0.5f), timeduration);
        //(obj_tween.transform as RectTransform).DOAnchorPos(new Vector2(0, -1000), 0.2f, false).SetEase(Ease.OutFlash);
        obj_tween.GetComponent<CanvasGroup>().DOFade(0, timeduration);
        DOVirtual.DelayedCall(0.2f, () =>
        {
            obj_tween.SetActive(false);
        });
    }

    void AlphaFadeIn(GameObject obj_tween, float timeduration)
    {
        obj_tween.SetActive(true);
        obj_tween.GetComponent<CanvasGroup>().alpha = 0;
        obj_tween.GetComponent<CanvasGroup>().DOFade(1, timeduration);
    }

    void AlphaFadeOut(GameObject obj_tween, float timeduration)
    {
        obj_tween.GetComponent<CanvasGroup>().alpha = 1;
        obj_tween.GetComponent<CanvasGroup>().DOFade(0, timeduration);
        DOVirtual.DelayedCall(timeduration, () =>
        {
            obj_tween.SetActive(false);
        });
    }

    //---------------------------------------------------------------------------- Tweeming GameObjects ----------------------------------------------------------------------------------------
    public void ScaleTween(GameObject obj_tween, Vector3 scale_amt, Vector3 scale_ini, float duration)
    {
        obj_tween.transform.localScale = scale_ini;
        obj_tween.transform.DOScale(scale_amt, duration);
    }


}
