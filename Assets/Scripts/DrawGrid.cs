using UnityEngine;
using UnityEngine.Tilemaps;

public class DrawGrid : MonoBehaviour
{
    public Tilemap tilemap;

    public int width = 20;
    public int height = 20;

    public float cellSize = 2f; // 48px if PPU = 24

    public Vector2Int offset = Vector2Int.zero;

    public Color gridColor = new Color(0, 1, 0, 0.3f);


    public Camera targetCamera;
    public int halfWidth = 20;
    public int halfHeight = 20;

    void OnDrawGizmos()
    {
        if (tilemap == null || targetCamera == null) return;

        Gizmos.color = gridColor;

        Vector3 camPos = targetCamera.transform.position;

        // Snap camera position to grid
        float startX = Mathf.Floor(camPos.x / cellSize) * cellSize;
        float startY = Mathf.Floor(camPos.y / cellSize) * cellSize;

        Vector3 origin = new Vector3(startX, startY, 0);

        // Apply offset
        origin += new Vector3(offset.x, offset.y, 0);

        // Draw vertical lines
        for (int x = -halfWidth; x <= halfWidth; x++)
        {
            Vector3 start = origin + new Vector3(x * cellSize, -halfHeight * cellSize, 0);
            Vector3 end = origin + new Vector3(x * cellSize, halfHeight * cellSize, 0);
            Gizmos.DrawLine(start, end);
        }

        // Draw horizontal lines
        for (int y = -halfHeight; y <= halfHeight; y++)
        {
            Vector3 start = origin + new Vector3(-halfWidth * cellSize, y * cellSize, 0);
            Vector3 end = origin + new Vector3(halfWidth * cellSize, y * cellSize, 0);
            Gizmos.DrawLine(start, end);
        }
    }
}
