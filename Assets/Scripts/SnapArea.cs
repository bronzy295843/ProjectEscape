using UnityEngine;

public class SnapArea : MonoBehaviour
{
    public float snapRange = 50f;

    public bool IsInSnapRange(Vector2 position)
    {
        float distance = Vector2.Distance(position, transform.localPosition);
        return distance < snapRange;
    }

    public Vector2 GetSnapPosition()
    {
        return transform.position;
    }
}

