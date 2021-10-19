using System;
using UnityEngine;
public class TileManager : MonoBehaviour
{
    public Action MissedTile;

    private readonly int MaxRowTiles = 5;
    private readonly int MaxColTiles = 5;

    [SerializeField]
    private Tile tilePrefab;

    private Tile[,] tiles;
    private Sprite[] sprites;

    void Awake()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites/Tile");
        CreateTiles();
    }

    private void CreateTiles()
    {
        tiles = new Tile[MaxRowTiles, MaxColTiles];

        for (int row = 0; row < MaxRowTiles; row++)
            for (int col = 0; col < MaxColTiles; col++)
            {
                tiles[row, col] = Instantiate(tilePrefab, new Vector3(row, col, 0.0f), Quaternion.identity, transform);
                tiles[row, col].MissedTile += this.OnMissedTile;
                tiles[row, col].gameObject.SetActive(false);
            }
    }

    public void Ready()
    {
        // 시작 할 Tile 활성화
        for (int row = 0; row < MaxRowTiles; row++)
            for (int col = 0; col < MaxColTiles; col++)
                tiles[row, col].gameObject.SetActive(true);
    }

    public void End()
    {
        for (int row = 0; row < MaxRowTiles; row++)
            for (int col = 0; col < MaxColTiles; col++)
            {
                tiles[row, col].Release();
                tiles[row, col].gameObject.SetActive(false);
            }
    }

    public bool OnCanMoved(Transform transform, Direction direction)
    {
        int row = Mathf.RoundToInt(transform.position.x);
        int col = Mathf.RoundToInt(transform.position.y);

        switch (direction)
        {
            case Direction.UP:
                if (col + 1 < MaxColTiles)
                    return tiles[row, col + 1].canMove;
                break;
            case Direction.DOWN:
                if (col - 1  >= 0)
                    return tiles[row, col - 1].canMove;
                break;
            case Direction.RIGHT:
                if (row + 1 < MaxRowTiles)
                    return tiles[row + 1, col].canMove;
                break;
            case Direction.LEFT:
                if (row - 1 >= 0)
                    return tiles[row - 1, col].canMove;
                break;
        }

        return false;
    }

    public void OnCreateColorTile(int limitTime)
    {
        while (true)
        {
            int row = UnityEngine.Random.Range(0, MaxRowTiles);
            int col = UnityEngine.Random.Range(0, MaxColTiles);

            // 이동 불가 타일 일 경우, 다시 뽑는다
            if (tiles[row, col].canMove == false)
                continue;

            //이미 선택 된 타일 일 경우, 다시 뽑는다.
            if (tiles[row, col].isSelected == true)
                continue;

            // 타일 설정
            int type = UnityEngine.Random.Range(0, 6);
            tiles[row, col].SetColorTile(type, limitTime, sprites[type]);

            break;
        }
    }

    public void OnCreateImpassableTile()
    {
        while (true)
        {
            int row = UnityEngine.Random.Range(0, MaxRowTiles);
            int col = UnityEngine.Random.Range(0, MaxColTiles);

            // 이동 불가 타일 일 경우, 다시 뽑는다
            if (tiles[row, col].canMove == false)
                continue;

            //이미 선택 된 타일 일 경우, 다시 뽑는다.
            if (tiles[row, col].isSelected == true)
                continue;

            // 타일 설정
            tiles[row, col].SetImpassableTile();

            break;
        }
    }

    public void OnMissedTile()
    {
        MissedTile?.Invoke();
    }
}