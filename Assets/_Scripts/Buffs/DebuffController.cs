using UnityEngine;

public class DebuffController : ObstacleEntity
{
    [SerializeField] private GameEventChannelSO gameEventChannel;
    [SerializeField] private DebuffTypes debuffType;
    [SerializeField] private AudioEventChannelSO AudioEventChannel;
    [SerializeField] private AudioClipSO debuffcollectionclip;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return; 
        
        Debug.Log("Triggered debuff collect");
        AudioEventChannel.PlayAudio(debuffcollectionclip);
        gameEventChannel.DebuffCollected(debuffType);
    }
}
