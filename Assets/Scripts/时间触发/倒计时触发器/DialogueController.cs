using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [Tooltip("用于显示对话的 TextMeshProUGUI 组件")]
    public TextMeshProUGUI dialogueText;
    
    [Tooltip("对话的每一句文本，每一项代表一行对话")]
    public string[] dialogueLines;
    
    [Tooltip("每个字符出现的间隔秒数")]
    public float typeDelay = 0.05f;
    
    private int currentLine = 0;
    // 可选：添加一个状态标志，防止重复调用对话
    public bool isPlayingDialogue = false;

    void Start()
    {
        if (isPlayingDialogue) return;
        isPlayingDialogue = true;
        
        // 开始对话时先暂停全局计时器
        if (TimeD.instance != null)
            TimeD.instance.StopTime();
        StartCoroutine(TypeDialogue());
    }

    IEnumerator TypeDialogue()
    {
        while (currentLine < dialogueLines.Length)
        {
            dialogueText.text = "";
            foreach (char c in dialogueLines[currentLine])
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(typeDelay);
            }
            
            // 每一句对话播放完毕后等待玩家按下确认键（这里使用 E 键）
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            currentLine++;
        }
        
        // 对话结束后恢复全局倒计时
        if (TimeD.instance != null)
            TimeD.instance.StartTime();
        
        // 清空文本，并隐藏对话面板
        dialogueText.text = "";
        gameObject.SetActive(false);
        isPlayingDialogue = false;
    }
}