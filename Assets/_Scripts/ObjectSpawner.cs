using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private GameObject[] buffs;
    [SerializeField] private GameObject[] debuffs;

    private Camera _camera; 
    
    private float _xLeftMax; 
    private float _xRightMax;

    private float _screenWidth; 

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _xLeftMax = CameraUtil.GetScreenLeftX(_camera);
        _xRightMax = CameraUtil.GetScreenRightX(_camera);
        
        _screenWidth = _xRightMax - _xLeftMax;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) 
        {
            SpawnObstacle();
        }
    }
    
    public void SpawnObstacle()
    {
        int randomSpawnNumber = Random.Range(1, 4); // How many obstacles should spawn
        
        for (int i = 0; i < randomSpawnNumber; i++)
        {
            int randomIndex = Random.Range(0, obstacles.Length); // Which object from the array spawns
            
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-2, 3), 5, Random.Range(-2, 3));
            
            Instantiate(obstacles[randomIndex], randomSpawnPosition, obstacles[randomIndex].transform.rotation);
        }
    }
}
