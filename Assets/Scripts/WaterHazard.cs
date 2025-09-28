using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class WaterHazard : MonoBehaviour
{
    [Header("What to do on death")]
    [SerializeField] bool reloadCurrentScene = true;
    [SerializeField] string gameOverSceneName = ""; // set if you want a separate GameOver scene

    void Reset()
    {
        // Make the tilemap collider act as a trigger by default
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    // Works if the water collider is a Trigger
    void OnTriggerEnter2D(Collider2D other) { TryKill(other.gameObject); }

    // Works if the water collider is NOT a Trigger
    void OnCollisionEnter2D(Collision2D col) { TryKill(col.gameObject); }

    void TryKill(GameObject obj)
    {
        if (!obj.CompareTag("Player")) return; // tag your player "Player"

        if (!string.IsNullOrEmpty(gameOverSceneName))
            SceneManager.LoadScene(gameOverSceneName);
        else if (reloadCurrentScene)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else
            Application.Quit(); // optional fallback
    }
}