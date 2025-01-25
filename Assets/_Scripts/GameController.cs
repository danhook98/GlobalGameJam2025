using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO gameEventChannel;
    [SerializeField] private ScoreManager scoreManager;

    // Main game loop. 
    private void Update()
    {
        
    }
}
