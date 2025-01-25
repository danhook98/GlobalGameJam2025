using UnityEngine;

public class BuffController : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO gameEventChannel;
    [SerializeField] private BuffTypes buffType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return; 
        
        Debug.Log("Triggered buff collect");
        gameEventChannel.BuffCollected(buffType);
    }
}
