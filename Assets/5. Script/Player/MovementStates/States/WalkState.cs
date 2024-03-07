using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MovementBaseState
{
    // Start is called before the first frame update
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Walking", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (movement.dir.magnitude < 0.1f) ExitState(movement, movement.Idle);
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (movement.dir.magnitude < 0.1f)
            {
                ExitState(movement, movement.Idle);
            }
            else if (movement.currentState == movement.Walk)
            {
                ExitState(movement, movement.Crouch);
            }
            else if (movement.currentState == movement.Crouch)
            {
                if (movement.dir.magnitude >= 0.1f)
                    ExitState(movement, movement.Walk);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("걸으며 점프");
            movement.previousState = this;
            ExitState(movement, movement.Jump);
        }
        if (movement.vInput < 0) movement.currentMoveSpeed = movement.walkbackSpeed;
        else movement.currentMoveSpeed = movement.walkSpeed;
    }
    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.anim.SetBool("Walking", false);
        movement.SwitchState(state);
    }
}
