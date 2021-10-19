using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIRoot : MonoBehaviour
{
    public Action GameStarted;
    public Action TutorialStarted;

    [SerializeField]
    private GameObject topPanel;
    [SerializeField]
    private GameObject gameoverPanel;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI bestScoreText;
    [SerializeField]
    private GameObject titlePanel;
    [SerializeField]
    private CountTextUI countText;

    private AudioSource gameOverAudioSrc;

    private void Awake()
    {
        gameOverAudioSrc = GetComponent<AudioSource>();

        topPanel.SetActive(false);
        gameoverPanel.SetActive(false);
    }

    public void OnScoreChanged(int score)
    {
        scoreText.text = string.Format("{0}", score);
    }

    public void OnBestScoreChanged(int record)
    {
        bestScoreText.text = string.Format("{0}", record);
    }

    public void OnGameStarted()
    {
        topPanel.gameObject.SetActive(true);
        scoreText.text = "0";
    }

    public void OnGameEnded()
    {
        gameOverAudioSrc.Play();
        gameoverPanel.gameObject.SetActive(true);
    }

    public void OnStartButtonClicked()
    {
        titlePanel.gameObject.SetActive(false);
        GameStarted?.Invoke();
    }

    public void OnRetryButtonClicked()
    {
        gameoverPanel.gameObject.SetActive(false);
        topPanel.gameObject.SetActive(false);
        GameStarted?.Invoke();
    }
}