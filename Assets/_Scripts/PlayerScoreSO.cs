using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScore", menuName = "BubbleGame/PlayerScore")]
public class PlayerScoreSO : ScriptableObject
{
    public int score;

    private void OnEnable() => score = 0;
}
