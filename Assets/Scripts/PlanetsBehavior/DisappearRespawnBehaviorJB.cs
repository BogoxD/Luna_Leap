using UnityEngine;

[CreateAssetMenu(menuName = "PlanetBehavior/DisappearRespawn")]
public class DisappearRespawnBehaviorJB : PlanetBehaviorJB {
    public float respawnTime = 3f;
    private bool isVisible = true;
    private float respawnTimer;

    public override void ApplyBehavior(Transform planetTransform) {
        if (!isVisible) {
            respawnTimer -= Time.deltaTime;
            if (respawnTimer <= 0f) {
                planetTransform.gameObject.SetActive(true);
                isVisible = true;
            }
        }
    }

    public void OnPlanetTouched(Transform planetTransform) {
        if (isVisible) {
            planetTransform.gameObject.SetActive(false);
            respawnTimer = respawnTime;
            isVisible = false;
        }
    }
}