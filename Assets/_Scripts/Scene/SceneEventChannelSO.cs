using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "SceneEventChannel", menuName = "BubbleGame/Scene Event Channel")]
public class SceneEventChannelSO : ScriptableObject
{
    public event UnityAction<SceneStorageSO> OnSceneChange;
    
    public void ChangeScene(SceneStorageSO sceneStorageSo) => OnSceneChange?.Invoke(sceneStorageSo);
}
