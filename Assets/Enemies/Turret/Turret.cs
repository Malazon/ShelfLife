using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] private GameObject _turretTop;

    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private ParticleSystem _particleSystem;

    [SerializeField] private float _sweepRate = 15f;

    private float sweepDirection = 1;
    
    private float lastSeen = -1000;

    private void Sweep()
    {
        float currentAngle = _turretTop.transform.localRotation.eulerAngles.y;

        if (sweepDirection == -1)
        {
            if (currentAngle > 180 && currentAngle < 345)
            {
                sweepDirection = 1;
            }
        }
        else
        {
            if (currentAngle < 180 && currentAngle > 15)
            {
                sweepDirection = -1;
            }
        }     
        
        _turretTop.transform.Rotate(Vector3.up, Time.deltaTime * _sweepRate * sweepDirection);
    }

    private void Track()
    {
        if (PlayerSingleton.Active == null) return;

        var targetDirection =
            (PlayerSingleton.Active.transform.position - _turretTop.transform.position).ProjectOntoPlane(Vector3.up);

        var desiredAngle = Quaternion.LookRotation(targetDirection).eulerAngles.y + 90;

        var localTransform = _turretTop.transform.localRotation.eulerAngles.y -
                             _turretTop.transform.rotation.eulerAngles.y;

        var newAngle = desiredAngle + localTransform;

        
        if (newAngle < 0)
        {
            newAngle += 360;
        } else if (newAngle > 360)
        {
            newAngle -= 360;
        }
        
        if (newAngle < 15 || newAngle > 345)
        {
            _turretTop.transform.rotation = Quaternion.Euler(0, newAngle, 0);
        }
    }

    private void TryFire()
    {
        var direction = _turretTop.transform.rotation * Vector3.left;
        var origin = _particleSystem.transform.position;

        if (Physics.Raycast( origin, direction, out RaycastHit hit, 1000))
        {
            _lineRenderer.SetPositions(new []{origin, hit.point});

            if (hit.collider.CompareTag("Player"))
            {
                lastSeen = Time.time;
                PlayerSingleton.Active.Combatant.DoDamage(50 * Time.deltaTime);
            }
        } 
        else
        {
            _lineRenderer.SetPositions(new []{origin, origin + direction * 100});
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSeen > 5)
        {
            _particleSystem.Stop();     
            Sweep();
        }
        else
        {      
            _particleSystem.Play();
            Track();
        }
        
        TryFire();
    }
}
