using System;
using System.Collections;
using TMPro;
using UnityEngine;
public class TimeManager : MonoBehaviour
{
    public Action GameStarted;
    public Action TutorialStarted;

    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private AudioSource countAudioSrc;

    private bool isGameStarted;
    private int timeToStart;

    private bool isTutorialEnded;

    private void Awake()
    {
        countAudioSrc = GetComponent<AudioSource>();
        isGameStarted = false;
        isTutorialEnded = true;
    }

    public void OnGameStarted()
    {
        if (isGameStarted)
            return;

        timeToStart = 3;

        isGameStarted = true;

        StartCoroutine(DelayedGameStart());
    }

    public void OnTutorialEnded()
    {
        isTutorialEnded = true;
    }

    IEnumerator DelayedGameStart()
    {
        WaitForSeconds wait = new WaitForSeconds(1.0f);

        if(TutorialStarted != null)
        {
            isTutorialEnded = false;
            TutorialStarted();
        }

        yield return new WaitWhile(() => isTutorialEnded == false);

        TutorialStarted = null;

        text.gameObject.SetActive(true);
        while (timeToStart > 0)
        {
            countAudioSrc.Play();
            text.text = string.Format("{0}", timeToStart);
            yield return wait;
            timeToStart--;
        }
        text.gameObject.SetActive(false);

        GameStarted?.Invoke();

        isGameStarted = false;
    }
}