using System.Collections.Generic;

namespace Excalibur.Physical
{
    public interface IPhysicalObject
    {
        string Tag { get; }
        HashSet<string> CollisionTags { get; }
        PhysicalComponent PhysicalComponent { get; }
        void OnCollisionWith(IPhysicalObject other);
    }
}