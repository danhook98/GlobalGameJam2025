using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameEventChannelSO gameEventChannel;
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 15f; // Can use smoothDamp for smoother movement. 
    
    // Component references.
    private Rigidbody2D _rigidbody2d;
    private Transform _transform;
    private Camera _camera;
    private CircleCollider2D _circleCollider2D;
    
    // Movement variables.
    private Vector2 _movement;
    private Vector2 _velocity;
    
    // Buff/debuff variables.
    private bool _shieldBuffActive = false;
    private bool _boostBuffActive = false;
    
    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _camera = Camera.main;
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void OnEnable()
    {
        inputReader.OnMoveEvent += OnMove;

        gameEventChannel.OnBuffShieldTriggered += TriggerShieldBuff;
        gameEventChannel.OnBuffBoostTriggered += TriggerBoostBuff;
    } 
    
    private void OnDisable()
    {
        inputReader.OnMoveEvent -= OnMove;
        
        gameEventChannel.OnBuffShieldTriggered -= TriggerShieldBuff;
        gameEventChannel.OnBuffBoostTriggered -= TriggerBoostBuff;
    } 

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
         
         // The shield buff is active.
         if (_shieldBuffActive)
         {
             _shieldBuffActive = false;
             return;
         }
         
         //Play Animation, wait for it to end
         // TODO: look at object pooling if we have time.
         gameEventChannel.PlayerHasDied();
         Destroy(gameObject);
    }

    private void OnMove(Vector2 input)
    {
        _movement.x = input.x;
    }

    #region Buffs and Debuffs
    private void TriggerShieldBuff(float time)
    {
        Debug.Log("Shield buff triggered");
        _shieldBuffActive = true;
        
        // TODO: add animation change.

        StartCoroutine(ResetShieldBuffState(time));
    }

    private IEnumerator ResetShieldBuffState(float time)
    {
        yield return new WaitForSeconds(time);

        if (_shieldBuffActive)
            _shieldBuffActive = false; 
    }

    private void TriggerBoostBuff(float time)
    {
        Debug.Log("Boost buff triggered");
        
        _boostBuffActive = true;
        
        // TODO: add animation change.
        _circleCollider2D.enabled = false;
        
        StartCoroutine(ResetBoostBuffState(time));
    }

    private IEnumerator ResetBoostBuffState(float time)
    {
        yield return new WaitForSeconds(time);
        _circleCollider2D.enabled = true;
        _boostBuffActive = false;
    }
    #endregion
}