using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationCodeHook : MonoBehaviour
{
    [SerializeField] Animator animator = null;

    private const string isDead_ParamName = "IsDead";
    private const string isFiring_ParamName = "IsFiring";
    private const string isCrouching_ParamName = "IsCrouching";
    private const string isMoving_ParamName = "IsMoving";
    private const string forward_ParamName = "Forward";
    private const string strafe_ParamName = "Strafe";

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
