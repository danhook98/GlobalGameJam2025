using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEventChannel", menuName = "BubbleGame/Game Event Channel")]
public class GameEventChannelSO : ScriptableObject
{
    // Player events
    public event UnityAction OnPlayerDeath;
    
    // Buffs and debuff events
    public event UnityAction<BuffTypes> OnBuffCollected;
    public event UnityAction<DebuffTypes> OnDebuffCollected;

    public void PlayerHasDied() => OnPlayerDeath?.Invoke();
    
    public void BuffCollected(BuffTypes buffTypeType) => OnBuffCollected?.Invoke(buffTypeType);
    public void DebuffCollected(DebuffTypes buffType) => OnDebuffCollected?.Invoke(buffType);
}
