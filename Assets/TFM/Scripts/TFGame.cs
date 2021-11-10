using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class Symbol
{
    public string name { get; set; }
    public bool foreign { get; set; }
    public int amount { get; set; }
}

public class Content
{
    public string name { get; set; }
    public List<string> classes { get; set; }
    public string text { get; set; }
    public List<Content> content { get; set; }
}

public class CardData
{
    public string title { get; set; }
    public bool active { get; set; }
    public List<string> tags { get; set; }
    public int costs { get; set; }
    public string requirements { get; set; }
    public string requirement_code { get; set; }
    public int number { get; set; }
    public object victory_points { get; set; }
    public string description { get; set; }
    public bool corporate { get; set; }
    public List<string> income_code { get; set; }
    public List<string> balance_code { get; set; }
    public List<Symbol> income_symbols { get; set; }
    public List<Symbol> balance_symbols { get; set; }
    public Content content { get; set; }
}

public class Root
{
    public List<CardData> cards { get; set; }
}


public class TFGame : MonoBehaviour
{
    //public Player CurrentPlayer;

    //Script script = new Script();


    public TFPlayer player;
    public TFCard card;
    public TFMars Mars;

    private int currentCardNumber = 1;
    private Root root;



    void Start()
    {
        using (StreamReader r = new StreamReader(@"Assets/cards.json"))
        {
            string json = r.ReadToEnd();
            this.root = JsonConvert.DeserializeObject<Root>(json);
            this.card.CardData = root.cards[currentCardNumber];
        }

        //this.player.PlayCard(this.card);
        //UserData.RegisterAssembly();
    }

    public void NextCard()
    {
        currentCardNumber += 1;
        this.card.CardData = root.cards[currentCardNumber];
        this.card.UpdateCardUI();
    }

    public void PrevCard()
    {
        currentCardNumber -= 1;
        this.card.CardData = root.cards[currentCardNumber];
        this.card.UpdateCardUI();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
