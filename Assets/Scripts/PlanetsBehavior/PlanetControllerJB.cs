using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetControllerJB : MonoBehaviour {
    public PlanetBehaviorJB[] behaviors;  // Array of different behaviors (including DisappearRespawnBehaviorJB)

    void Update() {
        // Apply each behavior in the array to this planet's transform
        foreach (var behavior in behaviors) {
            behavior.ApplyBehavior(transform);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Check if the object colliding has the tag "Player"
        if (collision.gameObject.CompareTag("Player")) {
            // Loop through behaviors and find any that are DisappearRespawnBehaviorJB
            foreach (var behavior in behaviors) {
                if (behavior is DisappearRespawnBehaviorJB disappearRespawn) {
                    // Call OnPlanetTouched for the disappearing and respawning behavior
                    disappearRespawn.OnPlanetTouched(this); // Pass in this PlanetControllerJB object
                }
            }
        }
    }
}