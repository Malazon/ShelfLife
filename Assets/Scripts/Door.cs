using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] public bool Open;
    [SerializeField] private Transform Hinge;

    void Update()
    {
        if (Open)
        {
            Hinge.rotation = Quaternion.RotateTowards(Hinge.rotation, Quaternion.Euler(0, 90, 0), 720 * Time.deltaTime);
        }
        else
        {
            Hinge.rotation = Quaternion.RotateTowards(Hinge.rotation, Quaternion.identity, 720 * Time.deltaTime);
        }
    }
}
