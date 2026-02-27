using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public static GameOverController Instance { get; private set; }


  
    [SerializeField] private Button continueButton;

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Make sure button is linked
        continueButton.onClick.AddListener(OnContinue);


    }

    public void OnContinue()
    {
        Time.timeScale = 1f; // resume
        // load the Intro scene
        SceneManager.LoadScene("IntroScene");
    }
}
