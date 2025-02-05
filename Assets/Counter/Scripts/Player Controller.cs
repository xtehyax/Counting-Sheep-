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


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        deltaMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * speed * Time.deltaTime;
        playerRb.AddRelativeForce(deltaMove);

        //Rotation based on Mouse Input
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);

        //Running Animation
        float currentSpeed = playerRb.velocity.magnitude;
        if (currentSpeed < 0.5f)
        {
            collieAnim.SetFloat("Speed_f", 0);
        }
        else if (currentSpeed >= 0.5f && speed < 1f)
        {
            collieAnim.SetFloat("Speed_f", 0.5f);
        }
        else
        {
            collieAnim.SetFloat("Speed_f", 1f);
        }
    }
}
