using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class RoadsterData
{
    private double epoch;
    private string date;
    private double semimajorAxis;
    private double eccentricity;
    private double inclination;
    private double longitudeOfAscendingNode;
    private double periapsisArgument;
    private double meanAnomaly;
    private double trueAnomaly;

    public double Epoch{
        get => epoch;
        set => epoch = value;
    }

    public string Date{
        get => date;
        set => date = value;
    }

    public double SemimajorAxis{
        get => semimajorAxis;
        set => semimajorAxis = value;
    }

    public double Eccentricity{
        get => eccentricity;
        set => eccentricity = value;
    }

    public double Inclination{
        get => inclination;
        set => inclination = value;
    }

    public double LongitudeOfAscendingNode{
        get => longitudeOfAscendingNode;
        set => longitudeOfAscendingNode = value;
    }

    public double PeriapsisArgument{
        get => periapsisArgument;
        set => periapsisArgument = value;
    }

    public double MeanAnomaly{
        get => meanAnomaly;
        set => meanAnomaly = value;
    }

    public double TrueAnomaly{
        get => trueAnomaly;
        set => trueAnomaly = value;
    }

    public void ParseData(string[] row){
        double.TryParse(row[0], NumberStyles.Float, CultureInfo.InvariantCulture, out epoch);
        date = row[1];
        double.TryParse(row[2], NumberStyles.Float, CultureInfo.InvariantCulture, out semimajorAxis);
        double.TryParse(row[3], NumberStyles.Float, CultureInfo.InvariantCulture, out eccentricity);
        double.TryParse(row[4], NumberStyles.Float, CultureInfo.InvariantCulture, out inclination);
        double.TryParse(row[5], NumberStyles.Float, CultureInfo.InvariantCulture, out longitudeOfAscendingNode);
        double.TryParse(row[6], NumberStyles.Float, CultureInfo.InvariantCulture, out periapsisArgument);
        double.TryParse(row[7], NumberStyles.Float, CultureInfo.InvariantCulture, out meanAnomaly);
        double.TryParse(row[8], NumberStyles.Float, CultureInfo.InvariantCulture, out trueAnomaly);
    }
}
