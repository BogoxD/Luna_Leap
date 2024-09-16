using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lenght, startPosX, startPosY;
    public GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));

        float distX = (cam.transform.position.x * parallaxEffect);
        float distY = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);

        if (temp > startPosX + lenght) startPosX += lenght;
        else if (temp < startPosX - lenght) startPosX -= lenght;
    }
}
