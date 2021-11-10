using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TFPlanet : MonoBehaviour
{

    public GameObject City;
    public GameObject Tiles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateGrid()
    {
        // destroy childs
        foreach (Transform child in this.Tiles.transform)
        {
            DestroyImmediate(child.gameObject);
        }

        for (int x=0; x<5; x++)
        {
            Ray ray = new Ray(new Vector3(-13f * x, 0, 0), Vector3.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                var newCity = Instantiate(this.City, hit.point, Quaternion.identity, this.Tiles.transform);
                newCity.transform.up = hit.normal;
            }
        }


    }
}
