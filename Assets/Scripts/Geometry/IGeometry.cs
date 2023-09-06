using UnityEngine;

public interface IGeometry
{
    Vector3 position { get; set; }
    Quaternion quaternion { get; set; }
}

public interface IGeometry2D : IGeometry
{

}

public interface IGeometry3D : IGeometry
{

}