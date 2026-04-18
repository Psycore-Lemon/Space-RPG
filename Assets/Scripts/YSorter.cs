using UnityEngine;

[ExecuteAlways]
public class YSorter : MonoBehaviour
{
    public int sortingOffset = 0;
    public float yMultiplier = 100f;

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (sr == null) return;

        sr.sortingOrder = (int)(-transform.position.y * yMultiplier) + sortingOffset;
    }
}
