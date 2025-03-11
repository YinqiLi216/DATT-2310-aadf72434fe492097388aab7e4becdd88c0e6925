using UnityEngine;
using UnityEngine.Rendering.Universal;  // 引入2D灯光系统

public class StreetLightFlicker : MonoBehaviour
{
    private Light2D streetLight;  // 2D点光源
    public float minIntensity = 0.8f;  // 最小亮度
    public float maxIntensity = 1.5f;  // 最大亮度
    public float flickerSpeed = 0.1f;  // 闪烁频率（秒）
    
    private void Start()
    {
        // 获取 Light2D 组件
        streetLight = GetComponent<Light2D>();
        if (streetLight == null)
        {
            Debug.LogError("StreetLightFlicker: 找不到 Light2D 组件，请确保该 GameObject 上有 Light2D！");
            return;
        }

        // 开始闪烁循环
        InvokeRepeating(nameof(FlickerLight), 0f, flickerSpeed);
    }

    void FlickerLight()
    {
        if (streetLight != null)
        {
            // 使灯光在 minIntensity 和 maxIntensity 之间随机变动
            streetLight.intensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}
