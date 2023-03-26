public class GraphVertex
{
    public readonly Station Station;
    public GraphVertex PreviousVertex;
    private string BranchName;

    public GraphVertex(Station station, GraphVertex previousVertex = null, string branchName = null)
    {
        Station = station;
        PreviousVertex = previousVertex;
        BranchName = branchName;
    }

    public Path GetPath()
    {
        var path = new Path();
        path.Stations.Add(Station.Name);
        var currentVertex = this;
        var branchName = BranchName;
        while (currentVertex.PreviousVertex is not null)
        {
            currentVertex = currentVertex.PreviousVertex;
            path.Stations.Add(currentVertex.Station.Name);
            if (!string.IsNullOrEmpty(currentVertex.BranchName) && branchName != currentVertex.BranchName) 
                path.Transfers++;
            branchName = currentVertex.BranchName;
        }

        path.Stations.Reverse();
        return path;
    }
}