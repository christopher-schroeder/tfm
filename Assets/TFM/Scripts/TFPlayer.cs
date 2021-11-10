using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TFPlayer : MonoBehaviour
{


    [HideInInspector] public Symbols Symbols = new Symbols();
    public TFResources Resources;
    public TFGame Game;
    [HideInInspector] public TFExecuter Executer;

    void Start()
    {
        this.Executer = new TFExecuter(this, this.Game);
    }

    // invoked by LUA
    public void SelectTile(Action<string> callback=null)
    {
        if (callback != null)
            callback.Invoke("");
    }

    public void AddSteel(int n)
    {
        Debug.Log("AddSteel " + n);
    }

    public void PlayCard(TFCard card)
    {
        //card.UpdateCardUI();
        //script.Globals["card"] = UserData.Create(card);
        this.Executer.Execute(card.GetIncomeCode());
    }


    public string GetName()
    {
        return "Christo";
    }
}