using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ZoneTrigger : MonoBehaviour
{
    [SerializeField] private Transform CameraLocation;
    [SerializeField] private GameObject LightsParent;

    private List<Light> lights;
    private static List<Light> allLights;
    
    private void Start()
    {
        if (allLights == null)
        {
            allLights = new List<Light>();
        }
        
        lights = new List<Light>();
        
        if (LightsParent == null) return;
        
        lights.AddRange(LightsParent.GetComponentsInChildren<Light>());
        allLights.AddRange(lights);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (CameraSingleton.Active == null) return;

        if (other.GetComponentInParent<PlayerSingleton>() != null)
            CameraSingleton.Active.TargetLocation = CameraLocation.transform;
        
        allLights = allLights.Where(l => l != null).ToList();

        foreach (var l in allLights.Except(lights))
        {
            l.gameObject.SetActive(false);
        }

        foreach (var l in lights)
        {
            l.gameObject.SetActive(true);
        }
    }
}