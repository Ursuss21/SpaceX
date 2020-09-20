using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData
{
    private string id;
    private string name;
    private int numberOfMissions;
    private string shipType;
    private string homePort;
    private string imageURL;

    public string Id{
        get => id;
        set => id = value;
    }

    public string Name{
        get => name;
        set => name = value;
    }

    public int NumberOfMissions{
        get => numberOfMissions;
        set => numberOfMissions = value;
    }

    public string ShipType{
        get => shipType;
        set => shipType = value;
    }

    public string HomePort{
        get => homePort;
        set => homePort = value;
    }
    
    public string ImageURL{
        get => imageURL;
        set => imageURL = value;
    }
}
