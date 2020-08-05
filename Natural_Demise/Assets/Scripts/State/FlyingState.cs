using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingState : BaseState {

    public override Vector3 ProcessMotion(Vector3 input) {
        ApplySpeed(ref input, motor.WindForce / motor.Mass); //in baseState

        return input;
    }


    public override Quaternion ProcessRotation(Vector3 input) {
        return input != Vector3.zero ? Quaternion.LookRotation(input.normalized) : transform.rotation;
    }
}