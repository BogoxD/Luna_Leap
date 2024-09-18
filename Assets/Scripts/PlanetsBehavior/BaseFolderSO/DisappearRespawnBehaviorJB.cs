using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "PlanetBehavior/DisappearRespawn")]
public class DisappearRespawnBehaviorJB : PlanetBehaviorJB {
    public float countdownTime = 3f;          // Time before the planet disappears after being touched
    public float respawnTime = 5f;            // Time to respawn after disappear
    public GameObject planetFallParticleSystemPrefab;

    // Apply behavior every frame
    public override void ApplyBehavior(Transform planetTransform) {
    }

    // This method will be called when the planet is touched by the player
    public void OnPlanetTouched(PlanetControllerJB planetController) {
        // Get the planet's specific state from its PlanetState component
        PlanetStateJB planetState = planetController.GetComponent<PlanetStateJB>();

        if (planetState != null && !planetState.isProcessing) {
            // Start the coroutine using the PlanetControllerJB
            planetController.StartCoroutine(DisappearAndRespawn(planetController.transform, planetState));
        }
    }

    //Disapear And ReAppear
    private IEnumerator DisappearAndRespawn(Transform planetTransform, PlanetStateJB planetState) {
        planetState.isProcessing = true;  // Mark this planet as processing to prevent multiple touches
        Debug.Log("Planet touched, starting countdown");

        // Instantiate the PlanetFallParticleSystem at the planet's position and make it a child of the planet
        GameObject particleSystemInstance = Instantiate(planetFallParticleSystemPrefab, planetTransform.position, Quaternion.identity, planetTransform);

        // Adjust the scale of the particle system to match the planet's scale
        particleSystemInstance.transform.localScale = planetTransform.localScale * 0.75f;

        ParticleSystem particleSystem = particleSystemInstance.GetComponent<ParticleSystem>();
        particleSystem.Play();

        // Wait for the countdown before "disappearing"
        yield return new WaitForSeconds(countdownTime);

        particleSystem.Stop();
        Destroy(particleSystemInstance);



        // Hide the planet by disabling its Renderer and/or Collider
        Renderer planetRenderer = planetTransform.GetComponent<Renderer>();
        Collider2D planetCollider = planetTransform.GetComponent<Collider2D>();
        if (planetRenderer != null) planetRenderer.enabled = false;  // Make planet invisible
        if (planetCollider != null) planetCollider.enabled = false;  // Disable interaction
        Debug.Log("Planet has disappeared, waiting to respawn");

        // Wait for the respawn time
        yield return new WaitForSeconds(respawnTime);

        // "Respawn" the planet by restoring its Renderer and Collider
        planetTransform.position = planetState.originalPosition;  // Reset to its specific original position
        if (planetRenderer != null) planetRenderer.enabled = true;   // Make planet visible again
        if (planetCollider != null) planetCollider.enabled = true;   // Enable interaction
        Debug.Log("Planet has respawned and is visible again");

        planetState.isProcessing = false;  // Reset processing flag after the planet has fully respawned
    }
}