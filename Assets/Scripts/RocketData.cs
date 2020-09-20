using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketData
{
    private string id;
    private string name;
    private string nationality;

    public string Id{
        get => id;
        set => id = value;
    }

    public string Name{
        get => name;
        set => name = value;
    }

    public string Nationality{
        get => nationality;
        set => nationality = value;
    }
}
