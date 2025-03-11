using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class StartEvent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject panel, timer;
    public string[] lines;
    private Boolean triggered;
    void Start()
    {
        triggered = true;
    }

    void Update() {
        if (triggered) {
            panel.SetActive(true);
            panel.GetComponent<TextPresentor>().lines = lines;
            triggered = false;
        }
    }
}
