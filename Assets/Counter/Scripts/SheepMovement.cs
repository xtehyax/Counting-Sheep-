using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SheepMovement : MonoBehaviour
{
    // Variables
    Rigidbody sheepRb;
    GameObject collie;
    [SerializeField] Animator sheepAnim;
    private float currentSpeed;

    public float speed;
    public float sheepAttractSpeed;
    [SerializeField] float turnSpeed;
    public float fleeDistance = 5;
    public float attractDistance = 10;
    public float groupedDistance = 1;

    //Audio
    private AudioSource sheepAudio;

    // Start is called before the first frame update
    void Start()
    {
        sheepRb = GetComponent<Rigidbody>();
        collie = GameObject.Find("Collie Player");
        sheepAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate look direction
        Vector3 lookDirection = (collie.transform.position - transform.position).normalized;

        // Add force to move away from the collie if too close
        if (Vector3.Distance(collie.transform.position, transform.position) < fleeDistance)
        {
            sheepRb.AddForce(-lookDirection * speed * Time.deltaTime);
            if (sheepAudio.isPlaying == false)
                {
                    sheepAudio.Play();
                }
        }
        else
        {
            // Move towards other sheep
            MoveToOtherSheep();
        }

        // Set rotation to face the direction of movement
        if (sheepRb.velocity != Vector3.zero)
        {
            var velocity = new Vector3(sheepRb.velocity.x, 0, sheepRb.velocity.z);
            Quaternion targetRotation = Quaternion.LookRotation(velocity); 
            sheepRb.rotation = Quaternion.Slerp(sheepRb.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }

        //Running Animation
        currentSpeed = sheepRb.velocity.magnitude;

        sheepAnim.SetFloat("Speed_f", currentSpeed / 5);
    }

    // Find objects with tag sheep
    private void MoveToOtherSheep()
    {
        Vector3 averageSheepPosition = Vector3.zero;
        int sheepCount = 0;

        foreach (GameObject sheep in GameObject.FindGameObjectsWithTag("Sheep"))
        {
            if (sheep != this.gameObject && Vector3.Distance(transform.position, sheep.transform.position) < attractDistance) // Don't include self in average position & only add sheep within attractDistance
            {
                averageSheepPosition += sheep.transform.position;
                sheepCount++;
            }
        }

        //Sheep group together
        if (sheepCount > 0)
        {
            averageSheepPosition /= sheepCount;
            if (Vector3.Distance(transform.position, averageSheepPosition) >= groupedDistance)
            {
                Vector3 moveDirection = (averageSheepPosition - transform.position).normalized;
                sheepRb.AddForce(moveDirection * sheepAttractSpeed * Time.deltaTime);
            }
        }
    }
}

