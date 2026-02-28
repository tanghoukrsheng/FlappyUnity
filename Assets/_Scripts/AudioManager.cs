using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _SFXSource;
    public AudioClip background;
    public AudioClip hit;
    public AudioClip flap;

    [Serializable]
    public struct SceneBgm { public string sceneName; public AudioClip clip; } // struct to hold scene-specific bgm clips
    public SceneBgm[] bgms; // array of scene-specific bgm clips

    public static AudioManager Instance { get; private set; }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persist across scenes
        } else {
            Destroy(gameObject);
            return;
        }

        if (_musicSource == null) _musicSource = GetComponent<AudioSource>(); // try to find AudioSource on the same GameObject if not assigned
        SceneManager.sceneLoaded += OnSceneLoaded; // subscribe to sceneLoaded event to change bgm when scenes change
    }

    private void Start()
    {
        // play BGM for the currently active scene (or fallback to `background`)
        PlayBgmForScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // unsubscribe from event to avoid potential issues if this object is destroyed during scene transitions (though it shouldn't be since it's a singleton)
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBgmForScene(scene.name);
    }

    private void PlayBgmForScene(string sceneName)
    {
        // find clip for this scene
        AudioClip clip = null;
        if (bgms != null && bgms.Length > 0) // only search if we have any bgm clips defined
        {
            for (int i = 0; i < bgms.Length; i++) if (bgms[i].sceneName == sceneName) { clip = bgms[i].clip; break; } // search for a matching scene name and get its clip
        }

        //if (_musicSource.clip == clip && _musicSource.isPlaying) return; // if we're already playing the correct clip, do nothing
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        _SFXSource.PlayOneShot(clip);
    }

    // Update is not needed for this manager
}
