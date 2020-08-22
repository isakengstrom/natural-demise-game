using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : BaseState
{
    public override void Construct() {
        base.Construct();

        motor.VerticalVelocity = 0f;
    }

    public override Vector3 ProcessMotion(Vector3 input) {
        playerInputDirectionMagnitude = input.magnitude;
        ApplySpeed(ref input, motor.Speed * 1.5f); //in baseState

        return input;
    }
   
    public override void Transition() {
        CheckIdleState();
        CheckWalkingState();
        CheckFallingState();
        CheckJumpingState();
        CheckDyingState();
    }
}