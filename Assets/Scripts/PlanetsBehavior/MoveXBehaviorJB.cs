using UnityEngine;

[CreateAssetMenu(menuName = "PlanetBehavior/MoveX")]
public class MoveXBehaviorJB : PlanetBehaviorJB {
    public float speed = 2f;
    public float minX = 0f;  // The minimum X position
    public float maxX = 5f;   // The maximum X position

    private bool movingRight = true;  // Direction flag

    public override void ApplyBehavior(Transform planetTransform) {

        //Vector3 currentPosition = planetTransform.position;

        // Check if the planet is moving to the right
        if (movingRight) {
            planetTransform.Translate(Vector2.right * speed * Time.deltaTime);

            // If the planet reaches the max limit, reverse the direction
            if (planetTransform.position.x >= maxX) {
                movingRight = false;
            }
        } else {
            // Move to the left
            planetTransform.Translate(Vector2.left * speed * Time.deltaTime);

            // when the planet reaches the min limit - reverse the direction
            if (planetTransform.position.x <= minX) {
                movingRight = true;
            }
        }
        //planetTransform.position = new Vector3(currentPosition.x, planetTransform.position.y, planetTransform.position.z);
    }
}