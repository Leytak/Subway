using System.Collections.Generic;

public class Path
{
    public List<string> Stations;
    public int Transfers;

    public Path()
    {
        Stations = new List<string>();
        Transfers = 0;
    }

    public override string ToString() => $"{string.Join(string.Empty, Stations)} (transfers: {Transfers})";
}