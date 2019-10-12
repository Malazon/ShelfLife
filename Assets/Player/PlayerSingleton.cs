using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    private static PlayerSingleton _active;

    public static PlayerSingleton Active => _active;

    [SerializeField] private Rigidbody rigidBody;

    public Rigidbody RigidBody => rigidBody;

    void Start()
    {
        
        if(_active != null)
        {
            if (_active.gameObject.activeInHierarchy)
            {
                Debug.LogWarning("Tried to initiate an additional player.");
                this.gameObject.SetActive(false);
                return;
            }
        }
        
        _active = this;
    }
}
