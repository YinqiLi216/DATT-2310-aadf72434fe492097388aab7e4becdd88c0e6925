using System;
using UnityEngine;

public class CameraCenterScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float followSpeed;
    private GameObject player;
    private Boolean hasPlayer;
    private Vector3 centerPos, mousePos, playerPos;
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerPos = player.transform.position;
            hasPlayer = true;
        }
        else
        {
            hasPlayer = false;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Camera mainCamera = Camera.main;
        if (!player.GetComponent<PlayerController>().IsGameOver() && IsMouseInScreen(Input.mousePosition)) {
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            playerPos = player.transform.position;
        }
        if (hasPlayer)
        {
            Vector3 centerPos0 = (mousePos + playerPos) / 2;
            centerPos = Vector3.Lerp(transform.position, centerPos0, followSpeed);
            transform.position = new Vector3(centerPos.x, centerPos.y, transform.position.z);
        }
    }

    private bool IsMouseInScreen(Vector3 mousePos)
    {
        return mousePos.x >= 0 && mousePos.x < Screen.width &&
               mousePos.y >= 0 && mousePos.y < Screen.height;
    }
}
