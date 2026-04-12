using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//呱： 写在最前面 ——> 这个脚本挂载在遮幕上
//    哈基蛙你这家伙……又在执着于写 妙妙画面效果了吗
//    这个适用于想要实现 渐显/渐隐 取决你调用这其中的哪个函数咯hiahia
public class Transition : MonoBehaviour
{
    
    private SpriteRenderer SR;

    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }

   //呱：这是渐显
    public void FadeIn(float duration)
    {
        StartCoroutine(Fade(0f, 1f, duration));
    }
    
    //呱：这是渐隐
    public void FadeOut(float duration)
    {
        StartCoroutine(Fade(1f, 0f, duration));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float time = 0f;
        Color color = SR.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            color.a = alpha;
            SR.color = color;
            yield return null;
        }

        color.a = endAlpha;
        SR.color = color;
    }
}
