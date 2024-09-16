using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlanetControllerJB : MonoBehaviour {
    public PlanetBehaviorJB[] behaviors;

    void Update() {
        foreach (var behavior in behaviors) {
            behavior.ApplyBehavior(transform);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        foreach (var behavior in behaviors) {
            if (behavior is DisappearRespawnBehaviorJB disappearRespawn) {
                disappearRespawn.OnPlanetTouched(transform);
            }
        }
    }
}