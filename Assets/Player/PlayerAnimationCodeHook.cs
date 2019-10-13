using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerCharacterTrinket
{
    public GameObject trinket;
    public GameObject anchor;
    public Vector3 offset;
    public Vector3 eulerRotation;

    public void SetTrinketToAnchorPositionWithOffset()
    {
        trinket.transform.parent = anchor.transform;
        trinket.transform.localPosition = offset;
        trinket.transform.localRotation = Quaternion.Euler(eulerRotation);
    }
}

public class PlayerAnimationCodeHook : MonoBehaviour
{
    [SerializeField] Animator animator = null;
    [SerializeField] PlayerCharacterTrinket mainHand;

    private const string isDead_ParamName = "IsDead";
    private const string isFiring_ParamName = "IsFiring";
    private const string isCrouching_ParamName = "IsCrouching";
    private const string isMoving_ParamName = "IsMoving";
    private const string forward_ParamName = "Forward";
    private const string strafe_ParamName = "Strafe";

    private void Start()
    {
        if(mainHand.trinket && mainHand.anchor)
        {
            mainHand.SetTrinketToAnchorPositionWithOffset();
        }
    }

    /// <summary>
    /// This should be done as soon as you block rotation and translation of the player
    ///  after they've met any loss conditions for the game
    /// </summary>
    /// <param name="state"> true will set the state machine in motion to land in the Death state</param>
    public void SetDead(bool state)
    {
        animator.SetBool(isDead_ParamName, state);
    }

    public void SetFiring(bool state)
    {
        animator.SetBool(isFiring_ParamName, state);
    }

    public void SetCrouching(bool state)
    {
        animator.SetBool(isCrouching_ParamName, state);
    }

    public void SetMoving(bool state)
    {
        animator.SetBool(isMoving_ParamName, state);
    }

    public void SetForward(float value)
    {
        animator.SetFloat(forward_ParamName, value);
    }

    public void SetStrafe(float value)
    {
        animator.SetFloat(strafe_ParamName, value);
    }
}
