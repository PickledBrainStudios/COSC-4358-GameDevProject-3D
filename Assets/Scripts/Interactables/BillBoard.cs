using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public float fadeSpeed = 1f;
    private SpriteRenderer sprite;
    private float originalAplpha;
    bool fading, shining;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originalAplpha = sprite.color.a;
        fading = true;
        shining = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        if (fading && sprite.color.a > 0)
        {
            sprite.color = new Color(255f, 255f, 255f, sprite.color.a - (Time.deltaTime * fadeSpeed));
        }
        else if (sprite.color.a <= 0) 
        {
            fading = false;
            shining = true;
        }
        if (shining && sprite.color.a < originalAplpha)
        {
            sprite.color = new Color(255f, 255f, 255f, sprite.color.a + (Time.deltaTime * fadeSpeed));
        }
        else if (sprite.color.a >= originalAplpha)
        {
            fading = true;
            shining = false;
        }
    }
}
