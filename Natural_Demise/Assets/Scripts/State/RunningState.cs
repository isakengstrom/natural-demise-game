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
        //Debug.Log(input);
        ApplySpeed(ref input, motor.Speed * 1.5f); //in baseState

        return input;
    }
    /*
    public override Quaternion ProcessRotation(Vector3 input) {
        //lastRotation = Quaternion.FromToRotation(Vector3.forward, input);
        //return Quaternion.FromToRotation(Vector3.forward, input);
        return Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(input.normalized), 0.35f);
    }
    */
    public override void Transition() {
        CheckIdleState();
        CheckWalkingState();
        CheckFallingState();
        CheckJumpingState();
        CheckDyingState();
    }
}