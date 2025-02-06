using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    // Variables
    public GameObject sheepPrefab;
    public GameObject[] sheepPrefabs;
    public float spawnRange = 18;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        SpawnSheepWave(gameManager.roundNumber); // function call
    }

    // Update is called once per frame
    void Update()
    {
        if (Debug.isDebugBuild && Input.GetKeyDown(KeyCode.P))
        {
            Instantiate(sheepPrefab, GenerateSpawnPos(), sheepPrefab.transform.rotation);
        }
    }

    public void SpawnSheepWave(int roundNumber)
    {
        gameManager.sheepHerded = 0;
        for (int i = 0; i < roundNumber; i++)
        {
            Instantiate(SpawnRandSheep(), GenerateSpawnPos(), sheepPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    public void DestroyAllSheep()
    {
        GameObject[] sheepArray = GameObject.FindGameObjectsWithTag("Sheep");
        foreach (GameObject sheep in sheepArray)
        {
            Destroy(sheep);
        }
    }

    //Spawn random sheep from array
    GameObject SpawnRandSheep()
    {
        int animalIndex = Random.Range(0, sheepPrefabs.Length);

        return sheepPrefabs[animalIndex];

    }
}
