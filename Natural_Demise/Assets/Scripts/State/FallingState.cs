using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : BaseState {
    public override void Construct() {
        base.Construct();

        print("hej");
        fallingTimer = Time.time;
    }

    public override Vector3 ProcessMotion (Vector3 input) {
        playerInputDirectionMagnitude = input.magnitude;
        
        ApplySpeed(ref input, motor.Speed);

        ApplyGravity(ref input, motor.Gravity);

        return input;
    }

    public override void Transition() {
        CheckLandingState();
        CheckDyingState();
    }
}
