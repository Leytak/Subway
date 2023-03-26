using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Pathfinder
{
    public static string FindShortestPath(Station stationFrom, Station stationTo)
    {
        var visitedStations = new List<string>();
        bool CanVisit(Station station) => !visitedStations.Contains(station.Name);

        var vertices = new List<GraphVertex> { new (stationFrom) };
        while (vertices.All(vertex => vertex.Station != stationTo))
        {
            var newVertices = new List<GraphVertex>();
            
            foreach (var vertex in vertices)
            {
                visitedStations.Add(vertex.Station.Name);
                foreach (var way in vertex.Station.Ways)
                {
                    if (way.StationFrom != vertex.Station && CanVisit(way.StationFrom))
                        newVertices.Add(new GraphVertex(way.StationFrom, vertex, way.BranchName));
                    if (way.StationTo != vertex.Station && CanVisit(way.StationTo))
                        newVertices.Add(new GraphVertex(way.StationTo, vertex, way.BranchName));
                }
            }

            vertices = newVertices;
        }

        return (from vertex in vertices where vertex.Station == stationTo select vertex.GetPath())
            .OrderBy(path => path.Transfers)
            .First()
            .ToString();
    }
}