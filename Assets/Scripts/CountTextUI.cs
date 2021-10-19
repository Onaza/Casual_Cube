using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountTextUI : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void Play(float start, float end, float interval = 1.0f)
    {
        text.gameObject.SetActive(true);
        StartCoroutine(Loop(start, end, interval));
    }

    private IEnumerator Loop(float start, float end, float interval)
    {
        WaitForSeconds wait = new WaitForSeconds(interval);

       if (start > end)
            interval = -1.0f * interval;

        while (start == end)
        {
            text.text = string.Format("{0}", start);
            start += interval;
            yield return wait;
        }

        text.gameObject.SetActive(false);
    }
}
