using UnityEngine;
using Excalibur.Physical;

public class PickupBucket : Character, IPhysicalObject
{


    private PhysicalComponent _physicalComp;
    public PhysicalComponent PhysicalComponent => _physicalComp;

    protected override void Awake()
    {
        base.Awake();
        _physicalComp = new PhysicalComponent(transform, new Box());
    }

    public override void GameUpdate(float dt)
    {
    }

    public void OnCollisionWith(PhysicalComponent other)
    {
        throw new System.NotImplementedException();
    }
}