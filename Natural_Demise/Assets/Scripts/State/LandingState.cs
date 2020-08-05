using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingState : BaseState
{
    public override Vector3 ProcessMotion (Vector3 input) {
        playerInputDirectionMagnitude = input.magnitude;
        
        ApplySpeed(ref input, motor.Speed);

        ApplyGravity(ref input, motor.Gravity);

        return input;
    }

    public override void Transition() {
        CheckIdleState();
        CheckWalkingState();
        CheckRunningState();
        CheckDyingState();
    }
}
