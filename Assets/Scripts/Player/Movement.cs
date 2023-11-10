using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float sprintSpeed = 10.0f;
    public float crouchSpeed = 2.0f;

    public float staminaDrain = 10f;
    public float staminaRecovery = 7f;
    private float stamina = 100f;
    private bool tired = false;

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
    private int stepIndex;
    readonly float stepVolume = 0.5f;

    private Transform raySource;
    private bool hardSurface = true;

    private Vector2 moveInputValue;

    private RawImage lungs;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        crouchScript = GetComponent<Crouch>();
        characterController = GetComponent<CharacterController>();
        timer = footstepTimer;
        raySource = gameObject.transform;
        lungs = GameObject.FindGameObjectWithTag("UI_Lung").GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stamina < 50f)
        {
            lungs.color = new Color(1f, (stamina * .01f), (stamina * .01f), (1f - (stamina * .01f)));
        }
        else {
            lungs.color = new Color(1f, 1f, 1f, (1f - (stamina * .01f)));
        }
        
        //Debug.Log(stamina);
        if (stamina <= 0f) {
            tired = true;
        }
        
        
        Ray r_0 = new(raySource.position, -raySource.up);//cast ray down
        if (Physics.Raycast(r_0, out RaycastHit hitInfo_0))
        {
            if (hitInfo_0.collider.gameObject)
            {
                //Debug.DrawRay(raySource.position, -raySource.up, Color.green);
                //Debug.Log(hitInfo_0.collider.tag);
                if (hitInfo_0.collider.gameObject.CompareTag("HardSurface") || hitInfo_0.collider.gameObject.CompareTag("Untagged"))
                {
                    hardSurface = true;
                }
                else 
                {
                    hardSurface = false;
                }
            }
        }
        

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

        //Debug.Log(move);
        
        // Sprinting

        if (Input.GetKey(KeyCode.LeftShift) && !crouchScript.isCrouching && stamina > 0f && !tired) //cant sprint if crouched, calculate player speed before applying move
        {
            playerSpeed = sprintSpeed;
            stamina -= staminaDrain*Time.deltaTime;
        }
        else if (stamina  < 100f)
        {
            if (stamina >= 50f) {
                tired = false;
            }
            stamina += staminaRecovery * Time.deltaTime;
        }

        // Handling footsteps
        if ((moveX != 0 || moveZ != 0) && characterController.isGrounded)
        {
            audioSource.volume = stepVolume;
            timer -= Time.deltaTime;
            if (hardSurface) { stepIndex = Random.Range(0, soundClips.Length); }
            else { stepIndex = Random.Range(0, forestClips.Length); }

            if (timer <= 0 && playerSpeed == walkSpeed)
            {
                if (hardSurface) { audioSource.PlayOneShot(soundClips[stepIndex]); }
                else { audioSource.PlayOneShot(forestClips[stepIndex]); }
                timer = footstepTimer;
            }
            else if (timer <= 0 && playerSpeed == sprintSpeed)
            {
                audioSource.volume = stepVolume * 1.5f;
                if (hardSurface) { audioSource.PlayOneShot(soundClips[stepIndex]); }
                else { audioSource.PlayOneShot(forestClips[stepIndex]); }
                timer = footstepTimer * 0.6f;
            }
            else if (timer <= 0 && playerSpeed == crouchSpeed)
            {
                audioSource.volume = stepVolume * 0.2f;
                if (hardSurface) { audioSource.PlayOneShot(soundClips[stepIndex]); }
                else { audioSource.PlayOneShot(forestClips[stepIndex]); }
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
        //characterController.Move(Time.fixedDeltaTime * playerSpeed * new Vector3(moveInputValue.x, 0f, moveInputValue.y));
    }

    private void OnMove(InputValue value)
    {
        moveInputValue = value.Get<Vector2>();
        move = transform.right * moveInputValue.x + transform.forward * moveInputValue.y;
        //Debug.Log(move);
    }

}
