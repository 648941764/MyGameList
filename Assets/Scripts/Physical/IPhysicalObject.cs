namespace Excalibur.Physical
{
    public interface IPhysicalObject
    {
        PhysicalComponent PhysicalComponent { get; }
        void OnCollisionWith(PhysicalComponent other);
    }
}