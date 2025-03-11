using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // 玩家对象
    public Vector3 offset = new Vector3(0, 1, -10); // 摄像机偏移量
    public float smoothSpeed = 5f; // 跟随平滑度

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // 切换场景时不销毁摄像机
    }

    private void LateUpdate()
    {
        if (player == null)
        {
            FindPlayer(); // 如果找不到玩家，尝试重新获取
            return;
        }

        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }

    private void FindPlayer()
    {
        GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
        if (foundPlayer != null)
        {
            player = foundPlayer.transform;
        }
    }
}