using UnityEngine;

namespace Excalibur.Physical
{
    public class PhysicalComponent
    {
        private Transform _transform;
        private IGeometric _shape;

        public PhysicalComponent(Transform transform, IGeometric shape)
        {
            (_transform, _shape) = (transform, shape);
            _shape.UpdateCenter(_transform.position);
            _shape.UpdateExtents(_transform.localScale / 2f);
            _shape.UpdateRotation(Quaternion.identity);
        }

        public Transform transform => _transform;
        public GameObject gameObject => transform.gameObject;

        public Vector3 position
        {
            get => _transform.position;
            set { _transform.position = value; _shape.UpdateCenter(value); }
        }

        public Vector3 localScale
        {
            get => _transform.localScale;
            set { _transform.localScale = value; _shape.UpdateExtents(value / 2f); }
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

        public Vector2 GetVertex2D(int index) => _shape.GetVertex2D(index);
        public Vector2 GetVertex3D(int index) => _shape.GetVertex3D(index);

        public bool HitPoint2D(Vector2 point)
        {
            return _shape.ContainsPoint2D(point);
        }

        public bool HitPoint3D(Vector3 point)
        {
            return _shape.ContainsPoint3D(point);
        }
    }
}