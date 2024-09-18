using UnityEngine;

[CreateAssetMenu(menuName = "PlanetBehavior/MoveX")]
public class MoveXBehaviorJB : PlanetBehaviorJB {
    public float speed = 2f;
    public float minX = -5f;  // The minimum X position
    public float maxX = 5f;   // The maximum X position

    private bool movingRight = true;  // Direction flag

    public override void ApplyBehavior(Transform planetTransform) {
        Vector3 moveDirection;

        // Check if the planet is moving to the right
        if (movingRight) {
            moveDirection = Vector3.right;
        } else {
            // Move to the left
            moveDirection = Vector3.left;
        }

        // Apply movement in world space, unaffected by the object's rotation
        planetTransform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        // If the planet reaches the max limit, reverse the direction
        if (planetTransform.position.x >= maxX) {
            movingRight = false;
        }
        // If the planet reaches the min limit, reverse the direction
        else if (planetTransform.position.x <= minX) {
            movingRight = true;
        }
    }
}