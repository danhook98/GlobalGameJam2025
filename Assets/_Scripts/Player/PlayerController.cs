using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameEventChannelSO gameEventChannel;
    [SerializeField] private AudioEventChannelSO audioEventChannel;
    [SerializeField] private AudioClipSO popAudioClip;
    private Animator _anim;
    private bool _popped;
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 15f; 
    
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
    
    private bool _poisonDebuffActive = false;
    private float _oldSpeed;
    
    private bool _bigBubbleDebuffActive = false;
    private Vector3 _oldScale;
    private float _oldColliderRadius;
    private float _bigBubbleScale = 1.5f;
    
    // Animations.
    private static readonly int Shield = Animator.StringToHash("Shield");
    private static readonly int NoShield = Animator.StringToHash("NoShield");
    private static readonly int Pop = Animator.StringToHash("Pop");
    
    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _camera = Camera.main;
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _anim = GetComponent<Animator>();
        _popped = false;
    }

    private void OnEnable()
    {
        inputReader.OnMoveEvent += OnMove;

        gameEventChannel.OnBuffShieldTriggered += TriggerShieldBuff;
        gameEventChannel.OnBuffBoostTriggered += TriggerBoostBuff;

        gameEventChannel.OnDebuffPoisonTriggered += TriggerPoisonDebuff;
        gameEventChannel.OnDebuffBigBubbleTriggered += TriggerBigBubbleDebuff;
    } 
    
    private void OnDisable()
    {
        inputReader.OnMoveEvent -= OnMove;
        
        gameEventChannel.OnBuffShieldTriggered -= TriggerShieldBuff;
        gameEventChannel.OnBuffBoostTriggered -= TriggerBoostBuff;
        
        gameEventChannel.OnDebuffPoisonTriggered -= TriggerPoisonDebuff;
        gameEventChannel.OnDebuffBigBubbleTriggered -= TriggerBigBubbleDebuff;
    } 

    private void Update()
    {
        // TODO: replace with a larger time interval for checking. 
        _transform.position = CameraUtil.ClampPlayerMovement(_transform, _camera);
    }

    private void FixedUpdate()
    {
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
             _anim.SetTrigger(NoShield);
             return;
         }

         //Play Animation, wait for it to end
         if (_popped != true)
         { 
             _anim.SetTrigger(Pop); 
             _popped = true; 
         }

         // TODO: look at object pooling if we have time.
         gameEventChannel.PlayerHasDied();
         audioEventChannel.PlayAudio(popAudioClip);
         Destroy(gameObject, 1f);
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
        _anim.SetTrigger(Shield);
        StartCoroutine(ResetShieldBuffState(time));
    }

    private IEnumerator ResetShieldBuffState(float time)
    {
        yield return new WaitForSeconds(time);

        if (_shieldBuffActive)
            _shieldBuffActive = false;
        _anim.SetTrigger(NoShield);
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
    
    private void TriggerPoisonDebuff(float time)
    {
        Debug.Log("Poison buff triggered");

        _poisonDebuffActive = true;

        _oldSpeed = moveSpeed;
        moveSpeed *= 0.7f;
        
        StartCoroutine(ResetPoisonDebuffState(time));
    }

    private IEnumerator ResetPoisonDebuffState(float time)
    {
        yield return new WaitForSeconds(time);
        _poisonDebuffActive = false;
        moveSpeed = _oldSpeed;
    }

    private void TriggerBigBubbleDebuff(float time)
    {
        Debug.Log("Big Bubble buff triggered");
        
        _bigBubbleDebuffActive = true;

        _oldScale = _transform.localScale;
        _oldColliderRadius = _circleCollider2D.radius;
        
        _transform.localScale *= _bigBubbleScale;
        _circleCollider2D.radius *= _bigBubbleScale;
        
        StartCoroutine(ResetBigBubbleDebuffState(time));
    }

    private IEnumerator ResetBigBubbleDebuffState(float time)
    {
        yield return new WaitForSeconds(time);
        _bigBubbleDebuffActive = false;
        _transform.localScale = _oldScale;
        _circleCollider2D.radius = _oldColliderRadius;
    }
    #endregion
}