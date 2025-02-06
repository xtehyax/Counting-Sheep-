using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables
    private Rigidbody playerRb;
    public float speed;

    //public Vector2 turn;
    //public float sensitivity = 5;
    [SerializeField] float turnSpeed = 5;

    //Animation
    [SerializeField] Animator collieAnim;
    [SerializeField] float currentSpeed;
    [SerializeField] Vector3 deltaMove;
    [SerializeField] CameraFollow focalPoint;

    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGameActive)
        {
            //When isGameActive is false, the player should trigger sit animation and stop moving
            collieAnim.SetBool("Sit_b", true);
            return;
        }

        collieAnim.SetBool("Sit_b", false);

        deltaMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * speed * Time.deltaTime;
        Vector3 relativeMove = focalPoint.transform.TransformDirection(deltaMove);
        playerRb.AddForce(relativeMove);

        ////Rotation based on Mouse Input
        //turn.x += Input.GetAxis("Mouse X") * sensitivity;
        //transform.localRotation = Quaternion.Euler(0, turn.x, 0);

        //Running Animation
        currentSpeed = playerRb.velocity.magnitude;

        collieAnim.SetFloat("Speed_f", currentSpeed / 5);

        //Dog faces direction of movement
        if (playerRb.velocity != Vector3.zero)
        {
            var velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
            Quaternion targetRotation = Quaternion.LookRotation(velocity);
            playerRb.rotation = Quaternion.Slerp(playerRb.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }

    }

}
