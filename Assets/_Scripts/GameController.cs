using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO gameEventChannel;
    [SerializeField] private ScoreManager scoreManager;

    private bool _gameLost = false;

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

    // Main game loop. 
    private void Update()
    {
        if (_gameLost) return; 
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
    }

    private void OnDebuffCollected(DebuffTypes buffType)
    {
        Debug.Log("Debuff Collected: " + buffType);
    }
    #endregion
}
