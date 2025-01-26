using System;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO gameEventChannel;
    [SerializeField] private PlayerScoreSO playerScore;
    
    private TextMeshProUGUI _scoreText;

    private bool _displayScore = true;

    private void OnEnable()
    {
        gameEventChannel.OnPlayerDeath += StopScoreUpdating;
    }

    private void OnDisable()
    {
        gameEventChannel.OnPlayerDeath -= StopScoreUpdating;
    }

    private void Awake()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (!_displayScore) return;
        _scoreText.text = $"Score: {playerScore.score}";
    }

    private void StopScoreUpdating() => _displayScore = false;
}
