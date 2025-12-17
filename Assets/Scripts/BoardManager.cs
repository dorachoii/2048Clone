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


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }




    public Vector2 CelltoWorld(int row, int col)
    {
        return new Vector2(leftX + col * cellSize, topY - row * cellSize);
    }

    public void MoveLeft()
    {
        for (int r = 0; r < row ; r++)
        {
            List<Tile> numbers = new List<Tile>();
            for(int c = 0; c < col; c++)
            {
                if(board[r,c] != null && board[r,c].value != 0)
                {
                    numbers.Add(board[r, c]);
                }
            }

            for(int i = 0; i < numbers.Count -1; i++)
            {
                if(numbers[i].value == numbers[i + 1].value)
                {
                    numbers[i].value *= 2;
                    Destroy(numbers[i+1].tileObject);
                    numbers[i].SetValue(numbers[i].value);
                    numbers.RemoveAt(i+1);
                }
            }

            int idx = 0;

            for(int c = 0; c < col; c++)
            {
                if(idx < numbers.Count)
                {
                    board[r, c] = numbers[idx];
                    board[r, c].MoveTo(new Vector2Int(r, c));
                    idx++;
                }else
                {
                    board[r, c] = null;
                }
            }
        }
    }

    public void MoveRight()
    {
        for (int r = 0; r < row ; r++)
        {
            List<Tile> numbers = new List<Tile>();
            for(int c = col -1; c >= 0; c--)
            {
                if(board[r,c] != null && board[r,c].value != 0)
                {
                    numbers.Add(board[r, c]);
                }
            }

            for(int i = 0; i < numbers.Count -1; i++)
            {
                if(numbers[i].value == numbers[i + 1].value)
                {
                    numbers[i].value *= 2;
                    Destroy(numbers[i+1].tileObject);
                    numbers[i].SetValue(numbers[i].value);
                    numbers.RemoveAt(i+1);
                }
            }

            int idx = 0;

            for(int c = col -1; c >= 0; c--)
            {
                if(idx < numbers.Count )
                {
                    board[r, c] = numbers[idx];
                    board[r, c].MoveTo(new Vector2Int(r, c));
                    idx++;
                }else
                {
                    board[r, c] = null;
                }
            }
        }
    }
    
    public void MoveUp()
    {
        for (int c = 0; c < col ; c++)
        {
            List<Tile> numbers = new List<Tile>();
            for(int r = 0; r < row; r++)
            {
                if(board[r,c] != null && board[r,c].value != 0)
                {
                    numbers.Add(board[r, c]);
                }
            }

            for(int i = 0; i < numbers.Count -1; i++)
            {
                if(numbers[i].value == numbers[i + 1].value)
                {
                    numbers[i].value *= 2;
                    Destroy(numbers[i+1].tileObject);
                    numbers[i].SetValue(numbers[i].value);
                    numbers.RemoveAt(i+1);
                }
            }

            int idx = 0;

            for(int r = 0; r < row; r++)
            {
                if(idx < numbers.Count)
                {
                    board[r, c] = numbers[idx];
                    board[r, c].MoveTo(new Vector2Int(r, c));
                    idx++;
                }else
                {
                    board[r, c] = null;
                }
            }
        }
    }
    
    public void MoveDown()
    {
        for (int c = 0; c < col ; c++)
        {
            List<Tile> numbers = new List<Tile>();
            for(int r = row-1; r >= 0; r--)
            {
                if(board[r,c] != null && board[r,c].value != 0)
                {
                    numbers.Add(board[r, c]);
                }
            }

            int idx = 0;

            for(int r = row-1; r >= 0; r--)
            {
                if(idx < numbers.Count)
                {
                    board[r, c] = numbers[idx];
                    board[r, c].MoveTo(new Vector2Int(r, c));
                    idx++;
                }else
                {
                    board[r, c] = null;
                }
            }
        }
    }
}
