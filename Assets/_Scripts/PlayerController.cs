using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 100f; // Can use smoothDamp for smoother movement. 
    [SerializeField] private InputReader inputReader;
    
    private Vector2 _movement = Vector2.zero;
    private Rigidbody2D _rigidbody2d;
    private Transform _transform;
    private Camera _camera;
    
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
        _rigidbody2d.linearVelocity = maxSpeed * Time.fixedDeltaTime * _movement;
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