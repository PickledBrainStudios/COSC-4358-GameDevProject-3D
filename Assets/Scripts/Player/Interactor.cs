using UnityEngine;
using UnityEngine.UI;

interface IInteractable {
    public void Interact();
}
interface CConsumable { 
    public void Consume();
}
interface QQuickTimeEvent {
    public void ActivateQTE();
}

public class Interactor : MonoBehaviour
{
    
    public float interactRange = 3f;
    public Texture2D reticle;
    public Texture2D reticleActive;
    private Transform interactorSource;
    private PlayerManager playerManager;
    
    private RawImage rawImage;

    private bool inRange = false;
    private IInteractable interactObj;

    private void Awake()
    {
        interactorSource = Camera.main.transform;
        //centerText = GameObject.Find("CenterScreen").GetComponent<TextMeshProUGUI>();
        rawImage = GameObject.FindWithTag("UI_Reticle").GetComponent<RawImage>();
        playerManager = GetComponent<PlayerManager>();
    }

    void Update()
    {
        Ray r_0 = new(interactorSource.position, interactorSource.forward);
        if (Physics.Raycast(r_0, out RaycastHit hitInfo_0, interactRange, 3))
        {
            //Debug.Log(hitInfo_0.collider.gameObject.name);
            //If your ray collides with something
            if (hitInfo_0.collider.gameObject.TryGetComponent(out IInteractable obj))
            {
                //If your ray hits an interactable
                //Debug.Log("INTERACTIONS");
                Debug.DrawRay(interactorSource.position, interactorSource.forward * interactRange, Color.green);
                //centerText.text = "Press E";
                interactObj = obj;
                rawImage.texture = reticleActive;
                inRange = true;
                if (Input.GetKeyDown(KeyCode.E))
                    Action(interactObj);
            }
            else
            {
                //If ray hits normal collider
                //Debug.Log("no interaction");
                Debug.DrawRay(interactorSource.position, interactorSource.forward * interactRange, Color.red);
                //centerText.text = "";
                inRange = false;
                rawImage.texture = reticle;
            }
        }
        else 
        {
            //If your ray doesnt collide with anything
            //Debug.Log("miss");
            Debug.DrawRay(interactorSource.position, interactorSource.forward * interactRange, Color.red);
            //centerText.text = "";
            rawImage.texture = reticle;
        }
        /*
        if (Input.GetKeyDown(KeyCode.E)) {
            Ray r = new(interactorSource.position, interactorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange)) {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) {
                    //hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj
                    interactObj.Interact();
                }
            }
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out CConsumable consumerObj)) {
            consumerObj.Consume();
        }
        if (other.TryGetComponent(out QQuickTimeEvent qteObj))
        {
            qteObj.ActivateQTE();
        }
    }

    private void OnInteract() {
        if (inRange && playerManager.inControl)
        {
            Debug.Log("A Press");
            Action(interactObj);
        }
        else
        {
            Debug.Log("NOT IN RANGE - NO CONTROL");
        }
    }

    private void Action(IInteractable interactObj) {
        Debug.Log("INTERACT");
        interactObj.Interact();
    }

}
