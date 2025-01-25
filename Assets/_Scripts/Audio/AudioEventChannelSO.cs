using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AudioEventChannel", menuName = "BubbleGame/Audio Event Channel")]
public class AudioEventChannelSO : ScriptableObject
{
    public event UnityAction<AudioClipSO> OnAudioPlayRequested; 
    
    public void PlayAudio(AudioClipSO audioClipSo) => OnAudioPlayRequested?.Invoke(audioClipSo);
}