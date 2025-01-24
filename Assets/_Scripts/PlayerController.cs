using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _halfPlayerSizeX;
    private Camera _camera;

    [SerializeField] private float maxSpeed = 100f; // Can use smoothDamp for smoother movement. Use this variable to allow movement.
    [SerializeField] private InputReader inputReader;
    
    private Vector2 _movement = Vector2.zero;
    private Rigidbody2D _rigidbody;
    
    private void Awake()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody2D>();
        _halfPlayerSizeX= GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void OnEnable()
    {
        inputReader.OnMoveEvent += OnMove;
    }

    private void OnDisable()
    {
        inputReader.OnMoveEvent -= OnMove;
    }

    private void Update()
    {
        ClampPlayerMovement();
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = maxSpeed * Time.fixedDeltaTime * _movement;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            //Play Animation, wait for it to end
            //Delete bubble
        }
    }

    private void ClampPlayerMovement()
    {
        Vector3 position = transform.position;
        
        float distance = transform.position.z - _camera.transform.position.z;
        
        float leftBorder = _camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + _halfPlayerSizeX;
        float rightBorder = _camera.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - _halfPlayerSizeX;
        
        position.x = Mathf.Clamp(position.x, leftBorder, rightBorder);
        transform.position = position;
    }

    private void OnMove(Vector2 input)
    {
        _movement = input;
    }
}