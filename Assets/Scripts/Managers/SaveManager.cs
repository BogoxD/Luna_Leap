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

    // Update the level buttons to match the player's progress
    public static void UpdateLevelButtons() {
        int highestLevel = LoadProgress(); // Load the highest completed level
        int totalLevels = 4; // Update this to the total number of levels you have

        for (int i = 1; i <= totalLevels; i++) {
            // Find the level button by its name
            GameObject button = GameObject.Find("Level " + i);

            if (button != null) {
                Button btnComponent = button.GetComponent<Button>();

                // Make buttons interactable if they correspond to levels less than or equal to the highest completed
                if (i <= highestLevel) {
                    btnComponent.interactable = true;
                    Debug.Log($"Button {i} is interactable");
                } else {
                    btnComponent.interactable = false;
                    Debug.Log($"Button {i} is not interactable");
                }
            }
        }
    }


    //Clear saved progress
    public static void ClearProgress() {
        PlayerPrefs.DeleteKey("HighestLevel");
        UpdateLevelButtons();
    }
}