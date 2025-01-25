using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private SceneEventChannelSO sceneEventChannel;

    private void OnEnable()
    {
        sceneEventChannel.OnSceneChange += ChangeScene;
    }

    private void OnDisable()
    {
        sceneEventChannel.OnSceneChange -= ChangeScene;
    }

    private void ChangeScene(SceneStorageSO scene)
    { 
        SceneManager.LoadScene(scene.scene);
    }
}
