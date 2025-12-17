using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject tilePrefab;


    void Start()
    {
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
        
        Tile tile = new Tile(2, new Vector2Int(row, col), tileObj);
        BoardManager.Instance.board[row, col] = tile;
    }
}
