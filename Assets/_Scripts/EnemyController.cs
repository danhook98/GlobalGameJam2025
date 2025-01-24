using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    
    private Rigidbody2D _rigidbody2d;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 movement = Time.fixedDeltaTime * moveSpeed * Vector2.down;
        _rigidbody2d.MovePosition(_rigidbody2d.position + movement);
    }
}
