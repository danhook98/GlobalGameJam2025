using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _obstacles;
    //private int minSpawn = 1;
    //private int maxSpawn = 3;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) 
        {
            SpawnObstacle();
        }
    }
    
    public void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, _obstacles.Length);
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-2, 3), 5, Random.Range(-2, 3));
        int randomSpawnNumber = Random.Range(1, 4);

        for (int i = 0; i < randomSpawnNumber; i++)
        {
            Instantiate(_obstacles[randomIndex], randomSpawnPosition, Quaternion.identity);
        }
    }
}
