using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
     [SerializeField] private Bricks _brickPrefab;
     [SerializeField] private float _spawnInterval = 3f;
     [SerializeField] private float _minY = -3f;
     [SerializeField] private float _maxY = 3f;

    private float _spawnTimer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimer -= Time.deltaTime; // decrease the spawn timer by the time that has passed since the last frame
        if (_spawnTimer <= 0f)
        {
            SpawnBrick();   
            _spawnTimer = _spawnInterval; // reset the spawn timer
        }
    }

    private void SpawnBrick()
    {
        // Instantiate a new brick at the spawner's position
        Vector2 spawnPosition = transform.position;  // the brick will be spawned at the spawner's position
        spawnPosition.y += Random.Range(_minY, _maxY); // add some vertical randomness to the spawn position
        Instantiate(_brickPrefab, spawnPosition, Quaternion.identity);  // Quaternion.identity means no rotation // the brick will be spawned with its original rotation
    }
}
