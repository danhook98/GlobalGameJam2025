using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameEventChannelSO gameEventChannel;
    [SerializeField] private ObjectSpawner objectSpawner;
    [SerializeField] private ScoreManager scoreManager;

    [Header("Spawn Variables")] 
    [SerializeField] private float spawnIntervalMin = 0.5f;
    [SerializeField] private float spawnIntervalMax = 2f;

    [Header("Buff/Debuff Variables")] 
    [SerializeField] private float shieldBuffTime = 10f;
    [SerializeField] private float boostBuffTime = 5f;
    
    [Header("Debug")]
    [SerializeField] private bool shouldSpawn = false;
    
    private bool _gameLost = false;
    private bool _buffActive = false;
    private bool _debuffActive = false;
    
    private float _nextSpawnTime;

    private void OnEnable()
    {
        // Player events.
        gameEventChannel.OnPlayerDeath += OnPlayerDeath;
        
        // Buff/debuff events.
        gameEventChannel.OnBuffCollected += OnBuffCollected;
        gameEventChannel.OnDebuffCollected += OnDebuffCollected;
    }

    private void OnDisable()
    {
        // Player events.
        gameEventChannel.OnPlayerDeath -= OnPlayerDeath;
        
        // Buff/debuff events.
        gameEventChannel.OnBuffCollected -= OnBuffCollected;
        gameEventChannel.OnDebuffCollected -= OnDebuffCollected;
    }

    private void Start()
    {
        _nextSpawnTime = Time.time + GetRandomSpawnTime();
    }

    // Main game loop. 
    private void Update()
    {
        if (_gameLost) return;

        // Spawn the next wave. 
        if (_nextSpawnTime < Time.time)
        {
            if (shouldSpawn)
                objectSpawner.SpawnObstacle();
            
            _nextSpawnTime = Time.time + GetRandomSpawnTime();
        }
    }

    private float GetRandomSpawnTime()
    {
        return Random.Range(spawnIntervalMin, spawnIntervalMax);
    }

    #region Player Events
    private void OnPlayerDeath()
    {
        Debug.Log("Player Death");
        _gameLost = true;
        // TODO: Stop obstacle spawning.
        // TODO: Display game over canvas. 
    }
    #endregion
    
    #region Buff/Debuff events
    private void OnBuffCollected(BuffTypes buffType)
    {
        Debug.Log("Buff Collected: " + buffType);

        if (_buffActive || _debuffActive) return;

        switch (buffType)
        {
            case BuffTypes.Shield:
                gameEventChannel.TriggerShieldBuff(shieldBuffTime);
                StartCoroutine(ResetBuffDebuffState(shieldBuffTime));
                break;
            case BuffTypes.Boost:
                break;
            case BuffTypes.ObstacleRemoval:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(buffType), buffType, null);
        }
        
        
    }

    private void OnDebuffCollected(DebuffTypes buffType)
    {
        Debug.Log("Debuff Collected: " + buffType);
        
        if (_buffActive || _debuffActive) return;
    }
    #endregion

    private IEnumerator ResetBuffDebuffState(float timeUntilReset)
    {
        yield return new WaitForSeconds(timeUntilReset);
        _buffActive = false;
        _debuffActive = false;
    }
}
