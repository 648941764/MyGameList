using UnityEngine;

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