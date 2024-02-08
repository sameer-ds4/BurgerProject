using UnityEngine;
using DG.Tweening;

public class ItemTween : MonoBehaviour
{
    public TweenType tweenType;

    public MoveDirection direction;
    public Scale scale;
    public Fade fade;
    public Ease ease;
    public float tweenTime;
    public bool delay;
    public float delayTime;

    private Vector3 actualPosition;
    private Vector3 actualScale;

    private Vector3 tweenPosition;


    //Get Actual Rectpositions of the element in the Canvas
    private void OnEnable()
    {
        actualPosition = (transform as RectTransform).anchoredPosition;
        actualScale = (transform as RectTransform).localScale;
        PreAnimate();

        if(delay)
            DOVirtual.DelayedCall(delayTime, () => { Animate(); });
        else
            Animate();
    }

    private void OnDisable()
    {
        (transform as RectTransform).anchoredPosition = actualPosition;
        (transform as RectTransform).localScale = actualScale;
    }

    private void OnDestroy() 
    {
        transform.DOKill(false);
    }
    
    private void PreAnimate()
    {
        switch (tweenType)
        {
            case TweenType.Move:
                MoveUIitem();
                break;

            case TweenType.Scale:
                ScaleUIitem();
                break;

            case TweenType.Fade:
                FadeUIitem();
                break;
        }
    }

    private void Animate()
    {
        switch(tweenType)
        {
            case TweenType.Move:
                (transform as RectTransform).DOAnchorPos(actualPosition, tweenTime).SetEase(ease);
                break;

            case TweenType.Scale:
                (transform as RectTransform).DOScale(actualScale, tweenTime).SetEase(ease);
                break;

            case TweenType.Fade:
                gameObject.GetComponent<CanvasGroup>().DOFade(fadeTo, tweenTime).SetEase(ease);
                break;
        }
    }

    //UI item direction - specified in the inspector
    private void MoveUIitem()
    {
        switch(direction)
        {
            case MoveDirection.Up:
                tweenPosition = new Vector3(0, (transform as RectTransform).localPosition.y + Screen.height, 0);
                break;

            case MoveDirection.Down:
                tweenPosition = new Vector3(0, (transform as RectTransform).localPosition.y - Screen.height, 0);
                break;

            case MoveDirection.Left:
                tweenPosition = new Vector3((transform as RectTransform).localPosition.x - Screen.width, 0, 0);
                break;

            case MoveDirection.Right:
                tweenPosition = new Vector3((transform as RectTransform).localPosition.x + Screen.width, 0, 0);
                break;
        }

        (transform as RectTransform).localPosition = (transform as RectTransform).localPosition + tweenPosition;
    }


    float from;
    //UI item scale - in and out scaling
    private void ScaleUIitem()
    {
        switch(scale)
        {
            case Scale.ScaleUp:
                from = 0.1f;
                break;

            case Scale.ScaleDown:
                from = 1.8f;
                break;
        }

        transform.localScale = Vector3.one * from;
    }

    int fadeFrom, fadeTo;
    //UI item fade - fade in and out transitions
    private void FadeUIitem()
    {

        switch(fade)
        {
            case Fade.FadeIn:
                fadeFrom = 0;
                fadeTo = 1;
                break;

            case Fade.FadeOut:
                fadeFrom = 1;
                fadeTo = 0;
                break;
        }

        if(!gameObject.GetComponent<CanvasGroup>())
            gameObject.AddComponent<CanvasGroup>();

        gameObject.GetComponent<CanvasGroup>().alpha = fadeFrom;
    }
}



public enum TweenType
{
    Move,
    Scale,
    Fade
}

public enum MoveDirection
{
    Up,
    Down,
    Left,
    Right
}

public enum Scale
{
    ScaleUp,
    ScaleDown
}

public enum Fade
{
    FadeIn,
    FadeOut
}
