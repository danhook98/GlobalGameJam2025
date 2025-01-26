using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO gameEventChannel;
    
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private GameObject[] buffs;
    [SerializeField] private GameObject[] debuffs;

    [SerializeField] private float buffDebuffSpawnChance = 0.1f;
    [SerializeField] private float buffOverDebuffChance = 0.6f;
    
    private List<GameObject> _spawnedObstacles;
    List<Tuple<GameObject, Vector2>> _obstaclesToSpawn;

    private Camera _camera; 
    
    private float _xLeftMax; 
    private float _xRightMax;

    private float _screenWidth;

    private bool _speedBuffActive = false;
    
    private bool _obstacleRemovalBuffActive = false;
    private int _wavesToIgnore = 0;
    private int _currentIgnoredWaves = 0;

    private void OnEnable()
    {
        gameEventChannel.OnBuffBoostTriggered += TriggerSpeedBuff;
        gameEventChannel.OnBuffObstacleRemovalTriggered += TriggerObstacleRemovalBuff;
    }

    private void OnDisable()
    {
        gameEventChannel.OnBuffBoostTriggered -= TriggerSpeedBuff;
        gameEventChannel.OnBuffObstacleRemovalTriggered -= TriggerObstacleRemovalBuff;
    }

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _xLeftMax = CameraUtil.GetScreenLeftX(_camera);
        _xRightMax = CameraUtil.GetScreenRightX(_camera);
        
        _screenWidth = _xRightMax - _xLeftMax;
        
        _spawnedObstacles = new List<GameObject>();
        _obstaclesToSpawn = new List<Tuple<GameObject, Vector2>>();
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
        if (_obstacleRemovalBuffActive)
        {
            if (_currentIgnoredWaves < _wavesToIgnore)
            {
                _currentIgnoredWaves++;
                return;
            }
            else
            {
                _obstacleRemovalBuffActive = false;
            }
        }
        
        int randomSpawnNumber = Random.Range(1, 4); // How many obstacles should spawn
        
        _obstaclesToSpawn.Clear();

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
                obstacle = GetRandomObstacle(obstacles);
            }
            
            // This creates a value that defines the width of 'columns' in world space. The more obstacles, the more
            // 'columns' there will be. 
            float spawnZoneWidth = _screenWidth / randomSpawnNumber;
            
            // Calculate a position within the current iteration's 'column'. 
            float position = (i * spawnZoneWidth) + Random.Range(0, spawnZoneWidth);
            Vector2 spawnPosition = new(position, 5f);
            
            _obstaclesToSpawn.Add(new Tuple<GameObject, Vector2>(obstacle, spawnPosition));
        }

        // TODO: convert to a camera util call. 
        //Vector2 topLeftX = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
        Vector2 topLeftX = new(CameraUtil.GetScreenLeftX(_camera), 5f);
        
        foreach ((GameObject obstacle, Vector2 position) in _obstaclesToSpawn)
        {
            GameObject spawnedObject = Instantiate(obstacle, topLeftX + position, obstacle.transform.rotation);

            if (_speedBuffActive)
            {
                spawnedObject.GetComponent<ObstacleEntity>().MoveSpeed = 10f;
            }
            
            _spawnedObstacles.Add(spawnedObject);
        }
    }

    private GameObject GetRandomObstacle(GameObject[] obstacles)
    {
        int randomIndex = Random.Range(0, obstacles.Length);
        return obstacles[randomIndex];
    }

    private void SetObstacleSpeeds(float speed)
    {
        foreach (GameObject obstacle in _spawnedObstacles)
        {
            obstacle.GetComponent<ObstacleEntity>().MoveSpeed = speed;
        }
    }

    #region Buffs and Debuffs
    private void TriggerSpeedBuff(float time)
    {
        _speedBuffActive = true;
        StartCoroutine(ManageSpeedBuff(time));
    }

    private IEnumerator ManageSpeedBuff(float time)
    {
        SetObstacleSpeeds(10f);
        yield return new WaitForSeconds(time);
        _speedBuffActive = false;
    }

    private void TriggerObstacleRemovalBuff(int wavesToIgnore)
    {
        _wavesToIgnore = wavesToIgnore;
        _currentIgnoredWaves = 0;
        _obstacleRemovalBuffActive = true;
    }
    #endregion
}
