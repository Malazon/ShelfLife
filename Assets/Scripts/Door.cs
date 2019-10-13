using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform Hinge;

    [SerializeField] private AudioClip[] openAndCloseClips = new AudioClip[2];
    [SerializeField] private AudioSource doorSoundSource = null;

    private float lastTriggerTime = 0;

    public void Trigger()
    {
        lastTriggerTime = Time.time;
    }

    void Update()
    {
        if (Time.time - lastTriggerTime < 0.5f)
        {
            // Open?
            HandleDoorSounds(openDoor: true);

            Hinge.rotation = Quaternion.RotateTowards(Hinge.rotation, Quaternion.Euler(0, 90, 0), 720 * Time.deltaTime);
        }
        else
        {
            // Close?
            HandleDoorSounds(openDoor: false);

            Hinge.rotation = Quaternion.RotateTowards(Hinge.rotation, Quaternion.identity, 720 * Time.deltaTime);
        }
    }

    private void HandleDoorSounds(bool openDoor)
    {
        if (!doorSoundSource.isPlaying)
        {
            if (openDoor)
            {
                doorSoundSource.clip = openAndCloseClips[0];
                doorSoundSource.Play();
            }
            else
            {
                doorSoundSource.clip = openAndCloseClips[1];
                doorSoundSource.Play();
            }
        }
    }
}
