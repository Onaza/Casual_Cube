using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private Controller controller;
    [SerializeField]
    private TutorialManager tutorialManager;
    [SerializeField]
    private PlayManager playManager;
    [SerializeField]
    private TileManager tileManager;
    [SerializeField]
    private TimeManager timeManager;
    [SerializeField]
    private UIRoot uiRoot;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            tutorialManager.TutorialEnded += timeManager.OnTutorialEnded;
            timeManager.TutorialStarted += tutorialManager.OnTutorialStarted;
        }

        playManager.Initialize(player, tileManager);

        BindingEvents();
    }
    private void BindingEvents()
    {
        controller.Swiped += player.OnSwiped;

        playManager.ScoreChanged += uiRoot.OnScoreChanged;
        playManager.BestScoreChanged += uiRoot.OnBestScoreChanged;
        playManager.GameEnded += controller.OnGameEnded;
        playManager.GameEnded += uiRoot.OnGameEnded;

        timeManager.GameStarted += controller.OnGameStarted;
        timeManager.GameStarted += playManager.OnGameStarted;
        timeManager.GameStarted += uiRoot.OnGameStarted;

        uiRoot.GameStarted += timeManager.OnGameStarted;
    }

    private void UnBindingEvents()
    {
        controller.Swiped -= player.OnSwiped;

        playManager.ScoreChanged -= uiRoot.OnScoreChanged;
        playManager.BestScoreChanged -= uiRoot.OnBestScoreChanged;
        playManager.GameEnded -= controller.OnGameEnded;
        playManager.GameEnded -= uiRoot.OnGameEnded;

        timeManager.GameStarted -= controller.OnGameStarted;
        timeManager.GameStarted -= playManager.OnGameStarted;
        timeManager.GameStarted -= uiRoot.OnGameStarted;

        uiRoot.GameStarted -= timeManager.OnGameStarted;
    }

    private void OnDestroy()
    {
        UnBindingEvents();
    }
}