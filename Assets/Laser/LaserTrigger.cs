using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrigger : MonoBehaviour
{
    [SerializeField] private Door _door;
    
    public void Hit()
    {
        lastHit = Time.time;
        if (_door != null)
        {
            _door.Open = true;
        }
    }

    private float lastHit = 0;
    
    public void Update() {
        if (Time.time - lastHit > 1)
        {
            if (_door != null)
            {
                _door.Open = false;
            }
        }
    }
}
