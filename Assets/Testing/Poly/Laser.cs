using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;
    private RedirectionCube lastCubeHit;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lr.SetPosition(0, transform.position);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward);

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            lr.SetPosition(1, hit.point);
            if (hit.collider.TryGetComponent(out RedirectionCube Cube)) // If I hit a RCube
            {
                if (lastCubeHit != Cube) // If its a new RCube
                {
                    if (lastCubeHit != null)
                    {
                        lastCubeHit.beingHit = false;
                    }
                    Cube.beingHit = true;
                    Cube.Hit();
                    lastCubeHit = Cube;
                }
            }
            else if (lastCubeHit != null) //
            {
                lastCubeHit.beingHit = false;
                lastCubeHit.Hit();
                lastCubeHit = null;
            }

        }
        else // If I didn't hit a RCube
        {
            lastCubeHit.beingHit = false;
            lastCubeHit.Hit();
            lastCubeHit = null;
        }
    }
}
