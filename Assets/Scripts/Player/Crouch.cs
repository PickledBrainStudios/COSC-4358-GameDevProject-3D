using UnityEngine;

public class Crouch : MonoBehaviour
{
    [HideInInspector]
    public bool isCrouching = false;
    //public float crouchDistance = 1f;
    private float originalHeight;
    //private Vector3 originalCenter;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        // Store original values for crouching
        //originalCenter = characterController.center;
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

        //overhead check
        //cast ray up
        //if(hit.distance < originalHeight/2f)
        // bool dontStand = true

        if (isCrouching)
        {
            //Debug.Log(originalCenter);
            //Debug.Log(originalHeight);
            //Debug.Log(crouchDistance);
            //characterController.center = originalCenter / crouchDistance;
            characterController.height = originalHeight / 2f;
            //Debug.Log(characterController.center);
            //Debug.Log(characterController.height);
        }
        else
        {
            //characterController.center = originalCenter;
            characterController.height = originalHeight;
        }
    }
}
