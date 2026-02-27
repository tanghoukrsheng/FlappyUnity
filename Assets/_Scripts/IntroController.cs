using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
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
