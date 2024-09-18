using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollectorManager : MonoBehaviour {
    // Static progressAmount to persist between scenes
    public static int progressAmount;
    public Slider progressSlider;
    private GameObject portal;


    private bool portalActive = false;

    private void Start() {
        // Reset progress when the scene starts
        ResetProgress();

        // Subscribe to the event to increase progress
        Pickup.OnCarrotCollect += IncreaseProgressAmount;

        // Find the GameObject tagged with "Portal"
        portal = GameObject.FindGameObjectWithTag("Portal");

        // Deactivate the portal initially
        if (portal != null) {
            portal.SetActive(false);
        } else {
            Debug.LogError("Level needs a portal");
        }
    }

    // Reset progress when a new scene is loaded
    private void ResetProgress() {
        progressAmount = 0;
        progressSlider.value = 0;
    }

    // Increase progress and activate the portal when progress >= 100
    void IncreaseProgressAmount(int amount) {
        progressAmount += amount;
        progressSlider.value = progressAmount;

        if (progressAmount >= 100 && !portalActive) {
            // Activate the portal
            if (portal != null) {
                portal.SetActive(true);
                portalActive = true;
                Debug.Log("Portal Open");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Log to check if the trigger is detected
        Debug.Log($"Triggered with {other.gameObject.name}");

        // Check if the player collided with the portal and the portal is active
        if (portalActive && other.CompareTag("Player")) {
            Debug.Log("Player collided with portal. Loading next level...");
            // Load the next level
            LoadNextLevel();
        }
    }

    // Load the next scene in the build settings
    void LoadNextLevel() {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Check if there's another level to load
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings) {
            // Reset progress for the next level
            progressAmount = 0;

            // Load the next scene
            SceneManager.LoadScene(currentSceneIndex + 1);
        } else {
            Debug.Log("No more levels to load.");
        }
    }

    private void OnDestroy() {
        // Unsubscribe from the event to avoid memory leaks
        Pickup.OnCarrotCollect -= IncreaseProgressAmount;
    }
}