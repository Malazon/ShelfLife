using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseWorldPosition : MonoBehaviour
{
    private Vector3? _position;
    private int _frame;
    
    public Vector3? Position
    {
        get
        {
            if (_frame != Time.frameCount)
            {
                updatePosition();
            }

            return _position;
        }
    }

    void updatePosition()
    {
        _frame = Time.frameCount;
        
        
        if (CameraSingleton.Active == null) return;

        var camera = CameraSingleton.Active;

        var mouseRay = camera.Camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(mouseRay.origin, mouseRay.direction * 100f);

        RaycastHit raycastHit;

        if (Physics.Raycast(mouseRay, out raycastHit, 100, LayerMask.GetMask("Floor")))
        {
            _position = raycastHit.point;
        }
    }
}
