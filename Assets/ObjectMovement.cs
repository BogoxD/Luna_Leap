using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float movementSpeed = 5f;

    void Update()
    {
        MoveRight();
    }
    private void MoveRight()
    {
        transform.Translate(transform.right * movementSpeed * Time.deltaTime);
    }
}
