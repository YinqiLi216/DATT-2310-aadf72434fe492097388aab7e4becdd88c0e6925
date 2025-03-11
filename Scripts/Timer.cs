using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float totalTime;
    public TextMeshProUGUI timerText;
    private float remainTime;
    private GameObject player;
    private Boolean timeFlowing;
    void Start()
    {
        remainTime = totalTime;
        UpdateTimerDisplay();
        timeFlowing = true;
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeFlowing) {
            if (Input.GetKey(KeyCode.Space))
            {
                remainTime -= 20 * Time.deltaTime;
            }
            else {
                remainTime -= Time.deltaTime;
            }
        }
        if (!player.GetComponent<PlayerController>().IsGameOver())
        {
            UpdateTimerDisplay();
        }
        else
        {
            timeFlowing = false;
            timerText.text = "XX:XX";
            timerText.color = Color.red;
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt((remainTime + 1) / 60f);
        int seconds = Mathf.FloorToInt((remainTime + 1) % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public float getRemainingTime() { 
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
