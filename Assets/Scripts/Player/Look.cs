using UnityEngine;

public class Look : MonoBehaviour
{

    public float mouseSensitivity = 2.0f;
    public Vector3 cameraLocation = new Vector3(0f,0.5f,0f);
    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;
    private Transform cameraTransform, playerTransform;

    // Start is called a single time before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Lock cursor to the center for looking around
        cameraTransform = Camera.main.transform; //get camera transform
        playerTransform = transform; //variablize for rotating player in look script
        horizontalRotation = playerTransform.eulerAngles.y;
        //horizontalRotation += playerTransform.yAngle; //something weird is happening. I want to turn the camera to the players initial forward direction
        //Debug.Log(playerTransform.rotation.y);
        //Debug.Log(Quaternion.LookRotation(cameraTransform.forward));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity; // Inverted for natural movement

        // Update camera position to player position
        cameraTransform.position = playerTransform.position + cameraLocation;

        verticalRotation += mouseY;//rotates camera up and down, clamp at 90 degrees up and down
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Limit vertical rotation to prevent flipping

        horizontalRotation += mouseX;//rotate based on mouse horizontal movement

        cameraTransform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
        playerTransform.rotation *= Quaternion.Euler(0f, mouseX, 0f);
    }
}

