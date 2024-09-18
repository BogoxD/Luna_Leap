using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {
    private CollectorManager collectorManager;

    private void Start() {
        // Find the CollectorManager in the scene
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
            LoadNextLevel();
        }
    }

    void LoadNextLevel() {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Check if there's another level to load
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings) {
            // Reset progress for the next level (if needed)
            CollectorManager.progressAmount = 0;

            // Load the next scene
            SceneManager.LoadScene(currentSceneIndex + 1);
        } else {
            Debug.Log("No more levels to load.");
        }
    }
}