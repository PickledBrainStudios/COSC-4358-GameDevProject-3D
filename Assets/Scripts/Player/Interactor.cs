using UnityEngine;
using TMPro;
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
    
    public float interactRange = 2;
    public Texture2D reticle;
    public Texture2D reticleActive;
    private Transform interactorSource;
    //private TextMeshProUGUI centerText;
    private RawImage rawImage;

    private void Start()
    {
        interactorSource = Camera.main.transform;
        //centerText = GameObject.Find("CenterScreen").GetComponent<TextMeshProUGUI>();
        rawImage = GameObject.FindWithTag("UI_Reticle").GetComponent<RawImage>();
    }

    void Update()
    {
        Ray r_0 = new(interactorSource.position, interactorSource.forward);
        if (Physics.Raycast(r_0, out RaycastHit hitInfo_0, interactRange))
        {
            if (hitInfo_0.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                Debug.DrawRay(interactorSource.position, interactorSource.forward * interactRange, Color.green);
                //centerText.text = "Press E";
                rawImage.texture = reticleActive;
            }
            else
            {
                Debug.DrawRay(interactorSource.position, interactorSource.forward * interactRange, Color.red);
                //centerText.text = "";
                rawImage.texture = reticle;
            }
        }
        else 
        {
            Debug.DrawRay(interactorSource.position, interactorSource.forward * interactRange, Color.red);
            //centerText.text = "";
            rawImage.texture = reticle;
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            Ray r = new(interactorSource.position, interactorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange)) {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) {
                    //hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj
                    interactObj.Interact();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if(other.TryGetComponent(out CConsumable consumerObj)) {
            consumerObj.Consume();
        }
        if (other.TryGetComponent(out QQuickTimeEvent qteObj))
        {
            qteObj.ActivateQTE();
        }
    }
}
