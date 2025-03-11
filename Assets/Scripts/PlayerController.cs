using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;      // 玩家正常移动速度
    public float autoMoveSpeed = 2f; // 自动向左移动的输入偏移值
    public bool autoMoveActive = false;  // 是否启用自动左移

    private Rigidbody2D rigidBody;
    private Animator anim;
    private bool isGameOver = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isGameOver) return;

        // 获取玩家的水平输入
        float horizontal = Input.GetAxis("Horizontal");

        // 如果启用了自动移动，则自动向左增加一个负的输入
        if (autoMoveActive)
        {
            horizontal -= autoMoveSpeed;
        }

        // 计算最终速度，并更新 Rigidbody2D
        Vector2 velocity = new Vector2(horizontal * moveSpeed, rigidBody.linearVelocity.y);
        rigidBody.linearVelocity = velocity;

        // 更新动画状态：只要有移动就播放走路动画
        anim.SetBool("walking", Mathf.Abs(horizontal) > 0.01f);

        // 根据移动方向翻转角色
        if (horizontal > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontal < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Danger"))
        {
            isGameOver = true;
        }
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    // 外部调用来启用/禁用自动移动
    public void EnableAutoMove()
    {
        autoMoveActive = true;
    }

    public void DisableAutoMove()
    {
        autoMoveActive = false;
    }
}