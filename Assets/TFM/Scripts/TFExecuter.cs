using MoonSharp.Interpreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TFExecuter
{
    private List<string> commands = null;
    private TFPlayer player;
    private TFGame game;
    private TFCard card;

    public Script script;

    private int command_index = 0;

    public TFExecuter(TFPlayer player, TFGame game)
    {
        this.player = player;
        this.game = game;

        UserData.RegisterType<TFPlayer>();
        UserData.RegisterType<TFResources>();
        UserData.RegisterType<TFResource>();
        UserData.RegisterType<TFBalance>();
        UserData.RegisterType<TFIncome>();
        UserData.RegisterType<TFCard>();
        UserData.RegisterType<TFMars>();
        this.script = new Script();
        this.script.Globals["Player"] = UserData.Create(this.player);
        this.script.Globals["Mars"] = UserData.Create(this.game.Mars);
    }

    public void Execute(List<string> commands)
    {
        this.commands = commands;
        string current_command = this.commands[command_index];
        script.DoString(current_command);
    }

    public bool ExecuteBool(string command)
    {
        return script.DoString("return " + command).Boolean;
    }

    public void SetCard(TFCard card)
    {
        this.card = card;
        this.script.Globals["Card"] = UserData.Create(card);
    }
}
