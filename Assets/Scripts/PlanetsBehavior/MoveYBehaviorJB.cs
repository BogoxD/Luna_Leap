using UnityEngine;

[CreateAssetMenu(menuName = "PlanetBehavior/MoveY")]
public class MoveYBehaviorJB : PlanetBehaviorJB {
    public float speed = 2f;
    public float minY = -3f;  // The minimum Y position
    public float maxY = 3f;   // The maximum Y position

    private bool movingUp = true;  // Direction flag

    public override void ApplyBehavior(Transform planetTransform) {
        // Create a movement direction
        Vector3 moveDirection;

        // Check if the planet is moving up
        if (movingUp) {
            moveDirection = Vector3.up;
        } else {
            // Move down
            moveDirection = Vector3.down;
        }

        // Apply movement in world space, unaffected by the object's rotation
        planetTransform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        // If the planet reaches the max limit, reverse the direction
        if (planetTransform.position.y >= maxY) {
            movingUp = false;
        }
        // If the planet reaches the min limit, reverse the direction
        else if (planetTransform.position.y <= minY) {
            movingUp = true;
        }
    }
}