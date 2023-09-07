using Excalibur.Geometric;
using Excalibur.Physical;

public sealed class PhysicalPlayer : Character
{
    private PhysicalObject _pPlayer;

    protected override void Awake()
    {
        _pPlayer = new PhysicalObject(transform, new Box(transform.position, transform.localScale));
    }

    public override void PlayerUpdate(float dt)
    {
    }
}