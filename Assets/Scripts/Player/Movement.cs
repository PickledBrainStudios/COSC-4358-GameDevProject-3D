using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float sprintSpeed = 10.0f;
    public float crouchSpeed = 2.0f;

    private float moveX;
    private float moveZ;
    private Vector3 move;

    private CharacterController characterController;
    private Crouch crouchScript;
    private float playerSpeed;
    private Vector3 playerVelocity;

    public AudioSource audioSource;
    public AudioClip[] soundClips;
    public AudioClip[] forestClips;
    public float footstepTimer = 1.0f;
    private float timer;
    private int i;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        crouchScript = GetComponent<Crouch>();
        characterController = GetComponent<CharacterController>();
        timer = footstepTimer;
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
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
        move = transform.right * moveX + transform.forward * moveZ;
        move = move.normalized; //normalize so we only have the direction the player wants to move in relative to the camera,
                                //this ensures that we don't add up vectors and have magnitues larger than 1 when we press horizontal and vertical movemen
        // Sprinting
        if (Input.GetKey(KeyCode.LeftShift) && !crouchScript.isCrouching) //cant sprint if crouched, calculate player speed before applying move
        {
            playerSpeed = sprintSpeed;
        }

        // Handling footsteps
        if ((moveX != 0 || moveZ != 0) && characterController.isGrounded)
        {
            audioSource.volume = .5f;
            timer -= Time.deltaTime;
            //i = Random.Range(0, soundClips.Length);
            i = Random.Range(0, forestClips.Length);

            if (timer <= 0 && playerSpeed == walkSpeed)
            {
                //audioSource.PlayOneShot(soundClips[i]);
                audioSource.PlayOneShot(forestClips[i]);
                timer = footstepTimer;
            }
            else if (timer <= 0 && playerSpeed == sprintSpeed)
            {
                audioSource.volume = audioSource.volume * 1.3f;
                //audioSource.PlayOneShot(soundClips[i]);
                audioSource.PlayOneShot(forestClips[i]);
                timer = footstepTimer * 0.6f;
            }
            else if (timer <= 0 && playerSpeed == crouchSpeed)
            {
                audioSource.volume *= 0.3f;
                //audioSource.PlayOneShot(soundClips[i]);
                audioSource.PlayOneShot(forestClips[i]);
                timer = footstepTimer * 1.5f;
            }
        }

        // Modify speed based on straffing and walking backwards
        if (Mathf.Abs(moveX) > 0)
        {
            playerSpeed *= 0.8f;
        }
        if (moveZ < 0) 
        {
            playerSpeed *= 0.6f;
        }

        //apply movement
        characterController.Move(Time.deltaTime * playerSpeed * move); 
    }
}
