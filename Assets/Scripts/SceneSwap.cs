using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
    // Reference to the respawn canvas UI
    public GameObject respawnCanvas;

    // This method loads a scene based on the scene name
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Call this method to show the respawn UI
    public void ShowRespawnUI()
    {
        if (respawnCanvas != null)
        {
            respawnCanvas.SetActive(true);
        }
    }

    // Trigger detection for colliding with the respawn trigger object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowRespawnUI();
        }
    }
}
