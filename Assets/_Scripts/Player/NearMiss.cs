using System;
using UnityEngine;

public class NearMiss : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO gameEventChannel;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("Near Miss!");
            gameEventChannel.ChangeScoreMultiplier(2, 0.5f);
        }
    }

}
