using Excalibur.Physical;
using UnityEngine;

public class Apple : Character, IPhysicalObject
{
    private PhysicalComponent physicalComp;
    public PhysicalComponent PhysicalComponent => physicalComp;

    protected override void Awake()
    {
        physicalComp = new PhysicalComponent(transform, new Box());
    }

    public override void GameUpdate(float dt)
    {
    }

    public void OnCollisionWith(PhysicalComponent other)
    {
    }
}

public static class ApplePool
{
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
            apple = UnityEngine.Object.Instantiate(UnityEngine.Resources.Load<Apple>("Prefab/Pickup/Apple"), appleParent);
        }
        apple = _apllePool.Get();
        return apple;
    }

    public static void ReleaseApple(Apple apple)
    {
        _apllePool.Release(apple);
    }
}