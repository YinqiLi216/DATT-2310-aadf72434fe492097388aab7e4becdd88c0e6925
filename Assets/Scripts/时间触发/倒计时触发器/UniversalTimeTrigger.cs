using UnityEngine;
using UnityEngine.Events;

public class UniversalTimeTrigger : MonoBehaviour
{
    [Tooltip("当剩余时间恰好等于这个整数秒时触发")]
    public int triggerSecond = 390; // 例如 390 代表 6:30
    
    [Tooltip("是否只触发一次")]
    public bool triggerOnce = true;

    [Tooltip("当触发条件满足时调用的事件")]
    public UnityEvent onTrigger;

    private bool hasTriggered = false;

    void Update()
    {
        // 如果只触发一次且已经触发，则不再检测
        if (triggerOnce && hasTriggered)
            return;

        // 获取全局倒计时
        if (TimeD.instance != null)
        {
            float remainingTime = TimeD.instance.GetRemainingTime();
            int intRemain = Mathf.FloorToInt(remainingTime); 
            
            // 如果剩余时间的整数部分“恰好”等于指定秒数
            if (intRemain == triggerSecond)
            {
                onTrigger.Invoke();
                hasTriggered = true;
            }
        }
        else
        {
            Debug.LogWarning("未找到全局计时器 TimeD，请确保它存在并且是单例。");
        }
    }
}
