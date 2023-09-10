using Excalibur.Physical;
using Excalibur.Geometric;
using UnityEngine;

public class Apple : Character, IPhysicalObject
{
    private PhysicalComponent physicalComp;

    public PhysicalComponent PhysicalComponent => physicalComp;

    protected override void Awake()
    {
        physicalComp = new PhysicalComponent(transform, new Box(transform.position, transform.localScale));
    }

    public override void GameUpdate(float dt)
    {

    }

    public void OnCollisionWith(PhysicalComponent other)
    {
    }
}