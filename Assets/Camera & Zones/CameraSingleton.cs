using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSingleton : MonoBehaviour
{
    private static CameraSingleton _active;

    public static CameraSingleton Active => _active;

    [SerializeField] Camera camera;
    [SerializeField] private float moveRate = 5f;
    [SerializeField] private float turnRate = 720f;
    public Camera Camera => camera;

    public Transform TargetLocation;
    
    void Start()
    {

        if(_active != null)
        {
            if (_active.gameObject.activeInHierarchy)
            {
                Debug.LogWarning("Tried to initiate a second primary camera.");
                this.gameObject.SetActive(false);
                return;
            }
        }

        _active = this;
        TargetLocation = this.transform;
    }

    private void Update()
    {
        this.transform.position =
            Vector3.MoveTowards(this.transform.position, this.TargetLocation.position, moveRate * Time.deltaTime);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, this.TargetLocation.rotation,
            turnRate * Time.deltaTime);
    }
}
