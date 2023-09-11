using Excalibur.Physical;
using System.Collections.Generic;
using UnityEngine;

public sealed class PickupPlayer : Character, IPhysicalObject
{
    [SerializeField] private SpriteRenderer[] _lifes;

    private PhysicalComponent physicalComp;
    public PhysicalComponent PhysicalComponent => physicalComp;

    public string Tag => "PickupPlayer";
    private readonly HashSet<string> _collisionTags = new HashSet<string>();
    public HashSet<string> CollisionTags => _collisionTags;

    private float _input;

    protected override void Awake()
    {
        physicalComp = new PhysicalComponent(transform, new Box());

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
        Vector2 pos = physicalComp.position;
        pos.x += input * 2f;
        if (!GameView.Instance.IsInSight(pos))
        {
            return;
        }
        physicalComp.position = pos;
    }
}