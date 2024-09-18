using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenuPanel;

    private bool isPaused = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    public void ResumeGame() {
        pauseMenuPanel.SetActive(false); // Hide pause menu
        Time.timeScale = 1f; // Resume game time
        isPaused = false; // Set pause state to false
    }

    public void PauseGame() {
        pauseMenuPanel.SetActive(true); // Show pause menu
        Time.timeScale = 0f; // Pause game time
        isPaused = true; // Set pause state to true
    }

    public void sceneChange(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }
}