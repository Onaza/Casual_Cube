using System;
using UnityEngine;
public class PlayManager : MonoBehaviour
{
    public Action GameEnded;
    public Action<int> BestScoreChanged;
    public Action<int> ScoreChanged;

    private Player player;
    private TileManager tileManager;
    private LevelManager levelManager;
    private SaveManager saveManager;

    private bool isGameover;
    private bool isInitialized;
    private int score;
    private int bestScore;

    private void Awake()
    {
        isInitialized = false;
        levelManager = GetComponent<LevelManager>();
        saveManager = GetComponent<SaveManager>();
    }

    public void Initialize(Player player, TileManager tileManager)
    {
        if (isInitialized == true)
            return;

        this.player = player;
        this.tileManager = tileManager;

        BindingEvents();

        isInitialized = true;
    }

    private void BindingEvents()
    {
        player.CanMoved += tileManager.OnCanMoved;
        player.MatchTheTile += OnMatchTheTile;
        tileManager.MissedTile += OnMissedTile;
        levelManager.CreateColorTile += tileManager.OnCreateColorTile;
        levelManager.CreatePasslessTile += tileManager.OnCreateImpassableTile;
    }

    public void OnGameStarted()
    {
        player.Ready();
        tileManager.Ready();
        levelManager.Ready();

        score = 0;
        bestScore = saveManager.LoadFromPlayerprefGetInt("Best");
        BestScoreChanged?.Invoke(bestScore);
        ScoreChanged?.Invoke(score);
        isGameover = false;
    }

    public void OnMissedTile()
    {
        if(score > bestScore)
        {
            saveManager.SaveToPlayerprefs("Best", score);
            BestScoreChanged?.Invoke(score);
        }

        isGameover = true;
        tileManager.End();
        player.End();
        GameEnded?.Invoke();
    }

    public void OnMatchTheTile()
    {
        if (isGameover == true)
            return;

        // 스코어 증가
        ScoreChanged?.Invoke(++score);

        // 난이도 조절
        levelManager.DifficultyControl();
    }
}