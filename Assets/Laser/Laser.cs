using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    private RedirectionCube _lastCubeHit;

    private void OnDisable()
    {
        if (_lastCubeHit != null)
        {
            _lastCubeHit.StopBeingHit();
            _lastCubeHit = null;
        }
    }
    
    void Update()
    {
        Vector3 startPosition = this.transform.position;
        Vector3 direction = this.transform.forward;
        Vector3 endPosition = direction * 1000 + startPosition;

        if (Physics.Raycast(startPosition, direction, out RaycastHit hit))
        {

            endPosition = hit.point;
            
            // Check for cube / collision
            if (hit.collider.TryGetComponent(out RedirectionCube Cube))
            {
                if (_lastCubeHit != null && _lastCubeHit != Cube)
                {
                    _lastCubeHit.StopBeingHit();
                }
                
                Cube.StartBeingHit();
                _lastCubeHit = Cube;
            } else if (_lastCubeHit != null)
            {
                _lastCubeHit.StopBeingHit();
                _lastCubeHit = null;
            }
            
            // Check for trigger
            if (hit.collider.TryGetComponent(out LaserTrigger Trigger))
            {
                Trigger.Hit();
            }
            
            // Check for combatant
            if (hit.collider.TryGetComponent(out Combatant combatant))
            {
                combatant.DoDamage(Time.deltaTime * 100);
            }

        }
        else if (_lastCubeHit != null)
        {
            _lastCubeHit.StopBeingHit();
            _lastCubeHit = null;
        }
        
        lineRenderer.SetPositions(new []{startPosition, endPosition});
    }
}
