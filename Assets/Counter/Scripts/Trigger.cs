using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenTrigger : MonoBehaviour
{
    private SheepSpawner sheepSpawner;

    // Start is called before the first frame update
    void Start()
    {
        sheepSpawner = FindObjectOfType<SheepSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sheep"))
        {
            sheepSpawner.SheepHerded();
            // remove sheep in the pen
            Destroy(other.gameObject);
        }
    }
}