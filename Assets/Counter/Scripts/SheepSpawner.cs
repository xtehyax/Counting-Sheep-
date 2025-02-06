using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    // Variables
    public GameObject sheepPrefab;
    public int roundNumber = 1;
    public float spawnRange = 18;
    public int sheepHerded;

    // Start is called before the first frame update
    void Start()
    {
        sheepHerded = 0;
        SpawnSheepWave(roundNumber); // function call
    }

    // Update is called once per frame
    void Update()
    {
        if (sheepHerded == roundNumber)
        {
            roundNumber++;
            SpawnSheepWave(roundNumber);
        }
    }

    public void SheepHerded()
    {
        sheepHerded++;
    }

    void SpawnSheepWave(int sheepToSpawn)
    {
        for (int i = 0; i < sheepToSpawn; i++)
        {
            Instantiate(sheepPrefab, GenerateSpawnPos(), sheepPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0.5f, spawnPosZ);
        return randomPos;
    }
}