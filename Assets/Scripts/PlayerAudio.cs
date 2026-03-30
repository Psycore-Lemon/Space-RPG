using UnityEngine;

public class PlayerAudio : MonoBehaviour
{

    public AK.Wwise.Event footstepEvent;

    public void PlayFootstep()
    {
        footstepEvent.Post(gameObject);
    }
}
