using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameEventChannelSO gameEventChannel;
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 15f; // Can use smoothDamp for smoother movement. 
    
    private Rigidbody2D _rigidbody2d;
    private Transform _transform;
    private Camera _camera;
    
    private Vector2 _movement = Vector2.zero;
    private Vector2 _velocity;
    
    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _camera = Camera.main;
    }

    private void OnEnable() => inputReader.OnMoveEvent += OnMove;
    private void OnDisable() => inputReader.OnMoveEvent -= OnMove; 

    private void Update()
    {
        // TODO: replace with a larger time interval for checking. 
        _transform.position = CameraUtil.ClampPlayerMovement(_transform, _camera);
    }

    private void FixedUpdate()
    {
        // _rigidbody2d.linearVelocity = maxSpeed * Time.fixedDeltaTime * _movement;
        // Vector2 movement = Time.fixedDeltaTime * moveSpeed * _movement;
        // _rigidbody2d.MovePosition(_rigidbody2d.position + movement);
        Vector2 targetVelocity = moveSpeed * _movement;
        _rigidbody2d.linearVelocity = Vector2.SmoothDamp(_rigidbody2d.linearVelocity, targetVelocity, ref _velocity, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (!collision.gameObject.CompareTag("Obstacle")) return;
         
        //Play Animation, wait for it to end
        // TODO: look at object pooling if we have time.
        gameEventChannel.PlayerHasDied();
        Destroy(gameObject);
    }

    private void OnMove(Vector2 input)
    {
        _movement.x = input.x;
    }
}