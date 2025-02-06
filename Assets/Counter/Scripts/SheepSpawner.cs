using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    // Variables
    public GameObject sheepPrefab;
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
    }

    public void SpawnSheepWave(int roundNumber)
    {
        gameManager.sheepHerded = 0;
        for (int i = 0; i < roundNumber; i++)
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

    public void DestroyAllSheep()
    {
        GameObject[] sheepArray = GameObject.FindGameObjectsWithTag("Sheep");
        foreach (GameObject sheep in sheepArray)
        {
            Destroy(sheep);
        }
    }
}
