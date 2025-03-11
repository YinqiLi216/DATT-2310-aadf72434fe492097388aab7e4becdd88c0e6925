using UnityEngine;

public class CloudMovementL : MonoBehaviour
{
    public float speed = 2f;                
    public float fadeDuration = 5f;        
    public float delayDuration = 3f;       
    public Vector3 startPosition;         

    private SpriteRenderer spriteRenderer;
    private float fadeTimer;
    private float delayTimer;              
    private bool fadingOut = true;          
    private bool waiting = false;           

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fadeTimer = 0f;
        delayTimer = 0f;
        startPosition = transform.position;  
    }

    void Update()
    {
        if (waiting)
        {
            
            delayTimer += Time.deltaTime;
            if (delayTimer >= delayDuration)
            {
                StartFadeIn();  
            }
            return; 
        }

       
        transform.Translate(Vector3.left * speed * Time.deltaTime);

     
        fadeTimer += Time.deltaTime;

        float alpha;
        if (fadingOut)
        {
            alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);
        }
        else
        {
            alpha = Mathf.Lerp(0f, 1f, fadeTimer / fadeDuration);
        }

        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }

        
        if (fadeTimer >= fadeDuration)
        {
            if (fadingOut)
            {
                StartWait();  
            }
            else
            {
                fadingOut = true;
                fadeTimer = 0f; 
            }
        }
    }

   
    void StartWait()
    {
        waiting = true;
        delayTimer = 0f;
        transform.position = startPosition;  
    }

    
    void StartFadeIn()
    {
        waiting = false;
        fadeTimer = 0f;
        fadingOut = false;

        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 0f; 
            spriteRenderer.color = color;
        }
    }
}
