using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private GameObject[] buffs;
    [SerializeField] private GameObject[] debuffs;

    [SerializeField] private float buffDebuffSpawnChance = 0.1f;
    [SerializeField] private float buffOverDebuffChance = 0.6f;

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
            GameObject obstacle;
            
            bool shouldSpawnBuffDebuff = Random.value < buffDebuffSpawnChance;

            if (shouldSpawnBuffDebuff)
            {
                obstacle = Random.value < buffOverDebuffChance ? GetRandomObstacle(buffs) : GetRandomObstacle(debuffs);
            }
            else
            {
                obstacle = obstacles[Random.Range(0, obstacles.Length)];
            }
            
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-2, 3), 5, Random.Range(-2, 3));
            
            Instantiate(obstacle, randomSpawnPosition, obstacle.transform.rotation);
        }
    }

    private GameObject GetRandomObstacle(GameObject[] obstacles)
    {
        int randomIndex = Random.Range(0, obstacles.Length);
        return obstacles[randomIndex];
    }
}
