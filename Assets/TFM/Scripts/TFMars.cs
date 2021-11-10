using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TFMars : MonoBehaviour
{
    private int o2 = 0;
    private int temperatur = 0;
    private int oceans = 0;

    public int O2
    {
        get { return this.o2; }
        set { this.o2 = value; }
    }

    public int Temperatur
    {
        get { return this.temperatur; }
        set { this.Temperatur = value; }
    }

    public int Oceans
    {
        get { return this.oceans; }
        set { this.oceans = value; }
    }

    public void Place(TFPlayer player, TFTile tile, GameObject go)
    {
        Debug.Log("place");
    } 
}
