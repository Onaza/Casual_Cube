using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUI : MonoBehaviour
{
    private RectTransform rectTransform;
    private RectTransform oldRectTransform;

    private float interval = 0.1f;
    private bool isRepeat = false;
    private bool isOneway = true;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="start">시작 위치</param>
    /// <param name="end">종료 위치</param>
    /// <param name="repeat">반복</param>
    /// <param name="oneway">왕복</param>
    /// <param name="interval">간격</param>
    public void Play(Vector3 start, Vector3 end, bool repeat = false, bool oneway = true, float interval = 0.1f)
    {
        this.interval = interval;
        this.isRepeat = repeat;
        this.isOneway = oneway;
        oldRectTransform = rectTransform;

        StartCoroutine(Loop(start, end));
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    public void StopAndREstoration()
    {
        StopAllCoroutines();
        rectTransform = oldRectTransform;
    }

    IEnumerator Loop(Vector3 start, Vector3 end)
    {
        while (true)
        {
            yield return StartCoroutine(Move(start, end));

            if(isOneway == false)
                yield return StartCoroutine(Move(end, start));

            if (isRepeat == false)
                break;
        }
    }
    
    IEnumerator Move(Vector3 start, Vector3 end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        rectTransform.anchoredPosition = start;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / interval;

            rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, end, percent);

            yield return null;
        }
    }
}
