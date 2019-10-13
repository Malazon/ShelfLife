using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoor : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GrabableObject GO) || other.tag == "Player")
        {
            if(GO.triggersButtons || other.tag == "Player")
            {
                Debug.Log("Button is Triggered");
            }           
        }

    }
}
