using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        if (movement.previousState == movement.Idle) movement.anim.SetTrigger("IdleJump");
        else if (movement.previousState == movement.Walk) movement.anim.SetTrigger("WalkJump");
    }
    public override void UpdateState(MovementStateManager movement)
    {
        if (movement.jumped && movement.IsGrounded())
        {
            
            
            if (movement.hzInput == 0 && movement.vInput == 0)
            {
                
                movement.SwitchState(movement.Idle);
            }
            else
            {
                movement.SwitchState(movement.Walk);
            }
        }
    }
}
