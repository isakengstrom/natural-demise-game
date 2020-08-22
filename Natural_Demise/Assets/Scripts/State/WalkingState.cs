using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : BaseState
{
    public override void Construct() {
        base.Construct();

        motor.VerticalVelocity = 0f;
    }

    public override Vector3 ProcessMotion(Vector3 input) {
        playerInputDirectionMagnitude = input.magnitude;
        ApplySpeed(ref input, motor.Speed); //in baseState

        return input;
    }
   
    public override void Transition() {
        CheckIdleState();
        CheckRunningState();
        CheckFallingState();
        CheckJumpingState();
        CheckDyingState();
    }
}
