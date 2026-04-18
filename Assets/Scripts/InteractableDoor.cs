using UnityEngine;

public class InteractableDoor : MonoBehaviour, IInteractable
{
    public Animator animator;

    public bool isOpen = false;
    public bool disableCollisionWhenOpen = true;

    void Reset()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Interact()
    {
        ToggleDoor();
    }

    [ContextMenu("Toggle Door")]
    public void ToggleDoor()
    {
        isOpen = !isOpen;
        animator.SetBool("Open", isOpen);

    }
}