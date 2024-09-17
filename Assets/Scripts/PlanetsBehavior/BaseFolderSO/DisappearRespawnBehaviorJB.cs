using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "PlanetBehavior/DisappearRespawn")]
public class DisappearRespawnBehaviorJB : PlanetBehaviorJB {
    public float countdownTime = 3f;          // Time before the planet disappears after being touched
    public float respawnTime = 5f;            // Time to respawn after disappearance
    private bool isProcessing = false;        // Track if the planet is currently in a respawn cycle
    private Vector3 originalPosition;         // To store the original position of the planet
    private bool positionStored = false;      // Track whether the original position has been stored

    // Apply behavior every frame (used in PlanetControllerJB)
    public override void ApplyBehavior(Transform planetTransform) {
        // Store original position if not already stored
        if (!positionStored) {
            originalPosition = planetTransform.position;
            positionStored = true;
        }
    }

    // This method will be called when the planet is touched by the player
    public void OnPlanetTouched(Transform planetTransform) {
        // Only start the coroutine if the planet is not already in the middle of a respawn cycle
        if (!isProcessing) {
            planetTransform.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(DisappearAndRespawn(planetTransform));
        }
    }

    // Coroutine to handle disappearance and respawn
    private IEnumerator DisappearAndRespawn(Transform planetTransform) {
        isProcessing = true;  // Mark as processing to prevent multiple touches
        Debug.Log("Planet touched, starting countdown");

        // Wait for the countdown before "disappearing"
        yield return new WaitForSeconds(countdownTime);

        // Hide the planet by disabling its Renderer and/or Collider
        Renderer planetRenderer = planetTransform.GetComponent<Renderer>();
        Collider2D planetCollider = planetTransform.GetComponent<Collider2D>();
        if (planetRenderer != null) planetRenderer.enabled = false;  // Make planet invisible
        if (planetCollider != null) planetCollider.enabled = false;  // Disable interaction
        Debug.Log("Planet has disappeared, waiting to respawn");

        // Wait for the respawn time
        yield return new WaitForSeconds(respawnTime);

        // "Respawn" the planet by restoring its Renderer and Collider
        planetTransform.position = originalPosition;  // Reset position
        if (planetRenderer != null) planetRenderer.enabled = true;   // Make planet visible again
        if (planetCollider != null) planetCollider.enabled = true;   // Enable interaction
        Debug.Log("Planet has respawned and is visible again");

        isProcessing = false;  // Reset processing flag after the planet has fully respawned
    }
}