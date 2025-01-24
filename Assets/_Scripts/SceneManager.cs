using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private SceneEventChannelSO sceneEventChannel;

    private void OnEnable()
    {
        //sceneEventChannel.OnSceneChange += ChangeScene;
    }

    private void OnDisable()
    {
        //sceneEventChannel.OnSceneChange -= ChangeScene;
    }

    private void ChangeScene(Scene scene)
    {
        //SceneManager.LoadScene(scene.name);
    }
}
