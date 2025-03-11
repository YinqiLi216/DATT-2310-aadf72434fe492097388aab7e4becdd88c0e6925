using UnityEngine;

public class CloudMovementR : MonoBehaviour
{
    public float speed = 2f;                // 向左移动的速度
    public float fadeDuration = 5f;         // 淡入所需时间
    public float delayDuration = 3f;        // 每次loop淡入前的等待时间
    public Vector3 startPosition;           // 云的初始位置
    public float resetXPosition = -10f;     // 当云移动到这个位置就重置（左侧屏幕外）

    private SpriteRenderer spriteRenderer;
    private float fadeTimer;                 // 透明度计时器
    private float delayTimer;                // 等待计时器
    private bool fadingIn = false;           // 是否正在淡入
    private bool waiting = false;            // 是否处于等待阶段

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;  // 记录初始位置
        StartWait();                         // 游戏开始时就先等待（从透明开始）
    }

    void Update()
    {
        if (waiting)
        {
            // 等待阶段，只计时，不移动
            delayTimer += Time.deltaTime;
            if (delayTimer >= delayDuration)
            {
                StartFadeIn();  // 等待结束，开始淡入
            }
            return;
        }

        if (fadingIn)
        {
            // 淡入阶段
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, fadeTimer / fadeDuration);
            SetCloudAlpha(alpha);

            if (fadeTimer >= fadeDuration)
            {
                fadingIn = false;  // 淡入完成
            }
        }
        else
        {
            // 移动阶段
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            // 如果云超出屏幕左侧，重置到初始位置，并进入等待
            if (transform.position.x < resetXPosition)
            {
                StartWait();
            }
        }
    }

    void StartWait()
    {
        waiting = true;
        delayTimer = 0f;
        transform.position = startPosition;  // 云直接回到初始位置
        SetCloudAlpha(0f);                    // 初始透明
    }

    void StartFadeIn()
    {
        waiting = false;
        fadingIn = true;
        fadeTimer = 0f;
    }

    void SetCloudAlpha(float alpha)
    {
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }
    }
}
