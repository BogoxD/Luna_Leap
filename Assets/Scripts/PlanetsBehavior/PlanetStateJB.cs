using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Only needs to be added if its a disappearing Planet
public class PlanetStateJB : MonoBehaviour {
    [HideInInspector]
    public Vector3 originalPosition;   // original position
    [HideInInspector]
    public bool isProcessing = false;  //is the planet is in the middle of a respawn cycle

    private void Awake() {
        //Store the original position
        originalPosition = transform.position;
    }
}