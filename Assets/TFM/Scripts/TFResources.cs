using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEngine;

public class TFResources : MonoBehaviour
{
    [HideInInspector] public TFResource Credits;
    [HideInInspector] public TFResource Steel;
    [HideInInspector] public TFResource Titanium;
    [HideInInspector] public TFResource Plant;
    [HideInInspector] public TFResource Power;
    [HideInInspector] public TFResource Heat;

    void Start()
    {
        this.Credits = this.transform.Find("Credits").GetComponent<TFResource>();
        this.Steel = this.transform.Find("Steel").GetComponent<TFResource>();
        this.Titanium = this.transform.Find("Titanium").GetComponent<TFResource>();
        this.Plant = this.transform.Find("Plant").GetComponent<TFResource>();
        this.Power = this.transform.Find("Power").GetComponent<TFResource>();
        this.Heat = this.transform.Find("Heat").GetComponent<TFResource>();
    }

    public void Turn(int tf)
    {
        this.Credits.Turn(tf);
        this.Steel.Turn();
        this.Titanium.Turn();
        this.Plant.Turn();
        this.Power.Turn();
        this.Heat.Turn();
    }
}
