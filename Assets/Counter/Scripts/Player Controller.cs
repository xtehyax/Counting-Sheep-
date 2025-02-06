using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables
    private Rigidbody playerRb;
    public float speed;

    public Vector2 turn;
    public float sensitivity = 5;

    //Animation
    [SerializeField] Animator collieAnim;
    [SerializeField] float currentSpeed;
    [SerializeField] Vector3 deltaMove;

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
            return;
        }

        deltaMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * speed * Time.deltaTime;
        playerRb.AddRelativeForce(deltaMove);

        //Rotation based on Mouse Input
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);

        //Running Animation
        currentSpeed = playerRb.velocity.magnitude;

        collieAnim.SetFloat("Speed_f", currentSpeed/5);

    }
}
