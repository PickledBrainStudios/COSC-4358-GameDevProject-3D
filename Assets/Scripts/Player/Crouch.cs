using UnityEngine;

public class Crouch : MonoBehaviour
{
    [HideInInspector]
    public bool isCrouching = false;
    private float originalHeight;
    private Vector3 originalCenter;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        // Store original values for crouching
        originalCenter = characterController.center;
        originalHeight = characterController.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCrouch();
        }
    }

    private void ToggleCrouch()
    {
        isCrouching = !isCrouching;

        if (isCrouching)
        {
            characterController.center = originalCenter / 2f;
            characterController.height = originalHeight / 2f;
        }
        else
        {
            characterController.center = originalCenter;
            characterController.height = originalHeight;
        }
    }
}
