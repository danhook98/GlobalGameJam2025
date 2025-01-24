using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneStorage", menuName = "BubbleGame/Scene Storage")]
public class SceneStorageSO : ScriptableObject
{
    public Scene scene;
}
