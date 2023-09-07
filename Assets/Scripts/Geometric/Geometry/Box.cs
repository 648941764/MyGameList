using System;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Excalibur.Geometric
{
    /*
     * 2D的Box数值放在x、y位置，
     * 
     */

    using BoxShape = Structs.Box;

    public class Box : IGeometric
    {
        public const int VERTEX_COUNT = 8;

        public BoxShape _box;

        public Box(Vector3 center, Vector3 extents)
        {
            if (extents.x <= 0f || extents.y <= 0f || extents.z < 0f)
            {
                throw new System.InvalidOperationException("Box constructor, extents can not be negative");
            }

            _box.center = center;
            _box.extents = extents;
            _box.rotation = Quaternion.identity;
        }

        public void UpdateCenter(Vector3 center)
        {
            _box.center = center;
        }

        public void UpdateRotation(Quaternion rotation)
        {
            _box.rotation = rotation;
        }

        public bool ContainsPoint2D(Vector2 point)
        {
            return Mathf.Abs(point.x) <= _box.extents.x && Mathf.Abs(point.y) <= _box.extents.y;
        }

        public bool ContainsPoint3D(Vector3 point)
        {
            return Mathf.Abs(point.x) <= _box.extents.x && Mathf.Abs(point.y) <= _box.extents.y;
        }

        public Vector2 GetVertex2D(int index)
        {
            switch (index)
            {
                case 0: return new Vector2(_box.center.x - _box.extents.x, _box.center.y - _box.extents.y);
                case 1: return new Vector2(_box.center.x - _box.extents.x, _box.center.y + _box.extents.y);
                case 2: return new Vector2(_box.center.x + _box.extents.x, _box.center.y - _box.extents.y);
                case 3: return new Vector2(_box.center.x + _box.extents.x, _box.center.y + _box.extents.y);
                default: throw new ArgumentOutOfRangeException(nameof(index), $"Invalid index: {index}. Valid vertex indices range from 0 to {VERTEX_COUNT / 2- 1}");
            }
        }

        public Vector3 GetVertex3D(int index)
        {
            switch (index)
            {
                case 0: return new Vector3(_box.center.x - _box.extents.x, _box.center.y - _box.extents.y, _box.center.z - _box.extents.z);
                case 1: return new Vector3(_box.center.x - _box.extents.x, _box.center.y - _box.extents.y, _box.center.z + _box.extents.z);
                case 2: return new Vector3(_box.center.x - _box.extents.x, _box.center.y + _box.extents.y, _box.center.z - _box.extents.z);
                case 3: return new Vector3(_box.center.x - _box.extents.x, _box.center.y + _box.extents.y, _box.center.z + _box.extents.z);
                case 4: return new Vector3(_box.center.x + _box.extents.x, _box.center.y - _box.extents.y, _box.center.z - _box.extents.z);
                case 5: return new Vector3(_box.center.x + _box.extents.x, _box.center.y - _box.extents.y, _box.center.z + _box.extents.z);
                case 6: return new Vector3(_box.center.x + _box.extents.x, _box.center.y + _box.extents.y, _box.center.z - _box.extents.z);
                case 7: return new Vector3(_box.center.x + _box.extents.x, _box.center.y + _box.extents.y, _box.center.z + _box.extents.z);
                default: throw new ArgumentOutOfRangeException(nameof(index), $"Invalid index: {index}. Valid vertex indices range from 0 to {VERTEX_COUNT - 1}");
            }
        }
    }
}