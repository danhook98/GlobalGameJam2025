using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEventChannel", menuName = "BubbleGame/Game Event Channel")]
public class GameEventChannelSO : ScriptableObject
{
    // Player events
    public event UnityAction OnPlayerDeath;
    
    // Buffs and debuff events
    public event UnityAction<BuffTypes> OnBuffCollected;
    public event UnityAction<float> OnBuffShieldTriggered;
    public event UnityAction OnBuffBoostTriggered;
    public event UnityAction OnBuffObstacleRemovalTriggered;

    public event UnityAction<DebuffTypes> OnDebuffCollected;
    public event UnityAction OnDebuffPoisonTriggered;
    public event UnityAction OnDebuffBigBubbleTriggered;
    public event UnityAction OnDebuffCrazyObstaclesTriggered;
    
    // Score events.
    public event UnityAction<int, float> OnChangeScoreMultiplier; 

    // Player event triggers.
    public void PlayerHasDied() => OnPlayerDeath?.Invoke();
    
    // Buff/debuff event triggers.
    public void BuffCollected(BuffTypes buffTypeType) => OnBuffCollected?.Invoke(buffTypeType);
    public void TriggerShieldBuff(float time) => OnBuffShieldTriggered?.Invoke(time);
    public void TriggerBoostBuff() => OnBuffBoostTriggered?.Invoke();
    public void TriggerObstacleRemovalBuff() => OnBuffObstacleRemovalTriggered?.Invoke();
    
    public void DebuffCollected(DebuffTypes buffType) => OnDebuffCollected?.Invoke(buffType);
    public void TriggerPoisonDebuff() => OnDebuffPoisonTriggered?.Invoke();
    public void TriggerBigBubbleDebuff() => OnDebuffBigBubbleTriggered?.Invoke();
    public void TriggerCrazyObstacleDebuff() => OnDebuffCrazyObstaclesTriggered?.Invoke();
    
    
    // Score event triggers.
    public void ChangeScoreMultiplier(int scoreMultiplier, float time) => OnChangeScoreMultiplier?.Invoke(scoreMultiplier, time);
}
