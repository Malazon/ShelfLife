using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform Hinge;

    private float lastTriggerTime = 0;
    
    public void Trigger()
    {
        lastTriggerTime = Time.time;
    }    
    
    void Update()
    {
        if (Time.time - lastTriggerTime < 0.5f)
        {
            Hinge.rotation = Quaternion.RotateTowards(Hinge.rotation, Quaternion.Euler(0, 90, 0), 720 * Time.deltaTime);
        }
        else
        {
            Hinge.rotation = Quaternion.RotateTowards(Hinge.rotation, Quaternion.identity, 720 * Time.deltaTime);
        }
    }
}
