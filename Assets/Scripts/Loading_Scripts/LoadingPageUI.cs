using UnityEngine.UI;
using UnityEngine;

public class LoadingPageUI : MonoBehaviour
{
    [SerializeField]
    private Slider loadingBar;
    public float sliderValue => loadingBar.value;
    public void LoadingProgress(float percentage)
    {
        loadingBar.value = percentage;
    }
}
