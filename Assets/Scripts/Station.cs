using System.Collections.Generic;

public class Station
{
    public readonly string Name;
    public readonly List<Way> Ways;

    public Station(string name)
    {
        Name = name;
        Ways = new List<Way>();
    }

    public void AddWay(Way way) => Ways.Add(way);
}