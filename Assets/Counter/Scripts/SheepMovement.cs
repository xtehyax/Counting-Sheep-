using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepMovement : MonoBehaviour
{
    //Variable
    Rigidbody sheepRb;
    GameObject collie;

    public float speed = 5;
    public float fleeDistance = 5;
    public float attractDistance = 10;
    public float groupedDistance = 1;

    // Start is called before the first frame update
    void Start()
    {
        sheepRb = GetComponent<Rigidbody>();
        collie = GameObject.Find("Collie Player");

    }

    // Update is called once per frame
    void Update()
    {
        //Calculate look direction
        Vector3 lookDirection = (collie.transform.position - transform.position).normalized;

        //Add force to move away from the collie if too close
        if (Vector3.Distance(collie.transform.position, transform.position) < fleeDistance)
        {
            sheepRb.AddForce(-lookDirection.normalized * speed * Time.deltaTime);
        }
        else
        {
            //Move towards other sheep
            MoveToOtherSheep();
        }

    }

    //Find objects with tag sheep
    private void MoveToOtherSheep()
    {
        Vector3 averageSheepPosition = Vector3.zero;
        int sheepCount = 0;

        foreach (GameObject sheep in GameObject.FindGameObjectsWithTag("Sheep"))
        {
            if (sheep != this.gameObject && Vector3.Distance(transform.position,sheep.transform.position) < attractDistance) //Don't include self in average position & only add sheep within attractDistance
            {
                averageSheepPosition += sheep.transform.position; 
                sheepCount++;
            }
        }

        if (sheepCount > 0)
        {
            averageSheepPosition /= sheepCount;
            if (Vector3.Distance(transform.position, averageSheepPosition) >= groupedDistance)
            {
                Vector3 moveDirection = (averageSheepPosition - transform.position).normalized;
                sheepRb.AddForce(moveDirection * speed * Time.deltaTime);
            }

        }
    }
}
