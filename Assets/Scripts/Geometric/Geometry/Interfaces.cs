using UnityEngine;

namespace Excalibur.Geometric
{
    public interface IGeometric
    {
        void UpdateCenter(Vector3 center);
        void UpdateRotation(Quaternion rotation);
        bool ContainsPoint2D(Vector2 point);
        bool ContainsPoint3D(Vector3 point);
        Vector2 GetVertex2D(int index);
        Vector3 GetVertex3D(int index);
    }
}