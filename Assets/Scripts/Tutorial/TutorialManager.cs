using System;
using System.Collections;
using UnityEngine;
public class TutorialManager : MonoBehaviour
{
    public Action<Direction> Swiped;
    public Action TutorialEnded;

    private readonly float Distance = 10000.0f;

    [SerializeField]
    TutorialUIRoot uIRoot;

    [SerializeField]
    private TutorialPlayer player;
    [SerializeField]
    private TutorialTileManager tileManager;

    private Vector3 startPosition;
    private Vector3 direction;

    private Direction currentDirection;

    private bool isTutorialStarted;
    private bool isCorrectInput;

    private void Awake()
    {
        startPosition = Vector3.zero;
        direction = Vector3.zero;
        isTutorialStarted = false;

        tileManager.CreateTiles();
    }

    public void OnTutorialStarted()
    {
        if (isTutorialStarted == true)
            return;

        isTutorialStarted = true;

        tileManager.ShowTiles();

        player.CanMoved += tileManager.OnCanMoved;

        this.Swiped += player.OnSwiped;


        StartCoroutine(InputLoop());
        StartCoroutine(TutorialLoop());
    }

    private IEnumerator TutorialLoop()
    {
        yield return StartCoroutine(FirstStep());
        yield return StartCoroutine(SecondStep());
        yield return StartCoroutine(LastStep());
    }

    private IEnumerator FirstStep()
    {
        uIRoot.ShowExplaination("Hello,", "This is a tutorial.");

        yield return new WaitForSeconds(3.0f);

        uIRoot.ShowExplaination("Roll the cube", "to the red tile.");

        // player
        player.Ready();

        // Color Tile 생성
        tileManager.CreateColorTileNoTimer(2, 2);

        // 플레이어 이동 콜백 대기
        // ui표시 - Down Arrow
        uIRoot.ShowUpDownArrow(new Vector2(-290.0f, 160.0f), new Vector2(-290.0f, 15.0f));
        currentDirection = Direction.DOWN;
        isCorrectInput = false;
        yield return new WaitWhile(() => isCorrectInput == false);

        // ui표시 - Down Arrow
        uIRoot.ShowUpDownArrow(new Vector2(-290.0f, 0.0f), new Vector2(-290.0f, -125.0f));
        currentDirection = Direction.DOWN;
        isCorrectInput = false;
        yield return new WaitWhile(() => isCorrectInput == false);

        // ui표시 - Right Arrow
        uIRoot.ShowLeftToRightArrow(new Vector2(-290.0f, -100.0f), new Vector2(-140.0f, -100.0f));
        currentDirection = Direction.RIGHT;
        isCorrectInput = false;
        yield return new WaitWhile(() => isCorrectInput == false);

        // ui표시 - Right Arrow
        uIRoot.ShowLeftToRightArrow(new Vector2(-140.0f, -100.0f), new Vector2(0.0f, -100.0f));
        currentDirection = Direction.RIGHT;
        isCorrectInput = false;
        yield return new WaitWhile(() => isCorrectInput == false);

        // Hit Effect
        uIRoot.HideArrow();
        EffectManager.Instance.SetAndPlay(2, 2, 0);
        tileManager.ReleaseTile(2, 2);
        uIRoot.ShowExplaination("Score points by", "match the tiles.");
        currentDirection = Direction.NONE;
    }

    IEnumerator SecondStep()
    {
        // Passless 타일 생성
        tileManager.CreatePasslessTile(3, 2);
        tileManager.CreateColorTile(4, 2);

        // ui : 오른쪽 이동 표시
        uIRoot.ShowLeftToRightArrow(new Vector2(0.0f, -100.0f), new Vector2(70.0f, -100.0f));
        currentDirection = Direction.RIGHT;
        isCorrectInput = false;
        yield return new WaitWhile(() => isCorrectInput == false);

        // ui : 이동 못함 표시
        uIRoot.ShowExplaination("Dark gray tiles are", "impassable.");
        uIRoot.HideArrow();
        currentDirection = Direction.NONE;
        yield return new WaitForSeconds(3.0f);
    }

    IEnumerator LastStep()
    {
        uIRoot.ShowExplaination("Tiles will disappear", "after a time limit.");

        tileManager.ReleaseTimestop(3, 2);
        tileManager.ReleaseTimestop(4, 2);
        yield return new WaitForSeconds(3.0f);
        uIRoot.ShowExplaination("If you miss the tiles,", "you lose.");

        yield return new WaitForSeconds(4.0f);
        uIRoot.ShowExplaination("The game,", "will stat soon.");
        
        //튜토리얼 종료 및 게임 시작
        yield return new WaitForSeconds(2.0f);
        TutorialEnded?.Invoke();
        this.gameObject.SetActive(false);

        PlayerPrefs.SetInt("Tutorial", 1);
    }

    private bool CheckInput(Direction direction)
    {
        if (currentDirection == Direction.ALL)
            return true;

        if (currentDirection == direction)
            return true;

        return false;
    }

    private IEnumerator InputLoop()
    {
        while(isTutorialStarted == true)
        {
            yield return null;

            if (Input.GetMouseButtonDown(0))
                startPosition = Input.mousePosition;
            else if (Input.GetMouseButton(0))
                direction = Input.mousePosition - startPosition;
            else if (Input.GetMouseButtonUp(0))
            {
                // 스와이프 거리가 짧으면 이동 하지 않는다
                if (direction.sqrMagnitude < Distance)
                    continue;

                Direction dir;

                if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
                {
                    if (direction.y > 0)
                        dir = Direction.UP;
                    else
                        dir = Direction.DOWN;
                }
                else
                {
                    if (direction.x > 0)
                        dir = Direction.RIGHT;
                    else
                        dir = Direction.LEFT;
                }

                if (CheckInput(dir) == true)
                {
                    Swiped?.Invoke(dir);
                    isCorrectInput = true;
                }   
            }
        }
    }
}