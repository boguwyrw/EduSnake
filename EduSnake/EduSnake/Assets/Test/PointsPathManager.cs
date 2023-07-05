using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsPathManager : MonoBehaviour
{
    public List<PointsMaker> pointsMakers = new List<PointsMaker>();

    private void FixedUpdate()
    {
        UpdatePointsList();
    }

    private void AddPointMarker()
    {
        Vector3 pointPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f);
        pointsMakers.Add(new PointsMaker(pointPos, transform.rotation));
    }

    public void UpdatePointsList()
    {
        AddPointMarker();
    }

    public void ClearPointsList()
    {
        pointsMakers.Clear();
        AddPointMarker();
    }
}
