using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioEventChannelSO audioEventChannel;
    
    [SerializeField] private AudioSource _audioSourceSfx;
    [SerializeField] private AudioSource _audioSourceMusic;

    // private void Awake()
    // {
    //     _audioSource = GetComponent<AudioSource>();
    //
    //     if (!_audioSource)
    //     {
    //         _audioSource = gameObject.AddComponent<AudioSource>();
    //     }
    // }

    private void OnEnable()
    {
        audioEventChannel.OnAudioPlayRequested += PlayAudio;
    }

    private void OnDisable()
    {
        audioEventChannel.OnAudioPlayRequested -= PlayAudio;
    }

    private void PlayAudio(AudioClipSO audioClip)
    {
        _audioSourceSfx.PlayOneShot(audioClip.clip);
    }
}
