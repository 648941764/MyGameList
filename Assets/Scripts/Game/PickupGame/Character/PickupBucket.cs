using UnityEngine;
using Excalibur.Physical;

public class PickupBucket : Character, IPhysicalObject
{
    private PhysicalComponent _physicalComp;
    public PhysicalComponent PhysicalComponent => _physicalComp;

    LinearEquation moveEquation = new LinearEquation();

    PickupModel model;

    protected override void Awake()
    {
        base.Awake();
        _physicalComp = new PhysicalComponent(transform, new Box());
        Vector3 scale = _physicalComp.localScale;
        _physicalComp.localScale = new Vector3(scale.x + 0.2f, scale.y, scale.z);
    }

    public override void OnGameStart()
    {
        Vector2 pos;
        (float, float) xRange = GameView.Instance.GetRangeHorizontal();
        pos.x = Random.Range(xRange.Item1, xRange.Item2);
        (float, float) yRange = GameView.Instance.GetRangeVertical();
        pos.y = yRange.Item2 * 0.35f;
        _physicalComp.position = pos;

        model = ModelManager.Instance.GetModel<PickupModel>();
        moveEquation.b = pos.x;
        _UpdateSpeed();
    }

    public override void GameUpdate(float dt)
    {
        Vector2 currentPos = _physicalComp.position;
        currentPos.x = moveEquation.Map(dt);
        if (!GameView.Instance.IsInSight(_physicalComp.GetVertex2D(2)))
        {
            _ReverseDir();
        }
        moveEquation.b = currentPos.x;
        _physicalComp.position = currentPos;
    }

    public void OnCollisionWith(PhysicalComponent other)
    {

    }

    private void _UpdateSpeed()
    {
        moveEquation.k = Mathf.Sign(moveEquation.k) * model.BucketSpeed;
    }

    private void _ReverseDir()
    {
        moveEquation.k = -moveEquation.k;
    }
}