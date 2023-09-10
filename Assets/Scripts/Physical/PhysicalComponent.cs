using UnityEngine;

namespace Excalibur.Physical
{
    public class PhysicalComponent
    {
        private Transform _transform;
        private IGeometric _shape;

        public string tag = string.Empty;

        public PhysicalComponent(Transform transform, IGeometric shape)
        {
            (_transform, _shape) = (transform, shape);
            _shape.UpdateCenter(_transform.position);
            _shape.UpdateExtents(_transform.localScale);
            _shape.UpdateRotation(Quaternion.identity);
        }

        public Vector3 position
        {
            get => _transform.position;
            set { _transform.position = value; _shape.UpdateCenter(value); }
        }

        public Quaternion rotation
        {
            get => _transform.rotation;
            set { _transform.rotation = value; _shape.UpdateRotation(value); }
        }

        public Vector3 localPosition
        {
            get => _transform.localPosition;
            set { _transform.localPosition = value; _shape.UpdateCenter(value); }
        }

        public bool HitPoint(Vector2 point)
        {
            return _shape.ContainsPoint2D(point);
        }

        public bool HitPoint(Vector3 point)
        {
            return _shape.ContainsPoint3D(point);
        }
    }
}