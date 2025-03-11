using UnityEngine;
using TMPro; // 需要 TextMeshPro 组件
using UnityEngine.SceneManagement;

public class TimeD : MonoBehaviour
{
    public static TimeD instance; // 单例

    public float totalTime = 420f; // 7分钟（420秒）
    private float remainTime;
    private bool timeFlowing = true;

    private TextMeshProUGUI timerText; // 计时器 UI

    private void Awake()
    {
        // 确保只有一个 TimeD 实例
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 切换场景时不销毁
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        remainTime = totalTime;

        // 在场景中创建 UI 计时器
        CreateTimerUI();
    }

    private void Update()
    {
        if (timeFlowing)
        {
            remainTime -= Time.deltaTime;
            UpdateTimerDisplay();

            if (remainTime <= 0)
            {
                remainTime = 0;
                timeFlowing = false;
                TimerEnd();
            }
        }
    }

    // UI 计时器的创建（全局可见）
    private void CreateTimerUI()
    {
        GameObject canvasObj = new GameObject("TimerCanvas");
        canvasObj.layer = LayerMask.NameToLayer("UI");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        GameObject textObj = new GameObject("TimerText");
        textObj.transform.SetParent(canvasObj.transform);

        timerText = textObj.AddComponent<TextMeshProUGUI>();
        timerText.fontSize = 50;
        timerText.alignment = TextAlignmentOptions.Center;
        timerText.color = Color.white;

        // 让计时器固定在屏幕顶部中央
        RectTransform rect = timerText.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(300, 100);
        rect.anchorMin = new Vector2(0.5f, 1f);
        rect.anchorMax = new Vector2(0.5f, 1f);
        rect.pivot = new Vector2(0.5f, 1f);
        rect.anchoredPosition = new Vector2(0, -50);

        DontDestroyOnLoad(canvasObj);
    }

    // 更新 UI 上的倒计时显示
    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainTime / 60f);
        int seconds = Mathf.FloorToInt(remainTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // 时间耗尽时触发的事件
    private void TimerEnd()
    {
        Debug.Log("倒计时结束，触发游戏结局！");
        // 这里可以加上你想触发的事件，比如切换场景：
        // SceneManager.LoadScene("GameOverScene");
    }

    // **对外提供控制方法**
    public float GetRemainingTime() => remainTime;
    public void StopTime() => timeFlowing = false;
    public void StartTime() => timeFlowing = true;
}
