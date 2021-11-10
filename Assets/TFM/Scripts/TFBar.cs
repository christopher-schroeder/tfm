using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TFBar : MonoBehaviour
{

    public RectTransform Front;
    public int Value=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        this.Front.sizeDelta = new Vector3(30, 15 + 26 * this.Value);
    }
}
