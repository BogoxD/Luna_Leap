using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    public float rangeShake;
    public float timeSmooth;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        Shake();
    }

    private void Shake()
    {
        transform.position = Vector3.Lerp(transform.position, Vector2.one * Random.Range(-rangeShake, -rangeShake), timeSmooth * Time.deltaTime);
    }
}
