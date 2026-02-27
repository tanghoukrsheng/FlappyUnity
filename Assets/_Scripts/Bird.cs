using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird: MonoBehaviour
{

    [SerializeField] private float _flapForce = 10f;
    [SerializeField] private float _rotation = 1.5f;
    [SerializeField] private float _maxHeight = 4f;
    [SerializeField] private TextMeshPro _scoreText;

    private Animator _animator;
    private Rigidbody2D _rb;
    private int _score= 0;

    private bool isInputAllowed = true; // flag to control whether input is allowed or not


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    private void Update()
    {
         if (!isInputAllowed)
            return; // If input is not allowed, do nothing
        
        if ( Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) // 0 = left mouse
        {
            Flap();   
        }

        _rb.MoveRotation(_rb.linearVelocityY * _rotation); // point nose downward/upward

    }

    private void Flap()
    {
        if (transform.position.y >= _maxHeight) return; // if the bird is above the max height, do not allow it to flap
        _rb.linearVelocityY = 0f;
        _rb.AddForce(Vector2.up * _flapForce, ForceMode2D.Impulse); // add an instant upward force to the bird
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        _animator.Play("Bird_Hit"); // play the hit animation
       Time.timeScale = 0.1f; // stop the game
       isInputAllowed = false; // disable input
       Invoke(nameof(ReloadIntro), 0.1f); // reload the scene after 0.2 seconds


    }

    private void ReloadIntro()
    {
        Time.timeScale = 1f; // resume the game
        isInputAllowed = true; // re-enable input
        SceneManager.LoadScene("GameOverScene"); // load the game over scene
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       _score++;
       _scoreText.text = _score.ToString(); // update the score text
    }
}

