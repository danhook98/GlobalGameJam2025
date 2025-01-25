using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _obstacles;

    void Update()
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
            int randomIndex = Random.Range(0, _obstacles.Length); // Which object from the array spawns
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-2, 3), 5, Random.Range(-2, 3));
            Instantiate(_obstacles[randomIndex], randomSpawnPosition, Quaternion.identity);
        }
    }
}
