using System;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveSpeed, direction0, faceDirection0;
    public Boolean rightCommand, leftCommand;
    public GameObject panel;
    private Rigidbody2D rigidBody;
    public TextMeshProUGUI gameoverText;
    private Boolean gameover, moved;
    private int runningFactor;
    private float commandDuration;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        faceDirection0 = 1;
        direction0 = 1;
        gameover = false;
        moved = false;
        gameoverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Boolean running = Input.GetKey(KeyCode.LeftShift);
        runningFactor = running ? 2 : 1;
        if (commandDuration > 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                commandDuration -= 20 * Time.deltaTime;
            }
            else
            {
                commandDuration -= Time.deltaTime;
            }
        }
        else {
            leftCommand = false;
            rightCommand = false;
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 velocity = rigidBody.linearVelocity;
        if (!panel.activeSelf && !IsGameOver())
        {
            if (leftCommand && commandDuration > 0)
            {
                velocity.x = (Math.Max(horizontal, 0) - 1) * runningFactor * moveSpeed;
            }
            else if (rightCommand && commandDuration > 0)
            {
                velocity.x = (Math.Min(horizontal, 0) + 1) * runningFactor * moveSpeed;
            }
            else 
            {
                velocity.x = horizontal * runningFactor * moveSpeed;
            }
            rigidBody.linearVelocity = velocity;
            if (Math.Abs(velocity.x) > 0.1) { 
                moved = true;
            }
        }

        direction0 = velocity.x > 0 ? 1 : (velocity.x < 0 ? -1 : direction0);
        faceDirection0 += 0.2f * direction0;
        faceDirection0 = Math.Min(1, Math.Max(-1, faceDirection0));

        transform.localScale = new Vector3(faceDirection0, 1, 1);
        if (transform.position.y < -20)
        {
            gameover = true;
            gameoverText.gameObject.SetActive(true);
            gameoverText.text = "You fell into the abyss in your embrace of freedom...";
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Danger")) { 
            gameover = true;
        }
    }
    public Boolean IsGameOver() {
        return gameover;
    }

    public Boolean IsMoved()
    {
        return moved;
    }
    public void AddCommandDuration(float duration)
    {
        commandDuration += duration;
    }
}
