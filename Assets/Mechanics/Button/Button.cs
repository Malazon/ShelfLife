using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Door door;
    
    private void OnTriggerStay(Collider other)
    {
        if (door == null) return;
        
        if (other.TryGetComponent(out GrabableObject GO))
        {
            if(GO.triggersButtons)
            {
                Debug.Log("press");
                door.Trigger();
            }           
        } else if (other.tag == "Player")
        {
            Debug.Log("press");
            door.Trigger();
        }
    }
}
