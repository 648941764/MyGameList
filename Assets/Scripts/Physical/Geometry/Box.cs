using System;
using UnityEngine;
using UnityEngine.UI;

namespace Excalibur.Physical
{
    using BoxShape = Geometry.Box;

    public class Box : IGeometric
    {
        public const int VERTEX_COUNT = 8;

        public BoxShape _box;

        public Vector3 center => _box.center;
        public Vector3 extents => _box.extents;
        public Quaternion rotation => _box.rotation;

        public void UpdateCenter(Vector3 center)
        {
            _box.center = center;
        }

        public void UpdateExtents(Vector3 extents)
        {
            _box.extents = extents;
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