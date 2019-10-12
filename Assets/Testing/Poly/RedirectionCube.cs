using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedirectionCube : MonoBehaviour
{
    private LineRenderer lr;

    public bool beingHit;

    private RedirectionCube lastCubeHit;

    public void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void Hit ()
    {
        if (beingHit)
        {
            lr.SetPosition(0, transform.position);
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward);
            Debug.Log("Redirecting");

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                lr.SetPosition(1, hit.point);
                if (hit.collider.TryGetComponent(out RedirectionCube Cube))
                {
                    if (lastCubeHit != Cube)
                    {
                        if (lastCubeHit != null)
                        {
                            lastCubeHit.beingHit = false;
                        }

                        Cube.Hit();
                        Cube.beingHit = true;
                        lastCubeHit = Cube;
                    }
                }
                else if (lastCubeHit != null)
                {
                    lastCubeHit.beingHit = false;
                    lastCubeHit = null;
                }
            }
            else
            {
                lastCubeHit.beingHit = false;
                lastCubeHit = null;
            }
        }
        
    }

}
