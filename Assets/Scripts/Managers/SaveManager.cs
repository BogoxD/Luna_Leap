using UnityEngine;
using UnityEngine.UI;

public static class SaveManager {
    // Save the player's progress (the highest completed level)
    public static void SaveProgress(int completedLevel) {
        int highestLevel = PlayerPrefs.GetInt("HighestLevel", 1); // Default is 1 (only Level 1 available)

        if (completedLevel > highestLevel) {
            PlayerPrefs.SetInt("HighestLevel", completedLevel);
            PlayerPrefs.Save(); // Ensure the data is saved immediately
        }
    }

    // Load the highest level the player has got to
    public static int LoadProgress() {
        return PlayerPrefs.GetInt("HighestLevel", 1); // Default is Level 1 unlocked
    }

    // Update the level buttons to reflect the player's progress
    public static void UpdateLevelButtons() {
        int highestLevel = LoadProgress();
        int totalLevels = 2; // Update this to the total number of levels

        for (int i = 1; i <= totalLevels; i++) {
            // Names to match the actual buttons
            GameObject button = GameObject.Find("Level " + i);

            if (button != null) {
                Button btnComponent = button.GetComponent<Button>();
                if (i <= highestLevel) {
                    // Make the button interactable if the level is unlocked
                    btnComponent.interactable = true;
                    Debug.Log($"Button {i} is interactable");
                } else {
                    // Otherwise, disable interaction
                    btnComponent.interactable = false;
                    Debug.Log($"Button {i} is not interactable");
                }
            }
        }
    }

    // Optional: Clear saved progress
    public static void ClearProgress() {
        PlayerPrefs.DeleteKey("HighestLevel");
        UpdateLevelButtons();
    }
}