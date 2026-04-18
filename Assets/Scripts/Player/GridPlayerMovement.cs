using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class GridPlayerMovement : MonoBehaviour
{
    public Tilemap wallTilemap;
    public Tilemap objectTilemap;
    public float moveSpeed = 4f;

    public Animator _animator;

    private PlayerInputHandler controls;
    private Vector2 moveInput;

    private Vector2Int currentLogicCell;
    private Vector3 targetWorldPos;
    private bool isMoving = false;

    public Vector2Int direction = new Vector2Int(0, -1);

    private const float LOGIC_CELL_SIZE = 2f; // 48px if PPU = 24

    void Awake()
    {
        controls = GetComponent<PlayerInputHandler>();
    }



    void Start()
    {
        currentLogicCell = WorldToLogicCell(transform.position);
        targetWorldPos = LogicCellToWorld(currentLogicCell);
        transform.position = targetWorldPos;
    }

    void Update()
    {
        _animator.SetBool("IsWalking", isMoving);
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetWorldPos,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, targetWorldPos) < 0.01f)
            {
                transform.position = targetWorldPos;
                isMoving = false;
            }

            return;
        }
        moveInput = controls.MoveInput;
        TryMove();
    }


    void TryMove()
    {
        Vector2Int dir = Vector2Int.zero;

        if (moveInput.x > 0.5f) dir = Vector2Int.right;
        else if (moveInput.x < -0.5f) dir = Vector2Int.left;
        else if (moveInput.y > 0.5f) dir = Vector2Int.up;
        else if (moveInput.y < -0.5f) dir = Vector2Int.down;
        else return;

        Vector2Int targetLogicCell = currentLogicCell + dir;
        _animator.SetFloat("MoveX", dir.x);
        _animator.SetFloat("MoveY", dir.y);

        direction = dir;
        if (IsBlocked(targetLogicCell))
            return;

        
        currentLogicCell = targetLogicCell;
        targetWorldPos = LogicCellToWorld(currentLogicCell);
        isMoving = true;
        //moveInput = Vector2.zero;
    }

    Vector3 LogicCellToWorld(Vector2Int cell)
    {
        return new Vector3(
            cell.x * LOGIC_CELL_SIZE + LOGIC_CELL_SIZE * 0.5f,
            cell.y * LOGIC_CELL_SIZE + LOGIC_CELL_SIZE * 0.5f,
            0f
        );
    }

    Vector2Int WorldToLogicCell(Vector3 worldPos)
    {
        return new Vector2Int(
            Mathf.FloorToInt(worldPos.x / LOGIC_CELL_SIZE),
            Mathf.FloorToInt(worldPos.y / LOGIC_CELL_SIZE)
        );
    }

    bool IsBlocked(Vector2Int logicCell)
    {
        // Convert the 48x48 logic cell into the 4 smaller 24x24 tilemap cells it covers
        int baseX = logicCell.x * 2;
        int baseY = logicCell.y * 2;

        Vector3Int[] coveredCells =
        {
            new Vector3Int(baseX,     baseY,     0),
            new Vector3Int(baseX + 1, baseY,     0),
            new Vector3Int(baseX,     baseY + 1, 0),
            new Vector3Int(baseX + 1, baseY + 1, 0)
        };

        foreach (var cell in coveredCells)
        {
            if (wallTilemap.HasTile(cell) || objectTilemap.HasTile(cell))
                return true;
        }

        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        float cellSize = 2f; // 48px if PPU = 24

        // Get current logic cell
        Vector2Int logicCell = WorldToLogicCell(transform.position);

        // Get center of that cell
        Vector3 center = new Vector3(
            logicCell.x * cellSize + cellSize * 0.5f,
            logicCell.y * cellSize + cellSize * 0.5f,
            0
        );

        // Draw the cell box
        Gizmos.DrawWireCube(center, new Vector3(cellSize, cellSize, 0));

        // Optional: draw center point
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(center, 0.05f);
    }

}
