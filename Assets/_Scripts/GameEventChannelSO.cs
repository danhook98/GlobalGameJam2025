using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEventChannel", menuName = "BubbleGame/Game Event Channel")]
public class GameEventChannelSO : ScriptableObject
{
    public event UnityAction OnPlayerDeath;

    public void PlayerHasDied() => OnPlayerDeath?.Invoke();
}
