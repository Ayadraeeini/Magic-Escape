//Eyad Al Raeeini - 02/17/2026
//swipe trail drawer
using System.Collections.Generic;
using UnityEngine;
public class SwipeTrailDrawer : MonoBehaviour
{
    public Camera cam;
    public LineRenderer lr;
    public float minPointDistance = 10f;

    private List<Vector3> points = new List<Vector3>();
    private bool drawing;

    void Awake()
    {
        if (lr == null)
            lr = GetComponent<LineRenderer>();
        lr.positionCount = 0;
    }

    public void Begin()
    {
        drawing = true;
        points.Clear();
        lr.positionCount = 0;
    }

    public void End()
    {
        drawing = false;
    }

    public void Clear()
    {
        points.Clear();
        lr.positionCount = 0;
    }

    public void AddScreenPoint(Vector2 screenPos)
    {
        if (!drawing) return;

        if (points.Count > 0)
        {
            Vector2 lastScreen = cam.WorldToScreenPoint(points[points.Count - 1]);
            if (Vector2.Distance(lastScreen, screenPos) < minPointDistance)
                return;
        }

        Vector3 world = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 5f));
        points.Add(world);
        lr.positionCount = points.Count;
        lr.SetPositions(points.ToArray());
    }
}