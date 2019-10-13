using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrigger : MonoBehaviour
{
    [SerializeField] private Door _door;
    
    public void Hit()
    {
        if (_door != null)
        {
            _door.Trigger();
        }
    }
}
