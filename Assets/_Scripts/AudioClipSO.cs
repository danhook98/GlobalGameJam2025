using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "AudioClip", menuName = "BubbleGame/Audio Clip")]
public class AudioClipSO : ScriptableObject
{
    public AudioClip clip;
}