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
        gameEventChannel.OnPlayerDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        gameEventChannel.OnPlayerDeath -= OnPlayerDeath;
    }

    // Main game loop. 
    private void Update()
    {
        if (_gameLost) return; 
    }

    private void OnPlayerDeath()
    {
        Debug.Log("Player Death");
        _gameLost = true;
        // TODO: Stop obstacle spawning.
        // TODO: Display game over canvas. 
    }
}
