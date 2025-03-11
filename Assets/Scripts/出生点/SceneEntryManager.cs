using UnityEngine;

public class SceneEntryManager : MonoBehaviour
{
    public Transform spawnLeft;  // 如果玩家从 RightExit 离开，则出生在左边
    public Transform spawnRight; // 如果玩家从 LeftExit 离开，则出生在右边

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("SceneEntryManager: 未找到玩家对象！");
            return;
        }

        // 根据上个场景的离开方向决定玩家的出生位置
        if (SceneTransitionData.lastExit == "LeftExit")
        {
            player.transform.position = spawnRight.position;
        }
        else if (SceneTransitionData.lastExit == "RightExit")
        {
            player.transform.position = spawnLeft.position;
        }
        else
        {
            Debug.LogWarning("SceneEntryManager: 未指定上一个出口，使用默认出生点。");
        }
    }
}