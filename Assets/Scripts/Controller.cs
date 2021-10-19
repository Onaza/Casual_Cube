using System;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Action<Direction> Swiped;

    // 스와이프 거리
    private readonly float Distance = 10000.0f;

    private bool isGameStart;
    private Vector3 startPosition;
    private Vector3 direction;

    void Awake()
    {
        startPosition = Vector3.zero;
        direction = Vector3.zero;
        isGameStart = false;
    }
    void Update()
    {
        if (!isGameStart)
            return;

        if (Input.GetMouseButtonDown(0))
            startPosition = Input.mousePosition;
        else if (Input.GetMouseButton(0))
            direction = Input.mousePosition - startPosition;
        else if (Input.GetMouseButtonUp(0))
        {
            // 스와이프 거리가 짧으면 이동 하지 않는다
            if (direction.sqrMagnitude < Distance)
                return;

            if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
            {
                if (direction.y > 0)
                    Swiped?.Invoke(Direction.UP);
                else
                    Swiped?.Invoke(Direction.DOWN);
            }
            else
            {
                if (direction.x > 0)
                    Swiped?.Invoke(Direction.RIGHT);
                else
                    Swiped?.Invoke(Direction.LEFT);
            }
        }
    }

    public void OnGameStarted()
    {
        isGameStart = true;
    }

    public void OnGameEnded()
    {
        isGameStart = false;
    }
}