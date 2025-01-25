using System;
using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int scorePerSecond = 2;
    [SerializeField] private PlayerScoreSO playerScore;
    
    private int _score;
    private float _scoreDelay;
    private float _nextScoreTime; 

    private void Awake()
    {
        _scoreDelay = 1f / scorePerSecond;
        _nextScoreTime = Time.time + _scoreDelay;
    }

    // There's probably a better way to do this for increasing the score. I did try a coroutine, but it had a weird
    // hitch/delay every second score increase, which made it look weird. This will do for now. 
    private void Update()
    {
        if (_nextScoreTime > Time.time) return;
        
        _score++; 
        playerScore.score = _score; // Update the player score data.
        _nextScoreTime = Time.time + _scoreDelay;
    }
}
