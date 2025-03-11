using System;
using UnityEngine;

public class EventInteractionController : MonoBehaviour
{
    public GameObject panel;
    public string[] lines;
    public string moveCommand;
    public float commandDuration; // 移动的持续时间
    private bool playerIn;
    private GameObject player;
    private PlayerController playerC;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerC = player.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogError("EventInteractionController: 未找到玩家对象，请检查场景中的 Player 标签");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIn = false;
        }
    }

    void Update()
    {
        if (!panel.activeSelf && playerIn && Input.GetKeyUp(KeyCode.E))
        {
            panel.SetActive(true);
            panel.GetComponent<TextPresentor>().lines = lines;

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
            Invoke(nameof(ResetPlayerMovement), commandDuration); // 一定时间后恢复速度
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