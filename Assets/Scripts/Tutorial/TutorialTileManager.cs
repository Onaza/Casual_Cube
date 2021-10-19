using UnityEngine;

public class TutorialTileManager : MonoBehaviour
{
    private readonly int MaxRowTiles = 5;
    private readonly int MaxColTiles = 5;

    [SerializeField]
    private TutorialTile tilePrefab;

    private TutorialTile[,] tiles;
    private Sprite sprites;

    public void CreateTiles()
    {
        tiles = new TutorialTile[5, 5];

        for (int row = 0; row < 5; row++)
            for (int col = 0; col < 5; col++)
                tiles[row, col] = Instantiate(tilePrefab, new Vector3(row, col, 0.0f), Quaternion.identity, transform);
    }

    public void ShowTiles()
    {
        for (int row = 0; row < 5; row++)
            for (int col = 0; col < 5; col++)
                tiles[row, col].gameObject.SetActive(true);
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
                if (col - 1 >= 0)
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

    public void CreateColorTile(int row, int col)
    {
        tiles[row, col].SetColorTile();
    }

    public void CreateColorTileNoTimer(int row, int col)
    {
        tiles[row, col].SetColorTileNoTimer();
    }

    public void CreatePasslessTile(int row, int col)
    {
        tiles[row, col].SetImpassableTile();
    }

    public void ReleaseTile(int row, int col)
    {
        tiles[row, col].Release();
    }

    public void ReleaseTimestop(int row, int col)
    {
        tiles[row, col].ReleaseStop();
    }
}
