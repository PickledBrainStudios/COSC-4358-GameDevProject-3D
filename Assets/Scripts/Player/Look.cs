using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    public float  cameraHeight = 1.0f;
    public Slider sensitivitySlider;
    private Transform cameraTransform, playerTransform;

    //Source: https://www.youtube.com/watch?v=Coch-PkHY54
    //new var
    // This enumeration describes which directions this script should control
    [Flags]
    public enum RotationDirection
    {
        None,
        Horizontal = (1 << 0),
        Vertical = (1 << 1)
    }

    [Tooltip("Which directions this object can rotate")]
    [SerializeField] private RotationDirection rotationDirections;
    [Tooltip("The rotation acceleration, in degrees / second")]
    [SerializeField] private Vector2 acceleration;
    [Tooltip("A multiplier to the input. Describes the maximum speed in degrees / second. To flip vertical rotation, set Y to a negative value")]
    [SerializeField] private Vector2 sensitivity;
    private Vector2 baseSens;
    [Tooltip("The maximum angle from the horizon the player can rotate, in degrees")]
    [SerializeField] private float maxVerticalAngleFromHorizon;
    [Tooltip("The period to wait until resetting the input value. Set this as low as possible, without encountering stuttering")]
    [SerializeField] private float inputLagPeriod;

    private Vector2 velocity; // The current rotation velocity, in degrees
    private Vector2 rotation; // The current rotation, in degrees
    private Vector2 lastInputEvent; // The last received non-zero input value
    private float inputLagTimer; // The time since the last received non-zero input value

    private Vector2 lookInputValue;

    // Start is called a single time before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Lock cursor to the center for looking around
        cameraTransform = Camera.main.transform; //get camera transform
        playerTransform = transform; 
        cameraTransform.position = new Vector3 ( playerTransform.position.x, playerTransform.position.y + cameraHeight, playerTransform.position.z );
        baseSens = sensitivity;
    }

    
    // When this component is enabled, we need to reset the state
    // and figure out the current rotation
    private void OnEnable()
    {
        //Debug.Log("onenable");
        // Reset the state
        velocity = Vector2.zero;
        inputLagTimer = 0;
        lastInputEvent = Vector2.zero;

        // Calculate the current rotation by getting the gameObject's local euler angles
        Vector3 euler = transform.localEulerAngles;
        //Debug.Log(euler.x);
        //Debug.Log(transform.localEulerAngles.x);
        // Euler angles range from [0, 360), but we want [-180, 180)
        if (euler.x >= 180)
        {
            euler.x -= 360;
        }
        euler.x = ClampVerticalAngle(euler.x);

        //Debug.Log(euler.x);

        // Set the angles here to clamp the current rotation
        ////////// Big change here************************************
        transform.localEulerAngles = euler;
        //////////
        // Rotation is stored as (horizontal, vertical), which corresponds to the euler angles
        // around the y (up) axis and the x (right) axis
        rotation = new Vector2(euler.y, rotation.y);
    }
    

    private float ClampVerticalAngle(float angle)
    {
        return Mathf.Clamp(angle, -maxVerticalAngleFromHorizon, maxVerticalAngleFromHorizon);
    }

    private Vector2 GetInput() {
        //add lag timer
        inputLagTimer += Time.deltaTime;
        //Get input vector
        Vector2 input = new (
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")
        );

        //for controler
        input += lookInputValue;



        /* Somtimes at fast frame rates, unity will not recieve input events every frame, which results
         * in zero values being given above. This can cause stuttering and make it difficult to fine tune
         * the acceleration setting. To fix this, disregard zero values. If the lag timer has passed the lag period, we can
         * assume that the user is not giving any input, so we actually want to set the input value to zero at that time.
         * Thus save the input value if it is non-zero or lag timer is met
         */ 
        if ((Mathf.Approximately(0, input.x) && Mathf.Approximately(0, input.y)) == false || inputLagTimer >= inputLagPeriod) {
            lastInputEvent = input;
            inputLagTimer = 0;
        }
        return lastInputEvent;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // The wanted velocity is the current input scaled by the sensitivity
        // This is also the maximum velocity
        Vector2 wantedVelocity = GetInput() * sensitivity;

        // Zero out the wanted velocity if this controller does not rotate in that direction
        if ((rotationDirections & RotationDirection.Horizontal) == 0)
        {
            wantedVelocity.x = 0;
        }
        if ((rotationDirections & RotationDirection.Vertical) == 0)
        {
            wantedVelocity.y = 0;
        }

        // Calculate new rotation
        velocity = new Vector2(
            Mathf.MoveTowards(velocity.x, wantedVelocity.x, acceleration.x * Time.fixedDeltaTime),
            Mathf.MoveTowards(velocity.y, wantedVelocity.y, acceleration.y * Time.fixedDeltaTime));
        rotation += velocity * Time.deltaTime;
        rotation.y = ClampVerticalAngle(rotation.y);

        // Convert the rotation to euler angles
        transform.localEulerAngles = new Vector3(0, rotation.x, 0);
        Camera.main.transform.localEulerAngles = new Vector3(rotation.y, 0, 0);

        //old code for look
        /*
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity; // Inverted for natural movement

        // Update camera position to player position
        cameraTransform.position = playerTransform.position + cameraLocation;

        verticalRotation += mouseY;//rotates camera up and down, clamp at 90 degrees up and down
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Limit vertical rotation to prevent flipping

        horizontalRotation += mouseX;//rotate based on mouse horizontal movement

        cameraTransform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
        playerTransform.rotation *= Quaternion.Euler(0f, mouseX, 0f);
        */
    }

    public void ChangeSensitivity() {
        sensitivity = 0.02f * sensitivitySlider.value * baseSens;
        //Debug.Log(sensitivity);
    }

    private void OnLook(InputValue value)
    {
        lookInputValue = value.Get<Vector2>();
        //Debug.Log(lookInputValue);
    }

}

