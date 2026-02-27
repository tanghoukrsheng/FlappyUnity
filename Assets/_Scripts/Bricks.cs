using UnityEngine;
using UnityEngine.SceneManagement;

public class Bricks : MonoBehaviour
{

    [SerializeField] private float _lifecycle = 10f; // the time in seconds after which the brick will be destroyed
    // per-prefab/static counters to track spawned bricks and base speed for newly spawned bricks
    private static int _spawnCounter = 0;
    private static float _movespeed = 3f;
    private const float DefaultMoveSpeed = 3f;
    private static bool s_sceneHooked = false;

    // Expose current base move speed so spawners can adapt spacing
    public static float CurrentMoveSpeed => _movespeed; // public getter for current move speed, so other scripts can access it but not modify it


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, _lifecycle); // destroy the brick after _lifecycle seconds


        // Increment spawn counter once when the brick is created (not every frame)
        _spawnCounter++;

        // Increase base speed for subsequent bricks every 5 spawns
        if (_spawnCounter % 5 == 0 && _spawnCounter != 0) // every 5 spawns, but not on the very first spawn
        {
            _movespeed += 2f;
        }

        // Hook sceneLoaded once per editor/play session so we can reset statics when a new game starts
        if (!s_sceneHooked)
        {
            SceneManager.sceneLoaded += OnSceneLoaded; // 
            s_sceneHooked = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move left at this brick's assigned speed (frame-rate independent)
        transform.Translate(Vector2.left * _movespeed * Time.deltaTime);
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "IntroScene" || scene.name == "Play")
        {
            _movespeed = DefaultMoveSpeed;
            _spawnCounter = 0;
            Debug.Log($"Bricks: static state reset for scene '{scene.name}' (moveSpeed={_movespeed}).");
        }
    }
}
