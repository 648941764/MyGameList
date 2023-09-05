using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartGameManager : MonoBehaviour
{
    private static SmartGameManager _instance;
    public static SmartGameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("SmartGameManager");
                _instance = go.AddComponent<SmartGameManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
