using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // 单例实例
    private static Timer instance;

    // 计时器相关
    public float totalTime;
    public TextMeshProUGUI timerText;
    private float remainTime;
    private GameObject player;
    private bool timeFlowing;

    private void Awake()
    {
        // 确保场景中只有一个 Timer 对象
        if (instance == null)
        {
            instance = this;
            // 对根物体调用 DontDestroyOnLoad
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        remainTime = totalTime;
        UpdateTimerDisplay();
        timeFlowing = true;

        // 查找玩家
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (timeFlowing)
        {
            // 如果按下空格，就加速减少时间
            if (Input.GetKey(KeyCode.Space))
            {
                remainTime -= 20 * Time.deltaTime;
            }
            else
            {
                remainTime -= Time.deltaTime;
            }
        }

        // 如果玩家存在且游戏未结束，则更新显示
        if (player != null && !player.GetComponent<PlayerController>().IsGameOver())
        {
            UpdateTimerDisplay();
        }
        else
        {
            // 玩家不存在或游戏结束时
            timeFlowing = false;
            timerText.text = "XX:XX";
            timerText.color = Color.red;
        }

        // 当时间耗尽，停止计时
        if (remainTime <= 0)
        {
            StopTime();
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt((remainTime + 1) / 60f);
        int seconds = Mathf.FloorToInt((remainTime + 1) % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // 修改方法名称为 GetRemainingTime 并确保是 public
    public float GetRemainingTime()
    {
        return remainTime;
    }

    public void StopTime()
    {
        timeFlowing = false;
    }

    public void StartTime()
    {
        timeFlowing = true;
    }
}
