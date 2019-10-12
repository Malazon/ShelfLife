using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    [SerializeField] private Transform CameraLocation;

    private void OnTriggerEnter(Collider other)
    {
        if (CameraSingleton.Active is null) return;

        if (other.GetComponentInParent<PlayerSingleton>() != null)
            CameraSingleton.Active.TargetLocation = CameraLocation.transform;
    }
}