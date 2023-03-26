public class Way
{
    public readonly string BranchName;
    public readonly Station StationFrom;
    public readonly Station StationTo;

    public Way(string branchName, Station stationFrom, Station stationTo)
    {
        BranchName = branchName;
        StationFrom = stationFrom;
        StationTo = stationTo;
    }

    public override string ToString() => $"Way {StationFrom.Name} - {StationTo.Name} ({BranchName})";
}