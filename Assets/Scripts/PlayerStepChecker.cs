using UnityEngine;

public class PlayerStepChecker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Floor")
        {
            if (Input.GetAxisRaw("Horizontal") != 0) {
                Transform transform = player.transform;
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.505f);
            }
        }
    }
}
