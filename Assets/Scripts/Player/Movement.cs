using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float sprintSpeed = 10.0f;
    public float crouchSpeed = 2.0f;

    private CharacterController characterController;
    private Crouch crouchScript;
    private float playerSpeed;
    private Vector3 playerVelocity;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        crouchScript = GetComponent<Crouch>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Apply gravity
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        // Calculate player speed based on crouch state, if crouching player should move slower
        playerSpeed = crouchScript.isCrouching ? crouchSpeed : walkSpeed;//movement manager

        // Player movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Sprinting
        if (Input.GetKey(KeyCode.LeftShift) && !crouchScript.isCrouching) //cant sprint if crouched, calculate player speed before applying move
        {
            playerSpeed = sprintSpeed;
        }

        characterController.Move(Time.deltaTime * playerSpeed * move); //apply movement
    }
}
