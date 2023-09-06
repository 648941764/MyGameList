using UnityEngine;

public struct Circle : IGeometry2D
{
    private Vector3 p;
    private Quaternion q;

    public Vector3 position { get => p; set => p = value; }
    public Quaternion quaternion { get => q; set => q = value; }
}