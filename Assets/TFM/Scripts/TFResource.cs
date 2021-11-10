using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TFResource : MonoBehaviour
{
    [HideInInspector]
    public TFBalance Balance;
    [HideInInspector]
    public TFIncome Income;

    public void Turn(int additional=0)
    {
        this.Balance.Add(this.Income.Value + additional);
    }

    public void Start()
    {
        this.Balance = this.transform.Find("Balance").GetComponent<TFBalance>();
        this.Income = this.transform.Find("Income").GetComponent<TFIncome>();
    }
}
