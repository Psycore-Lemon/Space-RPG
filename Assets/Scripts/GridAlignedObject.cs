using UnityEngine;

[ExecuteAlways]
public class GridAlignedObject : MonoBehaviour
{
    public float cellSize = 2f;
    public Vector2 offset = Vector2.zero;
    public bool snapInPlayMode = false;

    private Vector3 _lastPosition;

    void Update()
    {
        if (!Application.isPlaying || snapInPlayMode)
        {
            if (transform.position != _lastPosition)
            {
                SnapToGrid();
                _lastPosition = transform.position;
            }
        }
    }

    public void SnapToGrid()
    {
        Vector3 pos = transform.position;

        float snappedX = Mathf.Round((pos.x - offset.x) / cellSize) * cellSize + offset.x;
        float snappedY = Mathf.Round((pos.y - offset.y) / cellSize) * cellSize + offset.y;

        transform.position = new Vector3(snappedX, snappedY, transform.position.z);
    }

    void OnValidate()
    {
        SnapToGrid();
        _lastPosition = transform.position;
    }
}