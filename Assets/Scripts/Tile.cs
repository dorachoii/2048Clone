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

        if (tileObject != null)
        {
            var text = tileObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            text.text = value.ToString();

            var sp = tileObject.GetComponentInChildren<SpriteRenderer>();
            var baseColor = sp.color;

            // 추후 수정
            float factor = 1f / Mathf.Log(value, 2);
            sp.color = new Color(
                baseColor.r * factor,
                baseColor.g * factor,
                baseColor.b * factor
            );
        }
    }
}
