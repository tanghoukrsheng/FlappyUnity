using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
     [SerializeField] private Bricks _brickPrefab;
    [SerializeField] private float _spawnInterval = 3f;
    [Tooltip("If > 0, spawner will compute spawn interval to keep this horizontal spacing (world units) between bricks.")]
    [SerializeField] private float _desiredSpacing = 0f;
    [SerializeField] private float _minY = 1f;
    [SerializeField] private float _maxY = 5f;

    private float _spawnTimer;
    private float _baseSpawnInterval;
    private float _baseSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // store base values so we can scale spawn interval when speed changes
        _baseSpawnInterval = _spawnInterval;
        _baseSpeed = Mathf.Max(0.001f, Bricks.CurrentMoveSpeed);

        // initialize spawn timer
        if (_desiredSpacing > 0f)
            _spawnTimer = _desiredSpacing / _baseSpeed;
        else
            _spawnTimer = _baseSpawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimer -= Time.deltaTime; // decrease the spawn timer by the time that has passed since the last frame
        if (_spawnTimer <= 0f)
        {
            SpawnBrick();
            // reset spawn timer; compute interval based on current brick speed
            if (_desiredSpacing > 0f)
            {
                _spawnTimer = _desiredSpacing / Mathf.Max(0.001f, Bricks.CurrentMoveSpeed);
            }
            else
            {
                // scale the base interval inversely with current move speed so spacing stays similar
                _spawnTimer = _baseSpawnInterval * (_baseSpeed / Mathf.Max(0.001f, Bricks.CurrentMoveSpeed));
            }
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
