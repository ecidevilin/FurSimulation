using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Vertex
{
    public Vertex(Vector3 pos, float m)
    {
        Position = pos;
        Mass = m;
        OldPosition = Vector3.zero;
        Correction = Vector3.zero;
        Force = Vector3.zero;
        Velocity = Vector3.zero;
    }

    public float Mass;
    public Vector3 Position;
    public Vector3 OldPosition;
    public Vector3 Correction;
    public Vector3 Force;
    public Vector3 Velocity;

    public static float L0;
}
