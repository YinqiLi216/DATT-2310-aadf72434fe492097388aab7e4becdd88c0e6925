using System;
using UnityEngine;

public class EventInteractionController : MonoBehaviour
{
    public GameObject panel;
    public string[] lines;
    public string moveCommand;
    public float commandDuration;
    private Boolean playerIn;
    private GameObject player;
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerIn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerIn = false;
        }
    }

    void Update()
    {
        if (!panel.activeSelf && playerIn && Input.GetKeyUp(KeyCode.E)) {
            panel.SetActive(true);
            panel.GetComponent<TextPresentor>().lines = lines;
            PlayerController playerC = player.GetComponent<PlayerController>();
            if (moveCommand == "right") {
                playerC.rightCommand = true;
                playerC.AddCommandDuration(commandDuration);
            }
            if (moveCommand == "left")
            {
                playerC.leftCommand = true;
                playerC.AddCommandDuration(commandDuration);
            }
        }
    }
}
