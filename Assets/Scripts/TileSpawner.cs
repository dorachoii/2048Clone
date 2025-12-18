using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject tilePrefab;


    void Start()
    {
        BoardManager.Instance.OnMoveEnd += SpawnRandomTile;

        for(int r = 0; r < BoardManager.row; r++)
        {
            for(int c = 0; c < BoardManager.col; c++)
            {
                BoardManager.Instance.board[r, c] = new Tile(0, new Vector2Int(r, c), null);
            }
        }
        SpawnTileAt(2, 2);
        SpawnTileAt(3, 3);
    }

    void SpawnTileAt(int row, int col)
    {
        Vector2 pos = BoardManager.Instance.CelltoWorld(row, col);
        GameObject tileObj = Instantiate(tilePrefab, pos, Quaternion.identity);
        
        // value값도 나중에 조정되어야 함
        Tile tile = new Tile(2, new Vector2Int(row, col), tileObj);
        BoardManager.Instance.board[row, col] = tile;
        Debug.Log($"생성 타일:{row},{col}");
    }

    public void SpawnRandomTile()
    {
        List<Vector2Int> emptyCells = new List<Vector2Int>();

        // instance가 아니라 class에 직접 접근하는게 맞는지
        for(int r = 0; r < BoardManager.row; r++)
        {
            for(int c = 0; c < BoardManager.col; c++)
            {
                if(BoardManager.Instance.board[r,c] == null || BoardManager.Instance.board[r,c].value != 0)
                {
                    emptyCells.Add(new Vector2Int(r,c));
                }
            }
        }

        if(emptyCells.Count == 0) return;

        Vector2Int chosenCell = emptyCells[Random.Range(0, emptyCells.Count)];

        SpawnTileAt(chosenCell.x, chosenCell.y);
    }
}
