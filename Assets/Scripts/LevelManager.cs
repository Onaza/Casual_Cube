using System;
using UnityEngine;
public class LevelManager : MonoBehaviour
{
    private readonly int NormalTimeLimit = 30;
    private readonly int HardTimeLimit = 20;

    public Action<int> CreateColorTile;
    public Action CreatePasslessTile;

    private int weight;
    private int currentColorTileCount;

    public void Ready()
    {
        weight = 0;
        currentColorTileCount = 1;
        CreateColorTile?.Invoke(NormalTimeLimit);
    }

    public void DifficultyControl()
    {
        // 난이도 생성 구간
        // ---------------------------------
        // |   구간   |   제한 시간  | 타일 |
        // | 1 ~ 10   |      30s     |   1  |    
        // | 10 ~ 17  |    30s 20s   |   1  |
        // | 17 ~ 20  |    20s 20s   |   2  |
        // ---------------------------------
        currentColorTileCount--;

        // 가중치 초기화
        if (weight >= 30)
            weight = 0;

        weight += UnityEngine.Random.Range(0, 10);

        if (weight < 11)
        {
            // 1 ~ 10 : 10
            if(currentColorTileCount < 3)
            {
                CreateColorTile?.Invoke(NormalTimeLimit);
                currentColorTileCount++;
            }

            weight += 3;
        }
        else if (10 < weight && weight < 18)
        {
            // 10 ~ 17 : 7
            if (currentColorTileCount < 2)
            {
                CreateColorTile?.Invoke(NormalTimeLimit);
                CreateColorTile?.Invoke(HardTimeLimit);
                currentColorTileCount += 2;
            }
            else if ( 1 < currentColorTileCount )
            {
                CreateColorTile?.Invoke(HardTimeLimit);
                currentColorTileCount++;
            }

            if(13 < weight)
                CreatePasslessTile?.Invoke();

            weight += 2;
        }
        else
        {
            // 18 ~ 20 : 3
            if (currentColorTileCount < 2)
            {
                CreateColorTile?.Invoke(HardTimeLimit);
                CreateColorTile?.Invoke(HardTimeLimit);
                currentColorTileCount += 2;
            }
            else if (1 < currentColorTileCount)
            {
                CreateColorTile?.Invoke(HardTimeLimit);
                currentColorTileCount++;
            }
            CreatePasslessTile?.Invoke();

            weight++;
        }
    }
}