using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedirectionCube : MonoBehaviour
{
    [SerializeField] private Laser laser;
    private RedirectionCube lastCubeHit;

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
}
