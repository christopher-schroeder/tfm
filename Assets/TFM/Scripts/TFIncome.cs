using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TFIncome : MonoBehaviour
{
    private TextMeshProUGUI ui;
    private int _value = 0;

    public int Value
    {
        get { return this._value; }
        set
        {
            this._value = value;
            this.ui.text = value.ToString("+0;-#");
        }
    }

    public void Add(int amount, Action<String> callback=null)
    {
        this.Value += amount;
        if (callback != null)
            callback.Invoke("");
    }

    void Start()
    {
        this.ui = this.GetComponent<TextMeshProUGUI>();
    }
}
