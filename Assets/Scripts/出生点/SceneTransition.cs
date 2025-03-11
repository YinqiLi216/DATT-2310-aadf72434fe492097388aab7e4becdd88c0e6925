using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSceneLoader : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private string exitDirection; // "LeftExit" 或 "RightExit"

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 记录离开的方向
            SceneTransitionData.lastExit = exitDirection;
            // 加载下一个场景
            SceneManager.LoadScene(nextSceneName);
        }
    }
}