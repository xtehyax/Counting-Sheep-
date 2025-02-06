using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenTrigger : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sheep"))
        {
            gameManager.AddSheepHerded();
            gameManager.totalSheepHerded++;

        }
    }
}