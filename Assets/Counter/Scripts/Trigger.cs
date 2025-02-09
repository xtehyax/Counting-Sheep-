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
    //Sheep Enter Pen
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sheep"))
        {
            gameManager.AddSheepHerded();
            gameManager.totalSheepHerded++;
            gameManager.sheepInPen.Add(other.gameObject); // Add sheep to the list
            DestroySheep(other.gameObject);
        }
    }

    // Sheep leaves pen
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Sheep"))
        {
            gameManager.sheepInPen.Remove(other.gameObject); // Remove sheep from the list
            gameManager.sheepHerded--;
        }
    }

    //Managing pen numbers - if more than 5 sheep in the pen destroy them
    private void DestroySheep(GameObject thisSheep)
    {
        if (gameManager.sheepInPen.Count >= 5)
        {
            Destroy(thisSheep);
            gameManager.sheepInPen.Clear();
        }
    }

}