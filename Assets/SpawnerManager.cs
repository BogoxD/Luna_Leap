using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [Header("Spawner")]
    public GameObject[] spawnPrefabs;

    public float SpawnDelay;
    public int ObjectsPerWave;
    public int Waves;

    [Header("Positions")]
    public float rangeXpos;
    public float rangeYpos;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        //number of wave
        for (int i = 0; i < Waves; i++)
        {
            //Objects Per Wave
            for (int j = 0; j < ObjectsPerWave; j++)
            {
                int randIndex = Random.Range(0, spawnPrefabs.Length);


                GameObject spawnedObject = Instantiate(
                    spawnPrefabs[randIndex], 
                    transform.position + new Vector3(rangeXpos, rangeYpos), 
                    Quaternion.identity);

            }
            //spawn delay between waves
            yield return new WaitForSeconds(SpawnDelay);

        }
    }
}
