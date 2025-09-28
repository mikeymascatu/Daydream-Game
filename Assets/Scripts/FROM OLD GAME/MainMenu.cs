using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Scene To Load")]
    [SerializeField] string gameSceneName = "Game"; // set this to your gameplay scene name

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // works in Editor
#else
        Application.Quit(); // works in a build
#endif
    }
}