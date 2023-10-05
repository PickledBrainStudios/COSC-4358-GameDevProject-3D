using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [HideInInspector]
    public string objectID;

    private void Awake()
    {
        //Unique ID identifier so that multiple the same object (same name) can be singleton at once. i.e healthpacks and consumables
        objectID = name + transform.position.ToString();

        //We want to handle when we reload a previous scene to delete any duplicate dont destory obj that the scene may load. 
        //to do so we find evey object with a DontDestroy component
        for (int i = 0; i < Object.FindObjectsOfType<DontDestroy>().Length; i++)
        {
            //then we compare to see if that obj is this or not this component game object. If that object is not this then check id
            if (Object.FindObjectsOfType<DontDestroy>()[i] != this && Object.FindObjectsOfType<DontDestroy>()[i].objectID == objectID)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}
