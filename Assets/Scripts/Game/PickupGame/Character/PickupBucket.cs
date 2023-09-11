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
        Vector3 scale = _physicalComp.localScale;
        _physicalComp.localScale = new Vector3(scale.x + 0.2f, scale.y, scale.z);

        EnrollEvents(_OnPickupStageChangeHandler);
    }

    public override void OnBegin()
    {
        Vector2 pos;
        (float, float) xRange = GameView.Instance.GetRangeHorizontal();
        pos.x = Random.Range(xRange.Item1, xRange.Item2);
        (float, float) yRange = GameView.Instance.GetRangeVertical();
        pos.y = yRange.Item2 * 0.65f;
        _physicalComp.position = pos;

        _model = ModelManager.Instance.GetModel<PickupModel>();
        _moveEquation.b = pos.x;
        _UpdateSpeed();

        _ScheduleThrow();
    }

    public override void OnEnd()
    {
        GameManager.Instance.Unschedule(_throwAppleTimerIdentifier);
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

    private void _ThrowApple()
    {
        Apple apple = Apple.ApplePool.GetApple();
        // 扔出的角度是相对于Vecter3.up的角度，与Bucket的移动方向相反
        apple.Throw(_throwPoint.position, -Mathf.Sign(_moveEquation.k) * Random.Range(THROW_MIN_ANGLE, THROW_MAX_ANGLE));
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
        _throwAppleTimerIdentifier = GameManager.Instance.Schedule(_model.ThrowInterval, _ThrowApple, default, 0);
    }
}