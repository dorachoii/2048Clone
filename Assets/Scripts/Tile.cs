using UnityEngine;

public class Tile
{
    public int value;
    public Vector2Int cellPos;
    
    public GameObject tileObject;

    public Tile(int val, Vector2Int cPos, GameObject obj)
    {
        value = val;
        cellPos = cPos;
        tileObject = obj;
    }

    public void MoveTo(Vector2Int newCellPos)
    {
        cellPos = newCellPos;
        Vector2 newPos = BoardManager.Instance.CelltoWorld(newCellPos.x, newCellPos.y);
        TileAnimator animator = tileObject.GetComponent<TileAnimator>();
        animator.Move(newPos, 0.2f);
    }

    public void SetValue(int newValue)
    {
        value = newValue;

        if(tileObject != null)
        {
            var text = tileObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if(text != null)
            {
                text.text = value.ToString();
            }
        }
    }
}
