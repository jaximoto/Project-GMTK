using UnityEngine;

public class SpriteFade : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Color spriteColor;
    public bool startFadingOut = true;
    public float fadeSpeed;
    bool fadingOut = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteColor = spriteRenderer.color;
        if (startFadingOut)
            fadingOut = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadingOut)
        {
            if (spriteRenderer.color.a <= 0.05)
            {
                fadingOut = false;
                Debug.Log("Faded out");
            }
                
            else
            {
                Color newColor = new Color(spriteColor.r, spriteColor.g, spriteColor.b, Mathf.Lerp(spriteRenderer.color.a, 0f, Time.deltaTime * fadeSpeed));
                spriteRenderer.color = newColor;
            }
        }
        else
        {
            if (spriteRenderer.color.a >= .95)
                fadingOut = true;
            else
            {
                Color newColor = new Color(spriteColor.r, spriteColor.g, spriteColor.b, Mathf.Lerp(spriteRenderer.color.a, 1f, Time.deltaTime * fadeSpeed));
                spriteRenderer.color = newColor;
            }
        }
    }
}
