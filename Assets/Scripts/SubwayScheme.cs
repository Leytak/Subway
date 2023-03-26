using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubwayScheme : MonoBehaviour
{
    [Serializable]
    private class Branch
    {
        public string Name;
        public Color Color;
        public List<GameObject> Stations;
    }

    [SerializeField]
    private List<Branch> branches = new ();

    [Header("UI Elements")]
    [SerializeField]
    private TMP_Dropdown dropdownFrom;
    [SerializeField]
    private TMP_Dropdown dropdownTo;
    [SerializeField]
    private Button findButton;
    [SerializeField]
    private TMP_Text resultLabel;

    private Dictionary<string, Station> stations = new ();

    private void Awake()
    {
        FindStations();
        FindWays();
        DrawScheme();

        var options = new List<TMP_Dropdown.OptionData>();
        foreach (var station in stations.Keys)
            options.Add(new TMP_Dropdown.OptionData(station));
        dropdownFrom.AddOptions(options);
        dropdownTo.AddOptions(options);
        findButton.onClick.AddListener(OnFindClicked);
    }

    private void FindStations()
    {
        stations.Clear();
        foreach (var station in branches.SelectMany(branch => branch.Stations))
            stations.TryAdd(station.name, new Station(station.name));
    }

    private void FindWays()
    {
        foreach (var branch in branches)
        {
            for (int i = 1; i < branch.Stations.Count; i++)
            {
                var stationFrom = stations[branch.Stations[i - 1].name];
                var stationTo = stations[branch.Stations[i].name];
                var way = new Way(branch.Name, stationFrom, stationTo);
                stationFrom.AddWay(way);
                stationTo.AddWay(way);
            }
        }
    }

    private void DrawScheme()
    {
        var lineMaterial = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        foreach (var branch in branches)
        {
            var go = new GameObject();
            go.transform.parent = transform;
            go.name = branch.Name;
            var line = go.AddComponent<LineRenderer>();
            line.material = lineMaterial;
            line.widthMultiplier = 0.25f;
            line.startColor = branch.Color;
            line.endColor = branch.Color;
            var positions = branch.Stations.ConvertAll(station => station.transform.position);
            line.positionCount = positions.Count;
            line.SetPositions(positions.ToArray());
        }
    }

    private void OnFindClicked()
    {
        var stationFrom = dropdownFrom.options[dropdownFrom.value].text;
        var stationTo = dropdownTo.options[dropdownTo.value].text;
        resultLabel.text = Pathfinder.FindShortestPath(stations[stationFrom], stations[stationTo]);
    }

    private void OnDrawGizmos()
    {
        foreach (var branch in branches)
        {
            Gizmos.color = branch.Color;
            for (int i = 1; i < branch.Stations.Count; i++)
            {
                Gizmos.DrawLine(
                    branch.Stations[i - 1].transform.position,
                    branch.Stations[i].transform.position);
            }
        }
    }
}
