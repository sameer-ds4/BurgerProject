using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMenuUI : MonoBehaviour
{
    public GameObject logo;
    public LoadingPageUI _loadingPageUI;

    public float loadingSpeedOffSet=1f;

    private void Start()
    {
        Input.multiTouchEnabled = false;
        logo.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        logo.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack).OnComplete(()=> 
        {
            StartCoroutine(LoadScene());
        }); 
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            _loadingPageUI.LoadingProgress(Mathf.Lerp(_loadingPageUI.sliderValue, asyncOperation.progress + 0.11f,Time.deltaTime * loadingSpeedOffSet));
            
            if (_loadingPageUI.sliderValue >= 1f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
