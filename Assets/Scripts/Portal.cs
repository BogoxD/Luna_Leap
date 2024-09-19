using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {
    private CollectorManager collectorManager;

    private void Start() {
        collectorManager = FindObjectOfType<CollectorManager>();

        if (collectorManager == null) {
            Debug.LogError("CollectorManager not found in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Log to check if the trigger is detected
        Debug.Log($"Triggered with {other.gameObject.name}");

        // Check if the player collided with the portal and the portal is active
        if (collectorManager != null && collectorManager.IsPortalActive() && other.CompareTag("Player")) {
            Debug.Log("Player collided with portal. Loading next level...");
            SaveProgressAndLoadNextLevel();
        }
    }

    void SaveProgressAndLoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Save progress for the next level
        int nextLevelToUnlock = currentSceneIndex + 1;

        // If the next level exists, save it as unlocked
        if (nextLevelToUnlock < SceneManager.sceneCountInBuildSettings) {
            SaveManager.SaveProgress(nextLevelToUnlock); // Save the next level as unlocked
            Debug.Log("Saved progress for level: " + nextLevelToUnlock);
        }

        // Load the next level if available
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings) {
            // Reset progress for the next level
            CollectorManager.progressAmount = 0;

            // Load the next level
            SceneManager.LoadScene(currentSceneIndex + 1);
        } else {
            Debug.Log("Completed it mate");
        }
    }
}