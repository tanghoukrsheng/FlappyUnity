using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird: MonoBehaviour
{

    [SerializeField] private float _flapForce = 10f;
    [SerializeField] private float _rotation = 1.5f;
    private const int leftButton = 0;
    private Rigidbody2D _rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) // 0 = left mouse
        {
            Flap();   
        }

         if ( Input.GetKeyDown(KeyCode.R) )
        {
            ReloadScene();   
        }



        _rb.MoveRotation(_rb.linearVelocityY * _rotation); // point nose downward/upward

    
    }

    private void Flap()
    {
        
        _rb.linearVelocityY = 0f;
        _rb.AddForce(Vector2.up * _flapForce, ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Time.timeScale = 0f; // stop the game

    }

    private void ReloadScene()
    {
        Time.timeScale = 1f; // resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reload the current scene
    }

}

