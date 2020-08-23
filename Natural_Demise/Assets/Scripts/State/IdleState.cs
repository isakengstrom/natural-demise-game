using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState {


    public override void Construct() {
        base.Construct();

        motor.VerticalVelocity = 0f;
    }

    public override Vector3 ProcessMotion(Vector3 input) {
        playerInputDirectionMagnitude = input.magnitude; 

        ApplySpeed(ref input, 0); //in baseState

        return input;
    }

    public override void Transition() {
        CheckFallingState();
        CheckWalkingState();
        CheckRunningState();
        CheckJumpingState();
        CheckDyingState();
    }
}
