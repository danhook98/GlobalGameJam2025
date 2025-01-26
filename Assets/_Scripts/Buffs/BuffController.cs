using System;
using UnityEngine;

public class BuffController : ObstacleEntity
{
    [SerializeField] private GameEventChannelSO gameEventChannel;
    [SerializeField] private BuffTypes buffType;
    [SerializeField] private AudioEventChannelSO AudioEventChannel;
    [SerializeField] private AudioClipSO buffcollectionclip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return; 
        
        Debug.Log("Triggered buff collect");
        AudioEventChannel.PlayAudio(buffcollectionclip);
        gameEventChannel.BuffCollected(buffType);
    }
}