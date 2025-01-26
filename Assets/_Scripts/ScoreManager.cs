using System;
using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int scorePerSecond = 2;
    [SerializeField] private PlayerScoreSO playerScore;
    [SerializeField] private GameEventChannelSO gameEventChannel;
    
    private int _score;
    private int _multiplier; 
    private float _scoreDelay;
    private float _nextScoreTime;

    private void OnEnable()
    {
        gameEventChannel.OnChangeScoreMultiplier += SetScoreMultiplier;
    }

    private void OnDisable()
    {
        gameEventChannel.OnChangeScoreMultiplier -= SetScoreMultiplier;
    }

    private void Awake()
    {
        _scoreDelay = 1f / scorePerSecond;
        _multiplier = 1;
        _nextScoreTime = Time.time + _scoreDelay;
    }

    // There's probably a better way to do this for increasing the score. I did try a coroutine, but it had a weird
    // hitch/delay every second score increase, which made it look weird. This will do for now. 
    private void Update()
    {
        if (_nextScoreTime > Time.time) return;
        
        _score += 1 * _multiplier; 
        playerScore.score = _score; // Update the player score data.
        Debug.Log("Score: " + _score);
        _nextScoreTime = Time.time + _scoreDelay;
    }

    private void SetScoreMultiplier(int multiplier, float time)
    {
        _multiplier = multiplier;
        StartCoroutine(ResetScoreMultiplier(time));
        Debug.Log(_multiplier);
    }

    private IEnumerator ResetScoreMultiplier(float time)
    {
        yield return new WaitForSeconds(time);
        _multiplier = 1;
        Debug.Log("Reset score multiplier: " + _multiplier);
    }
}
