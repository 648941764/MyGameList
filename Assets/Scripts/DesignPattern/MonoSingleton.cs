using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindAnyObjectByType<T>();
                if (_instance == null)
                {
                    _instance = (new GameObject(typeof(T).Name)).AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake() { }
    protected virtual void Start() { }
    protected virtual void Update() { }
}
