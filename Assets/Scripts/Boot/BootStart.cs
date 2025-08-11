using UnityEngine;
using UnityEngine.SceneManagement;

public class BootStart : MonoBehaviour
{
    [Tooltip("Scene to load after a short delay")]
    public string sceneName = "Battle";
    public float delay = 0.05f;

    void Start() { Invoke(nameof(Go), delay); }
    void Go() { SceneManager.LoadScene(sceneName); }
}