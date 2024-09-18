using UnityEngine;

[CreateAssetMenu(menuName = "PlanetBehavior/Rotate")]
public class RotateBehaviorJB : PlanetBehaviorJB {
    public float rotationSpeed = 50f;

    public override void ApplyBehavior(Transform planetTransform) {
        planetTransform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}