using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO gameEventChannel;
    [SerializeField] private ScoreManager scoreManager;

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
        
    }

    private void OnPlayerDeath()
    {
        Debug.Log("Player Death");
    }
}
