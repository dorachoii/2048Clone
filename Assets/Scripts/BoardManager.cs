using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    public const int row = 4;
    public const int col = 4;
    private const float cellSize = 1f;

    private float leftX = -1.5f;
    private float topY = 1.5f;


    public Tile[,] board = new Tile[row, col];

    public delegate void MoveEndDelegate();
    public event MoveEndDelegate OnMoveEnd;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    // 배열 좌표 -> 월드좌표
    public Vector2 CelltoWorld(int row, int col)
    {
        return new Vector2(leftX + col * cellSize, topY - row * cellSize);
    }

    public void MoveRight() => MoveHorizontal(Direction.Right);
    public void MoveLeft() => MoveHorizontal(Direction.Left);
    public void MoveUp() => MoveVertical(Direction.Up);
    public void MoveDown() => MoveVertical(Direction.Down);

    void MoveHorizontal(Direction dir)
    {
        bool moved = false;

        // 위에서 아래로 탐색
        for (int r = 0; r < row; r++)
        {
            List<Tile> numbers = new List<Tile>();

            int start = (dir == Direction.Left) ? 0 : col - 1;
            int end = (dir == Direction.Left) ? col : -1;
            int step = (dir == Direction.Left) ? 1 : -1;

            for (int c = start; c != end; c += step)
            {
                if (board[r, c] != null && board[r, c].value != 0)
                {
                    Debug.Log($"현재 보드의 타일:{r},{c}");
                    numbers.Add(board[r, c]);
                }
            }

            numbers = Merge(numbers);
            

            int idx = 0;
            for (int c = start; c != end; c += step)
            {
                if (idx < numbers.Count)
                {
                    board[r, c] = numbers[idx];
                    board[r, c].MoveTo(new Vector2Int(r, c));
                    idx++;
                }
                else
                {
                    board[r, c] = null;
                }
            }
        }

        Debug.Log("OnMoveEnd");
    }

    void MoveVertical(Direction dir)
    {
        // 왼쪽에서 오른쪽으로 탐색
        for (int c = 0; c < col; c++)
        {
            List<Tile> numbers = new List<Tile>();

            int start = (dir == Direction.Up) ? 0 : row - 1;
            int end = (dir == Direction.Up) ? row : -1;
            int step = (dir == Direction.Up) ? 1 : -1;

            for (int r = start; r != end; r += step)
            {
                if (board[r, c] != null && board[r, c].value != 0)
                {
                    numbers.Add(board[r, c]);
                }
            }

            numbers = Merge(numbers);

            int idx = 0;
            for (int r = start; r != end; r += step)
            {
                if (idx < numbers.Count)
                {
                    board[r, c] = numbers[idx];
                    board[r, c].MoveTo(new Vector2Int(r, c));
                    idx++;
                }
                else
                {
                    board[r, c] = null;
                }
            }
        }
        OnMoveEnd?.Invoke();
    }

    List<Tile> Merge(List<Tile> numbers)
    {
        for (int i = 0; i < numbers.Count - 1; i++)
        {
            if (numbers[i].value == numbers[i + 1].value)
            {
                numbers[i].value *= 2;
                Destroy(numbers[i + 1].tileObject);
                numbers[i].SetValue(numbers[i].value);
                numbers.RemoveAt(i + 1);
            }
        }
        return numbers;
    }
}
