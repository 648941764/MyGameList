using UnityEngine;

namespace Excalibur.Physical.Geometry
{
    public struct Box
    {
        /// <summary> 中心点 </summary>
        public Vector3 center;
        /// <summary> 一半边长 </summary>
        public Vector3 extents;
        /// <summary> 旋转 </summary>
        public Quaternion rotation;
    }
}