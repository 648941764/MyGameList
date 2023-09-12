using Excalibur.Physical;
using System.Collections.Generic;
using UnityEngine;

public sealed class PickupPlayer : Character, IPhysicalObject
{
    [SerializeField] private SpriteRenderer[] _lifes;

    private PhysicalComponent _physicalComp;
    public PhysicalComponent PhysicalComponent => _physicalComp;

    public string Tag => "PickupPlayer";
    private readonly HashSet<string> _collisionTags = new HashSet<string>()
    {
        "Apple"
    };
    public HashSet<string> CollisionTags => _collisionTags;

    private float _input;

    protected override void Awake()
    {
        Box bound = new Box();
        _physicalComp = new PhysicalComponent(transform, bound);
        bound.UpdateExtents(new Vector2(_lifes[0].transform.localScale.x, _lifes[0].transform.localScale.y * 3f) * 0.5f);
        EnrollEvents(_UpdateMove);
        EnrollEvents(_UpdatePlayerHealthUI);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    public override void Begin()
    {
        base.Begin();
        PhysicalManager.Instance.Add(this);
        int i = -1;
        while (++i < _lifes.Length)
        {
            _lifes[i].color = Color.green;
        }
        Vector2 pos = GameView.Instance.GetViewCenter();
        (float, float) xRange = GameView.Instance.GetRangeHorizontal();
        (float, float) yRange = GameView.Instance.GetRangeVertical();
        pos.x += Random.Range(xRange.Item1 * 0.8f, xRange.Item2 * 0.8f);
        pos.y -= yRange.Item2 * 0.75f;
        _physicalComp.position = pos;
    }

    public override void Over()
    {
        base.Over();

        PhysicalManager.Instance.Del(this);
    }

    public override void GameUpdate(float dt)
    {
    }

    public void OnCollisionWith(IPhysicalObject other)
    {
        other.OnCollisionWith(this);
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

    private void _UpdatePlayerHealthUI(EventParam eventParam)
    {
        switch (eventParam.eventName)
        {
            case EventName.PickupAppleEscape:
                {
                    int currentHealth = ModelManager.Instance.GetModel<PickupModel>().GetPlayerHealth();
                    int i = -1;
                    while (++i < _lifes.Length)
                    {
                        _lifes[i].color = (_lifes.Length - 1 - i) < currentHealth ? Color.green : Color.red;
                    }
                    break;
                }
        }
    }

    private void _UpdateMove()
    {
        float input = Input.GetAxisRaw("Horizontal");
        Vector2 pos = _physicalComp.position;
        pos.x += input * 0.02f;
        if (input < 0f)
        {
            if (!GameView.Instance.IsInSight(_physicalComp.GetVertex2D(0) * 1.01f)) { return; }
        }
        else if (input > 0f)
        {
            if (!GameView.Instance.IsInSight(_physicalComp.GetVertex2D(2) * 1.01f)) { return; }
        }
        _physicalComp.position = pos;
    }
}