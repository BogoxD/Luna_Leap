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



/*using UnityEngine;

[CreateAssetMenu(menuName = "PlanetBehavior/DisappearRespawn")]
public class DisappearRespawnBehaviorJB : PlanetBehaviorJB {
    public float countdownTime = 3f;          // Time before the planet disappears after being touched
    public float respawnTime = 5f;            // Time to respawn after disappearance
    private bool isVisible = true;            // Tracks visibility state
    private bool countdownActive = false;     // Tracks if the countdown has started
    private float countdownTimer;             // Timer for the countdown before disappearing
    private float respawnTimer;               // Timer for respawn
    private Vector3 originalPosition;         // To store the original position of the planet
    private bool positionStored = false;      // Track whether the original position has been stored

    // Apply behavior every frame (used in PlanetControllerJB)
    public override void ApplyBehavior(Transform planetTransform) {
        // Store original position if not already stored
        if (!positionStored) {
            originalPosition = planetTransform.position;
            positionStored = true;
        }

        // Handle the countdown for disappearing
        if (countdownActive) {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer <= 0f) {
                // After countdown, make the planet invisible
                MakeInvisible(planetTransform);
                countdownActive = false;
            }
        }

        // Handle the respawn countdown when the planet is invisible
        if (!isVisible) {
            respawnTimer -= Time.deltaTime;
            if (respawnTimer <= 0f) {
                Respawn(planetTransform);
            }
        }
    }

    // This method will be called when the planet is touched by the player
    public void OnPlanetTouched(Transform planetTransform) {
        // Start countdown if the planet is visible and the countdown hasn't started yet
        if (isVisible && !countdownActive) {
            countdownTimer = countdownTime;  // Start the 3-second countdown
            countdownActive = true;          // Mark the countdown as active
            Debug.Log("Planet touched, starting countdown");
        }
    }

    // Makes the planet invisible
    private void MakeInvisible(Transform planetTransform) {
        planetTransform.gameObject.SetActive(false);  // Hide the planet
        isVisible = false;  // Mark the planet as invisible
        respawnTimer = respawnTime;  // Start the respawn timer
        Debug.Log("Planet is now invisible, respawn timer started");
    }

    // Respawn the planet at its original position after a delay
    private void Respawn(Transform planetTransform) {
        planetTransform.position = originalPosition;  // Reset to original position
        planetTransform.gameObject.SetActive(true);   // Make the planet visible again
        isVisible = true;                             // Mark the planet as visible
        countdownActive = false;                      // Reset countdown state
        Debug.Log("Planet has respawned");
    }
}*/