using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEventChannel", menuName = "BubbleGame/Game Event Channel")]
public class GameEventChannelSO : ScriptableObject
{
    // Player events
    public event UnityAction OnPlayerDeath;
    
    // Buffs and debuff events
    public event UnityAction<BuffTypes> OnBuffCollected;
    public event UnityAction OnBuffShieldTriggered;
    public event UnityAction OnBuffBoostTriggered;
    public event UnityAction OnBuffPlatformRemovalTriggered;

    public event UnityAction<DebuffTypes> OnDebuffCollected;
    public event UnityAction OnDebuffPoisonTriggered;
    public event UnityAction OnDebuffBigBubbleTriggered;
    public event UnityAction OnDebuffCrazyPlatformsTriggered;
    
    // Score events.
    public event UnityAction<int, float> OnChangeScoreMultiplier; 

    // Player event triggers.
    public void PlayerHasDied() => OnPlayerDeath?.Invoke();
    
    // Buff/debuff event triggers.
    public void BuffCollected(BuffTypes buffTypeType) => OnBuffCollected?.Invoke(buffTypeType);
    public void TriggerShieldBuff() => OnBuffShieldTriggered?.Invoke();
    public void TriggerBoostBuff() => OnBuffBoostTriggered?.Invoke();
    public void TriggerPlatformRemovalBuff() => OnBuffPlatformRemovalTriggered?.Invoke();
    
    public void DebuffCollected(DebuffTypes buffType) => OnDebuffCollected?.Invoke(buffType);
    public void TriggerPoisonDebuff() => OnDebuffPoisonTriggered?.Invoke();
    public void TriggerBigBubbleDebuff() => OnDebuffBigBubbleTriggered?.Invoke();
    public void TriggerCrazyPlatformsDebuff() => OnDebuffCrazyPlatformsTriggered?.Invoke();
    
    
    // Score event triggers.
    public void ChangeScoreMultiplier(int scoreMultiplier, float time) => OnChangeScoreMultiplier?.Invoke(scoreMultiplier, time);
}
