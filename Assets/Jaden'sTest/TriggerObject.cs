using System.Collections;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    public GameObject prefab;
    public GameObject target;
    public float speed = 1f;

    private void Start()
    {
        prefab.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        prefab.SetActive(true);
        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        while (Vector3.Distance(prefab.transform.position, target.transform.position) > 0.1f)
        {
            prefab.transform.position = Vector3.Lerp(prefab.transform.position, target.transform.position, speed * Time.deltaTime);
            yield return null;
        }
    }
}
