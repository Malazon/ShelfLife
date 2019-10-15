using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardReciever : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GrabableObject GO))
        {
            if (GO.isKeyCard)
            {
                Debug.Log("KeyCard Accepted");
                GO.DestroyThis();
            }
        }

    }
}
