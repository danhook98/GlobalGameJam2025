using UnityEngine;

public class DebuffController : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO gameEventChannel;
    [SerializeField] private DebuffTypes debuffType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return; 
        
        Debug.Log("Triggered debuff collect");
        gameEventChannel.DebuffCollected(debuffType);
    }
}
