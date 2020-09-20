using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchData
{
    private string missionName;
    private int numberOfPayloads;
    private RocketData rocketData;
    private List<ShipData> shipData;
    private bool isFutureLaunch;

    public string MissionName{
        get => missionName;
        set => missionName = value;
    }

    public int NumberOfPayloads{
        get => numberOfPayloads;
        set => numberOfPayloads = value;
    }

    public RocketData RocketData{
        get => rocketData;
        set => rocketData = value;
    }

    public List<ShipData> ShipData{
        get => shipData;
        set => shipData = value;
    }

    public bool IsFutureLaunch{
        get => isFutureLaunch;
        set => isFutureLaunch = value;
    }
}
