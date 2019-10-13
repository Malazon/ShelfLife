using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class RedirectionCube : MonoBehaviour
{
    [SerializeField] private Laser laser;

    public void StartBeingHit()
    {
        if (!laser.isActiveAndEnabled)
        {
            laser.gameObject.SetActive(true);
        }
    }

    public void StopBeingHit()
    {
        if (laser.isActiveAndEnabled)
        {
            laser.gameObject.SetActive(false);
        }
    }

    public void FixedUpdate()
    {
        transform.rotation =
            Quaternion.LookRotation((transform.rotation * Vector3.forward).ProjectOntoPlane(Vector3.up));
    }
}
