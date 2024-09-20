using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenSpawnManager : MonoBehaviour
{
    public GameObject[] startscreenPrefabs;
    public int startscreenIndex;
    private float startDelay = 1f;
    private float spawnInterval = 2f;
    public float spawnRangeY = 5f;
    public float spawnX = -10f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomItem", startDelay, spawnInterval);
    }

   void SpawnRandomItem()
    {
        int startscreenIndex = Random.Range(0, startscreenPrefabs.Length);

        float spawnPosY = Random.Range(-spawnRangeY, spawnRangeY);
        

        Instantiate(startscreenPrefabs[startscreenIndex], new Vector2(spawnX, spawnPosY), startscreenPrefabs[startscreenIndex].transform.rotation);
    }
}
