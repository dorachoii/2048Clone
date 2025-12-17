using UnityEngine;
using System.Collections;

public class TileAnimator : MonoBehaviour
{
    public void Move(Vector2 targetPos, float duration)
    {
        StartCoroutine(IMove(targetPos, duration));
    }
    
    private IEnumerator IMove(Vector2 targetPos, float duration)
    {
        Vector2 startPos = transform.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector2.Lerp(startPos, targetPos,t);
            yield return null;
        }
        transform.position = targetPos;
    }
}
