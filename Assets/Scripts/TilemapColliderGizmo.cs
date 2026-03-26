using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapColliderGizmo : MonoBehaviour
{
    public Tilemap collisionTilemap;

    public Color colliderColor = new Color(1, 0, 0, 0.3f);

    void OnDrawGizmos()
    {
        if (collisionTilemap == null) return;

        Gizmos.color = colliderColor;

        BoundsInt bounds = collisionTilemap.cellBounds;

        foreach (var pos in bounds.allPositionsWithin)
        {
            if (!collisionTilemap.HasTile(pos))
                continue;

            Vector3 center = collisionTilemap.GetCellCenterWorld(pos);

            Gizmos.DrawWireCube(center, Vector3.one);

            // Optional: filled version
            Gizmos.color = new Color(1, 0, 0, 0.1f);
            Gizmos.DrawCube(center, Vector3.one);

            Gizmos.color = colliderColor;
        }
    }
}
