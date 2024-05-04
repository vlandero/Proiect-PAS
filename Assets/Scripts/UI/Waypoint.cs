using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

[Serializable]
public struct WaypointData
{
    public Transform target;
    public Image waypoint;
}
public class Waypoint : MonoBehaviour
{
    public List<WaypointData> waypoints;

    private void Update()
    {
        foreach (var waypointData in waypoints)
        {
            SetWaypoint(waypointData.waypoint, waypointData.target);
        }
    }

    private void SetWaypoint(Image waypoint, Transform target)
    {
        float minX = waypoint.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = waypoint.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position);

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        waypoint.transform.position = pos;
    }

    public void AddWaypoint(WaypointData w)
    {
        waypoints.Add(w);
    }

    public void RemoveWaypoint(WaypointData w)
    {
        w.waypoint.gameObject.SetActive(false);
        waypoints.Remove(w);
    }
}
