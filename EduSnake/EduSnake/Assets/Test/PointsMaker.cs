using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsMaker
{
    public Vector3 pointPosition;
    public Quaternion pointRotation;

    public PointsMaker(Vector3 pointPos, Quaternion pointRot)
    {
        pointPosition = pointPos;
        pointRotation = pointRot;
    }
}
