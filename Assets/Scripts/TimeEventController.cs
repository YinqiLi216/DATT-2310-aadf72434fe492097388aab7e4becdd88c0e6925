using System;
using UnityEngine;

public class TimeEventController : MonoBehaviour
{
    public GameObject panel, timer;
    public float timeTrigger;
    public string[] lines;
    public string moveCommand;
    public float commandDuration;
    private bool triggered;
    private GameObject player;
    private PlayerController playerC;

    void Start()
    {
        triggered = true;

        // 查找玩家对象
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerC = player.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogError("TimeEventController: 未找到玩家对象，请检查场景中的 Player 标签");
        }

        // 查找 Timer 对象（使用新的 API）
        if (timer == null)
        {
            Timer timerComponent = FindFirstObjectByType<Timer>();
            if (timerComponent != null)
            {
                timer = timerComponent.gameObject;
            }
            else
            {
                Debug.LogError("Timer object not found in scene.");
            }
        }
    }

    void Update()
    {
        if (timer != null && timer.activeSelf && timer.GetComponent<Timer>().GetRemainingTime() <= timeTrigger && triggered)
        {
            panel.SetActive(true);
            panel.GetComponent<TextPresentor>().lines = lines;
            triggered = false;

            if (playerC != null)
            {
                if (moveCommand == "right")
                {
                    MovePlayer(Vector2.right);
                }
                else if (moveCommand == "left")
                {
                    MovePlayer(Vector2.left);
                }
            }
        }
    }

    private void MovePlayer(Vector2 direction)
    {
        Rigidbody2D rb = playerC.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * playerC.moveSpeed;
            Invoke(nameof(ResetPlayerMovement), commandDuration);
        }
    }

    private void ResetPlayerMovement()
    {
        if (playerC != null)
        {
            playerC.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }
}
