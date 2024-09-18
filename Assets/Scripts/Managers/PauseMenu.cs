using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenuPanel; // Reference to your pause menu panel prefab

    private bool isPaused = false;

    void Update() {
        // Listen for the Escape key to toggle the pause menu
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    // Method to resume the game
    public void ResumeGame() {
        pauseMenuPanel.SetActive(false); // Hide pause menu
        Time.timeScale = 1f; // Resume game time
        isPaused = false; // Set pause state to false
    }

    // Method to pause the game
    public void PauseGame() {
        pauseMenuPanel.SetActive(true); // Show pause menu
        Time.timeScale = 0f; // Pause game time
        isPaused = true; // Set pause state to true
    }

    public void sceneChange(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }
}