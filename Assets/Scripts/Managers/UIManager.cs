using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    private GameObject levelPanel;

    void Start() {
        levelPanel = GameObject.Find("LevelPanel");
        SaveManager.UpdateLevelButtons(); // Update levels
        // Deactivate the level panel after updateing
        if (levelPanel != null) {
            levelPanel.SetActive(false);
        }
    }

    public void sceneChange(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadLevel(int level) {
        SceneManager.LoadScene("Map_" + level);
    }

    public void ClearProgress_() {
        SaveManager.ClearProgress();
    }
}