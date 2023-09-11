using Excalibur.Physical;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Apple : Character, IPhysicalObject
{
    [SerializeField] private float _upTime = 0.5f;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private PhysicalComponent _physicalComp;
    private float _startAngle;
    private float _upTimer = 0f;
    /// <summary> 向右为true，向左为负 </summary>
    private bool _moveDir;
    private AppleData _data;

    public PhysicalComponent PhysicalComponent => _physicalComp;

    public string Tag => "Apple";
    private readonly HashSet<string> _collisionTags = new HashSet<string>();
    public HashSet<string> CollisionTags => _collisionTags;

    protected override void Awake()
    {
        _physicalComp = new PhysicalComponent(transform, new Box());
        EnrollEvents(_ =>
        {
            switch (_.eventName)
            {
                case EventName.GameOver:
                    {
                        ApplePool.ReleaseApple(this);
                        break;
                    }
            }
        });
    }

    public override void Begin()
    {
        base.Begin();
        PhysicalManager.Instance.Add(this);
    }

    public override void Over()
    {
        base.Over();
        PhysicalManager.Instance.Del(this);
    }

    public override void GameUpdate(float dt)
    {
        _upTimer += dt;
        if (_upTimer <= _upTime)
        {
            float upDis = Mathf.Abs(Mathf.Lerp(_startAngle, 0, _upTimer / _upTime) * dt * 0.1f);
            _physicalComp.position += Vector3.up * upDis;
        }
        else
        {
            _physicalComp.position -= new Vector3(0f, _data.Speed * dt, 0f);
            if (!GameView.Instance.IsInSight(_physicalComp.GetVertex2D(0)))
            {
                // 掉出屏幕外，扣Player血量
                Broadcast(ParamPool.Get(EventName.PickupAppleEscape));
                ApplePool.ReleaseApple(this);
            }
        }
    }

    public void OnCollisionWith(IPhysicalObject other)
    {
        if (other.Tag == "PickupPlayer")
        {
            ModelManager.Instance.GetModel<PickupModel>().AddScore(_data);
            ApplePool.ReleaseApple(this);
        }
    }

    public void Throw(Vector3 throwPos, float angle)
    {
        _data = new AppleData();
        _spriteRenderer.color = _data.Color;
        _physicalComp.position = throwPos;
        _startAngle = angle;
        _moveDir = Mathf.Sign(angle) == 1;
        _upTimer = 0f;
        Begin();
    }

    public static class ApplePool
    {
        private static Apple _prefab;
        private static Transform _appleParent;
        private static readonly ObjectPool<Apple> _apllePool = new ObjectPool<Apple>(
            200,
            default,
            _ => _.gameObject.SetActive(false),
            _ => Object.Destroy(_.gameObject)
            );

        private static Transform appleParent
        {
            get
            {
                if (_appleParent == null)
                {
                    _appleParent = GameObject.Find("Apples").transform;
                }
                return _appleParent;
            }
        }

        public static Apple GetApple()
        {
            Apple apple;
            if (_apllePool.currentCount == 0)
            {
                if (_prefab == null)
                {
                    _prefab = Resources.Load<Apple>("Prefab/Pickup/Apple");
                }
                apple = Object.Instantiate(_prefab, appleParent);
            }
            else
            {
                apple = _apllePool.Get();
            }
            return apple;
        }

        public static void ReleaseApple(Apple apple)
        {
            apple.Over();
            _apllePool.Release(apple);
        }
    }
}