using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoor : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "ButtonTriggererBlock")
        {
            Debug.Log("Button is Triggered");
        }
    }
}
