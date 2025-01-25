using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    
    private Rigidbody2D _rigidbody2d;
    private Camera _camera;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        // Destroy the game object if the obstacle gets too far off screen. 
        if (_rigidbody2d.position.y < CameraUtil.GetScreenBottomY(_camera) - 3f)
        {
            Destroy(gameObject);
        }
        
        Vector2 movement = Time.fixedDeltaTime * moveSpeed * Vector2.down;
        _rigidbody2d.MovePosition(_rigidbody2d.position + movement);
    }
}
