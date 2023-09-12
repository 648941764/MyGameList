using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D _rigBrid;
    private void OnEnable()
    {
       _rigBrid = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Jump();
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigBrid.velocity = new Vector2(_rigBrid.velocity.x, speed);
        }
    }
}
