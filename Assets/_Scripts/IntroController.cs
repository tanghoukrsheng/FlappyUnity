using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public float delayBeforeStart = 3f; // Delay in seconds before input is allowed
    private float timer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Increment timer

        if (timer < delayBeforeStart) return; // Wait until the delay is over

        // After delay, allow input detection
        if (Input.anyKeyDown)
        {
            LoadMainGameScene();
        }
    }

     private void LoadMainGameScene()
    {
        SceneManager.LoadScene("Play"); // Load the main game scene
    }
}
