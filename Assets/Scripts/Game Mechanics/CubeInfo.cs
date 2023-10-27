using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInfo : MonoBehaviour
{
    public Cube cube;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // private void OnMouseDown()
    // {
    //     if(!this.cube.status)
    //     {
    //         BurgerObject currentburger = PlayerController.Instance.PlaceBurgerComponent(transform);
    //         cube.burgerpart = currentburger;
    //         cube.status = true;
    //     }
    //     else
    //     {
    //         Debug.LogError("full");        
    //     }
    // }

}

[System.Serializable]
public class Cube
{
    public bool status;
    public BurgerObject burgerpart;
}
