using UnityEngine;
using Excalibur.Physical;

public class PickupBucket : Character
{
    public const float THROW_MIN_ANGLE = 30f;
    public const float THROW_MAX_ANGLE = 60f;

    [SerializeField] private Transform _throwPoint;

    private PhysicalComponent _physicalComp;
    private LinearEquation _moveEquation = new LinearEquation();
    private PickupModel _model;
    private int _throwAppleTimerIdentifier;

    public PhysicalComponent PhysicalComponent => _physicalComp;

    protected override void Awake()
    {
        base.Awake();
        _physicalComp = new PhysicalComponent(transform, new Box());
        EnrollEvents(_OnPickupStageChangeHandler);
    }

    public override void Begin()
    {
        base.Begin();
        Vector2 pos = GameView.Instance.GetViewCenter();
        (float, float) xRange = GameView.Instance.GetRangeHorizontal();
        pos.x += Random.Range(xRange.Item1, xRange.Item2);
        (float, float) yRange = GameView.Instance.GetRangeVertical();
        pos.y += yRange.Item2 * 0.55f;
        _physicalComp.position = pos;

        _model = ModelManager.Instance.GetModel<PickupModel>();
        _moveEquation.b = pos.x;
        _UpdateSpeed();
        _ThrowApple();
    }

    public override void Over()
    {
        base.Over();
        GameManager.Instance.Unschedule(_throwAppleTimerIdentifier);
    }

    public override void GameUpdate(float dt)
    {
        Vector2 currentPos = _physicalComp.position;
        currentPos.x = _moveEquation.Map(dt);
        if (_moveEquation.k > 0)
        {
            if (!GameView.Instance.IsInSight(_physicalComp.GetVertex2D(2)))
            {
                _ReverseDir();
            }
        }
        else if (_moveEquation.k < 0)
        {
            if (!GameView.Instance.IsInSight(_physicalComp.GetVertex2D(0)))
            {
                _ReverseDir();
            }
        }
        _moveEquation.b = currentPos.x;
        _physicalComp.position = currentPos;
    }

    private void _ThrowApple()
    {
        Apple apple = Apple.ApplePool.GetApple();
        apple.Throw(_throwPoint.position);
        _ScheduleThrow();
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
                    _ThrowApple();
                    _ScheduleThrow();
                }
                break;
        }
    }

    private void _ScheduleThrow()
    {
        GameManager.Instance.Unschedule(_throwAppleTimerIdentifier);
        int duration = Random.Range(_model.ThrowInterval / 2, _model.ThrowInterval);
        _throwAppleTimerIdentifier = GameManager.Instance.Schedule(duration, _ThrowApple, default, -1);
    }
}