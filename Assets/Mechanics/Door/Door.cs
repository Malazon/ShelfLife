using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    enum DoorState
    {
        Closed,
        Ajar,
        Open
    };

    [SerializeField] private Transform Hinge;

    [SerializeField] private AudioClip[] openAndCloseClips = new AudioClip[2];
    [SerializeField] private AudioSource doorSoundSource = null;

    private float lastTriggerTime = 0;
    private DoorState myState = DoorState.Closed;
    

    public void Trigger()
    {
        lastTriggerTime = Time.time;

        if(myState == DoorState.Closed)
        {
            myState = DoorState.Ajar;
        }
    }

    void Update()
    {
        UpdateDoorStateMachine();
    }

    private void UpdateDoorStateMachine()
    {
        switch (myState)
        {
            case DoorState.Open:
                {
                    // too long?  back to ajar!
                    if (Time.time - lastTriggerTime > 0.5f)
                    {
                        myState = DoorState.Ajar;
                    }
                }
                break;
            case DoorState.Ajar:
                {
                    if (Time.time - lastTriggerTime < 0.5f)
                    {
                        Quaternion targetQuat = Quaternion.Euler(0, 90, 0);
                        // Open
                        Hinge.localRotation = Quaternion.RotateTowards(Hinge.localRotation, targetQuat, 720 * Time.deltaTime);

                        if (Mathf.Approximately(Quaternion.Angle(Hinge.localRotation, targetQuat), 0f))
                        {
                            ChangeState(DoorState.Open);
                        }
                    }
                    else
                    {
                        // Close
                        Hinge.localRotation = Quaternion.RotateTowards(Hinge.localRotation, Quaternion.identity, 720 * Time.deltaTime);

                        if (Mathf.Approximately(Quaternion.Angle(Hinge.localRotation, Quaternion.identity), 0f))
                        {
                            ChangeState(DoorState.Closed);
                        }
                    }
                }
                break;
            case DoorState.Closed:
                {
                    if (Time.time - lastTriggerTime < 0.5f)
                    {
                        ChangeState(DoorState.Ajar);
                    }
                }
                break;
            default: break;
        }
        switch (myState)
        {
            case DoorState.Open:
                {
                    // too long?  back to ajar!
                    if (Time.time - lastTriggerTime > 0.5f)
                    {
                        myState = DoorState.Ajar;
                    }
                }
                break;
            case DoorState.Ajar:
                {
                    if (Time.time - lastTriggerTime < 0.5f)
                    {
                        Quaternion targetQuat = Quaternion.Euler(0, 90, 0);
                        // Open
                        Hinge.localRotation = Quaternion.RotateTowards(Hinge.localRotation, targetQuat, 720 * Time.deltaTime);

                        if (Mathf.Approximately(Quaternion.Angle(Hinge.localRotation, targetQuat), 0f))
                        {
                            ChangeState(DoorState.Open);
                        }
                    }
                    else
                    {
                        // Close
                        Hinge.localRotation = Quaternion.RotateTowards(Hinge.localRotation, Quaternion.identity, 720 * Time.deltaTime);

                        if (Mathf.Approximately(Quaternion.Angle(Hinge.localRotation, Quaternion.identity), 0f))
                        {
                            ChangeState(DoorState.Closed);
                        }
                    }
                }
                break;
            case DoorState.Closed:
                {
                    if (Time.time - lastTriggerTime < 0.5f)
                    {
                        ChangeState(DoorState.Ajar);
                    }
                }
                break;
            default: break;
        }

    }

    private void ChangeState(DoorState newState, bool silent = false)
    {
        myState = newState;

        if (!silent)
        {
            switch (newState)
            {
                case DoorState.Open:
                    {
                        HandleDoorSounds(openDoor: true);
                    }
                    break;
                case DoorState.Closed:
                    {
                        HandleDoorSounds(openDoor: false);
                    }
                    break;
            }
        }
    }

    private void HandleDoorSounds(bool openDoor)
    {
        if (doorSoundSource != null && !doorSoundSource.isPlaying)
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
