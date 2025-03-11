using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public CanvasGroup BlackScreen;  // 黑屏 UI (CanvasGroup)
    public ParticleSystem rainEffect; // 雨滴特效 (Particle System)
    public float fadeDuration = 3f;  // 渐变时间

    void Start()
    {
        // 初始化黑屏
        BlackScreen.alpha = 1;
        rainEffect.Stop(); // 先隐藏雨滴
        StartCoroutine(FadeInScene());
    }

    IEnumerator FadeInScene()
    {
        rainEffect.Play(); // 开启雨滴特效

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            BlackScreen.alpha = Mathf.Lerp(1, 0, timer / fadeDuration); // 黑屏渐变
            yield return null;
        }

        BlackScreen.alpha = 0; // 确保完全透明
    }
}
