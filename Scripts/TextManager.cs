using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextPresentor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI textMeshPro;
    public GameObject timer;
    public string[] lines;
    public float delay;
    private int index;
    private Boolean started;
    void Start()
    {
        gameObject.SetActive(false);
        textMeshPro.text = string.Empty;
        started = false;
        timer = GameObject.FindGameObjectWithTag("Timer");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && !started)
        {
            StartDialogue();
            timer.GetComponent<Timer>().StopTime();
            started = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
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

    void StartDialogue(){
        index = 0;
        StartCoroutine(TypeLine()); 
    }

    IEnumerator TypeLine() {
        foreach (char c in lines[index].ToCharArray()) {
            textMeshPro.text += c;
            yield return new WaitForSeconds(delay);
        }
    }

    void NextLine() {
        textMeshPro.text = String.Empty;
        if (index < lines.Length - 1) {
            index++;
            StartCoroutine (TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            started = false;
            timer.GetComponent<Timer>().StartTime();
        }
    }
}
