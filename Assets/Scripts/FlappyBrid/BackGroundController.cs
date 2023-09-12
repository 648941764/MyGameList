using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 _startPos;
    private Vector2 _endPos = new Vector2(-6.75f, -0.09f);

    private void Awake()
    {
        _startPos = transform.position;
    }
    private void Update()
    {
        transform.Translate(-speed, 0, 0);
        if (transform.position.x <= _endPos.x)
        {
            transform.position = _startPos;
        }
    }
}
//暂停游戏的方法是Time.TiemScale
