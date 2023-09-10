using Excalibur.Geometric;
using Excalibur.Physical;
using UnityEngine;

public sealed class PickupPlayer : Character, IPhysicalObject
{
    private PhysicalComponent physicalComp;
    public PhysicalComponent PhysicalComponent => physicalComp;

    protected override void Awake()
    {
        physicalComp = new PhysicalComponent(transform, new Box(transform.position, transform.localScale));

        EnrollEvents(_UpdateMove);
    }

    public override void GameUpdate(float dt)
    {
    }

    public void OnCollisionWith(PhysicalComponent other)
    {

    }

    private void _UpdateMove(EventParam eventParam)
    {
        switch (eventParam.eventName)
        {
            case EventName.KeyInput:
                switch (eventParam.Get<KeyCode>())
                {
                    case KeyCode.A:
                    case KeyCode.D:
                        {
                            _UpdateMove();
                            break;
                        }
                }
                break;
        }
    }

    private void _UpdateMove()
    {
        float input = Input.GetAxisRaw("Horizontal");
        Vector2 pos = physicalComp.position;
        pos.x += input;
        physicalComp.position = pos;
    }
}