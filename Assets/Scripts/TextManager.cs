using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextPresentor : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public GameObject timer;
    public string[] lines;
    public float delay;
    private int index;
    private bool started;

    void Start()
    {
        gameObject.SetActive(false);
        textMeshPro.text = string.Empty;
        started = false;
        timer = GameObject.FindGameObjectWithTag("Timer");
    }

    void Update()
    {
        if (gameObject.activeSelf && !started)
        {
            StartDialogue();
            if (timer != null)
            {
                if (timer.activeSelf)
                {
                    timer.GetComponent<Timer>().StopTime();
                }
            }
            started = true;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            // 防止数组为空或索引越界
            if (lines == null || lines.Length == 0)
            {
                Debug.LogWarning("No dialogue lines assigned.");
                return;
            }
            if (index >= lines.Length)
            {
                Debug.LogWarning("Index is out of bounds: " + index);
                return;
            }

            if (textMeshPro.text == lines[index])
            {
                NextLine();
            }
            else 
            { 
                StopAllCoroutines();
                textMeshPro.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine()); 
    }

    IEnumerator TypeLine()
    {
        textMeshPro.text = string.Empty; // 每次开始前清空文本
        foreach (char c in lines[index].ToCharArray())
        {
            textMeshPro.text += c;
            yield return new WaitForSeconds(delay);
        }
    }

    void NextLine()
    {
        textMeshPro.text = string.Empty;
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            started = false;
            timer.GetComponent<Timer>().StartTime();
        }
    }
}

