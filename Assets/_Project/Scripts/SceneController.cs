using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : Singleton<SceneController>
{

    public AudioSource gameMusic;
    public AudioClip menu;
    public AudioClip game;

    protected override void Awake(){
        IsPersistentBetweenScenes = true;
        base.Awake();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        gameMusic = GetComponent<AudioSource>();
    }

    public void ChangeScene(string scene) {
        if (gameMusic.clip.name != "menu music") {
            Debug.Log("eita");
            gameMusic.clip = menu;
            gameMusic.Play();
        }
        SceneManager.LoadScene(scene);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "Game") {
            Destroy(this);
        }
    }
}
