using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorchange : MonoBehaviour
{
    Color[] givencol;

    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        givencol = new Color[] { Color.red, Color.gray, Color.green };
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit detect;


            if(Physics.Raycast(ray, out detect))
            {
                mat.color = givencol[ Random.Range(0, givencol.Length)];
            }    
        }
    }
}
