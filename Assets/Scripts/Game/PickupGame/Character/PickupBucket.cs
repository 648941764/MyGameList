using UnityEngine;
using Excalibur.Physical;

public class PickupBucket : Character, IPhysicalObject
{
    private PhysicalComponent _physicalComp;
    private LinearEquation _moveEquation = new LinearEquation();
    private PickupModel _model;
    private int _throwAppleTimerIdentifier;

    public PhysicalComponent PhysicalComponent => _physicalComp;

    protected override void Awake()
    {
        base.Awake();
        _physicalComp = new PhysicalComponent(transform, new Box());
        Vector3 scale = _physicalComp.localScale;
        _physicalComp.localScale = new Vector3(scale.x + 0.2f, scale.y, scale.z);

        EnrollEvents(_OnPickupStageChangeHandler);
    }

    public override void OnGameStart()
    {
        Vector2 pos;
        (float, float) xRange = GameView.Instance.GetRangeHorizontal();
        pos.x = Random.Range(xRange.Item1, xRange.Item2);
        (float, float) yRange = GameView.Instance.GetRangeVertical();
        pos.y = yRange.Item2 * 0.35f;
        _physicalComp.position = pos;

        _model = ModelManager.Instance.GetModel<PickupModel>();
        _moveEquation.b = pos.x;
        _UpdateSpeed();

        _throwAppleTimerIdentifier = GameManager.Instance.Schedule(_model.ThrowInterval, () => _ThrowApple());
    }

    public override void GameUpdate(float dt)
    {
        Vector2 currentPos = _physicalComp.position;
        currentPos.x = _moveEquation.Map(dt);
        if (!GameView.Instance.IsInSight(_physicalComp.GetVertex2D(2)))
        {
            _ReverseDir();
        }
        _moveEquation.b = currentPos.x;
        _physicalComp.position = currentPos;
    }

    public void OnCollisionWith(PhysicalComponent other)
    {

    }

    private void _ThrowApple()
    {

    }

    private void _UpdateSpeed()
    {
        _moveEquation.k = Mathf.Sign(_moveEquation.k) * _model.BucketSpeed;
    }

    private void _ReverseDir()
    {
        _moveEquation.k = -_moveEquation.k;
    }

    private void _OnPickupStageChangeHandler(EventParam eventParam)
    {
        switch (eventParam.eventName)
        {
            case EventName.PickupGameStageChange:
                {
                    GameManager.Instance.Unschedule(_throwAppleTimerIdentifier);
                    _throwAppleTimerIdentifier = GameManager.Instance.Schedule(_model.ThrowInterval, _ThrowApple);
                }
                break;
        }
    }
}