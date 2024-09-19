using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectorManager : MonoBehaviour {
    public static int progressAmount;
    private Slider progressSlider;
    private GameObject portal;
    private bool portalActive = false;

    [SerializeField] AudioClip portalActiveClip;

    private void Start() {
        GameObject sliderObject = GameObject.Find("CarrotSlider");
        progressSlider = sliderObject.GetComponent<Slider>();
        portal = GameObject.FindGameObjectWithTag("Portal");

        ResetProgress();

        // Subscribe to the event to increase progress
        Pickup.OnCarrotCollect += IncreaseProgressAmount;

        // Deactivate the portal initially
        if (portal != null) {
            portal.SetActive(false);
        } else {
            Debug.LogError("Level needs a portal");
        }
    }

    private void ResetProgress() {
        progressAmount = 0;
        progressSlider.value = 0;
    }

    void IncreaseProgressAmount(int amount) {
        progressAmount += amount;
        progressSlider.value = progressAmount;

        if (progressAmount >= 100 && !portalActive) {
            // Activate the portal
            if (portal != null) {
                portal.SetActive(true);
                portalActive = true;
                Debug.Log("Portal Open");
                SoundFXManager.Instance.PlaySoundFXClip(portalActiveClip, transform, 1f);
            }
        }
    }

    private void OnDestroy() {
        // Unsubscribe from the event to avoid memory leaks
        Pickup.OnCarrotCollect -= IncreaseProgressAmount;
    }

    public bool IsPortalActive() {
        return portalActive;
    }
}