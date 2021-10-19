using System.Collections;
using UnityEngine;

public class Twinkle2D : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private float interval = 0.1f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Play(float interval = 0.1f)
    {
        this.interval = interval;
        StartCoroutine(Loop());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    public void StopAndRestoration()
    {
        StopAllCoroutines();

        Color color = spriteRenderer.color;
        color.a = 1.0f;
        spriteRenderer.color = color;
    }

    private IEnumerator Loop()
    {
        while(true)
        {
            // Alpha 값을 1 -> 0으로 : fade out
            yield return StartCoroutine(Fade(1, 0.3f));
            // Alpha 값을 0 -> 1으로 : fade in
            yield return StartCoroutine(Fade(0.3f, 1));
        }
    }

    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while(percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / interval;

            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(start, end, percent);
            spriteRenderer.color = color;
            yield return null;
        }
    }
}
