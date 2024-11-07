using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerMixer : MonoBehaviour
{
    public Image customerImage;
    public Sprite[] sprites;

    private void Start() 
    {
        SetImage();
    }

    private void SetImage()
    {
        int rand = Random.Range(0, sprites.Length);
        customerImage.sprite = sprites[rand];
    }
}
