using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float _halfPlayerSizeX;

    [SerializeField] private float maxSpeed = 100f; // Can use smoothDamp for smoother movement. 
    [SerializeField] private InputReader inputReader;
    [SerializeField] private CameraUtil cameraUtilScript;
    
    private Vector2 _movement = Vector2.zero;
    private Rigidbody2D _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _halfPlayerSizeX= GetComponent<SpriteRenderer>().bounds.size.x/2;
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
        cameraUtilScript.ClampPlayerMovement();
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
            //Delete bubble
            Destroy(gameObject);
        }
    }

    private void OnMove(Vector2 input)
    {
        _movement = input;
    }
}