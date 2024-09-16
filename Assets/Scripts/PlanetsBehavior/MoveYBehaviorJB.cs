using UnityEngine;

[CreateAssetMenu(menuName = "PlanetBehavior/MoveY")]
public class MoveYBehaviorJB : PlanetBehaviorJB {
    public float speed = 2f;
    public float minY = -3f;  // The minimum Y position
    public float maxY = 3f;   // The maximum Y position

    private bool movingUp = true;  // Direction flag

    public override void ApplyBehavior(Transform planetTransform) {

        Vector3 currentPosition = planetTransform.position;

        // Check if the planet is moving up
        if (movingUp) {
            planetTransform.Translate(Vector2.up * speed * Time.deltaTime);

            // If the planet reaches the max limit, reverse the direction
            if (planetTransform.position.y >= maxY) {
                movingUp = false;
            }
        } else {
            // Move down
            planetTransform.Translate(Vector2.down * speed * Time.deltaTime);

            // If the planet reaches the min limit, reverse the direction
            if (planetTransform.position.y <= minY) {
                movingUp = true;
            }
        }
        planetTransform.position = new Vector3(planetTransform.position.x, currentPosition.y, planetTransform.position.z);
    }
}