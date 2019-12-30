using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12f;
    public float gravity = -9.81f;
    public float groundDistance = .4f;
    public float jumpHeight = 3f;

    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask GroundMask;

    private Vector3 velocity;
    private bool isGrounded;


    void Start() { }

    // Update is called once per frame
    void Update() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, GroundMask);

        if (isGrounded) {
            if (velocity.y < 0) {
                velocity.y = -2f;
            }

            if (Input.GetButtonDown("Jump")) {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * (speed * Time.deltaTime));

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}