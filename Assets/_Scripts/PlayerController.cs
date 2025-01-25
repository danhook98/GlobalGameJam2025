using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 100f; // Can use smoothDamp for smoother movement. 
    [SerializeField] private InputReader inputReader;
    
    private Vector2 _movement = Vector2.zero;
    private Rigidbody2D _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() => inputReader.OnMoveEvent += OnMove;
    private void OnDisable() => inputReader.OnMoveEvent -= OnMove; 

    private void Update()
    {
        // TODO: replace with a larger time interval for checking. 
        transform.position = CameraUtil.ClampPlayerMovement(transform);
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = maxSpeed * Time.fixedDeltaTime * _movement;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            //Call Event PlayerDeath()
            //Play Animation, wait for it to end
            // TODO: look at object pooling if we have time.
            Destroy(gameObject);
        }
    }

    private void OnMove(Vector2 input)
    {
        _movement = input;
    }
}