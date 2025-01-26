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
    public event UnityAction<float> OnBuffBoostTriggered;
    public event UnityAction<int> OnBuffObstacleRemovalTriggered;

    public event UnityAction<DebuffTypes> OnDebuffCollected;
    public event UnityAction<float> OnDebuffPoisonTriggered;
    public event UnityAction<float> OnDebuffBigBubbleTriggered;
    public event UnityAction OnDebuffCrazyObstaclesTriggered;
    
    // Score events.
    public event UnityAction<int, float> OnChangeScoreMultiplier; 

    // Player event triggers.
    public void PlayerHasDied() => OnPlayerDeath?.Invoke();
    
    // Buff/debuff event triggers.
    public void BuffCollected(BuffTypes buffTypeType) => OnBuffCollected?.Invoke(buffTypeType);
    public void TriggerShieldBuff(float time) => OnBuffShieldTriggered?.Invoke(time);
    public void TriggerBoostBuff(float time) => OnBuffBoostTriggered?.Invoke(time);
    public void TriggerObstacleRemovalBuff(int waves) => OnBuffObstacleRemovalTriggered?.Invoke(waves);
    
    public void DebuffCollected(DebuffTypes buffType) => OnDebuffCollected?.Invoke(buffType);
    public void TriggerPoisonDebuff(float time) => OnDebuffPoisonTriggered?.Invoke(time);
    public void TriggerBigBubbleDebuff(float time) => OnDebuffBigBubbleTriggered?.Invoke(time);
    public void TriggerCrazyObstacleDebuff() => OnDebuffCrazyObstaclesTriggered?.Invoke();
    
    
    // Score event triggers.
    public void ChangeScoreMultiplier(int scoreMultiplier, float time) => OnChangeScoreMultiplier?.Invoke(scoreMultiplier, time);
}
