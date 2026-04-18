using UnityEngine;
using UnityEngine.Windows;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 1.0f;
    public LayerMask interactLayer;

    private PlayerInputHandler input;
    private GridPlayerMovement movement;

    void Awake()
    {
        movement = GetComponent<GridPlayerMovement>();
        input = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        DrawDebugRay();
        if (input.InteractPressedThisFrame)
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        
        Vector2 origin = transform.position;
        Vector2 direction = movement.direction;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, interactDistance, interactLayer);

        if (hit.collider != null)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    void DrawDebugRay()
    {
        if (movement == null) return;

        Vector3 dir = new Vector3(movement.direction.x, movement.direction.y, 0f);
        Debug.DrawRay(transform.position, dir * interactDistance, Color.green);
    }

    void OnDrawGizmosSelected()
    {
        GridPlayerMovement move = GetComponent<GridPlayerMovement>();
        if (move == null) return;

        Vector3 dir = new Vector3(move.direction.x, move.direction.y, 0f);
        Vector3 origin = transform.position;
        Vector3 end = origin + dir * interactDistance;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, end);
        Gizmos.DrawSphere(end, 0.1f);
    }


}
