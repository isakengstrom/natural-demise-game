using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingState : BaseState {
    public override Vector3 ProcessMotion(Vector3 input) {
        ApplySpeed(ref input, motor.WindForce / motor.Mass); //in baseState

        return input;
    }


    public override Quaternion ProcessRotation(Vector3 input)
    {
        var rotation = transform.rotation;
        return input != Vector3.zero ? Quaternion.Lerp(rotation, Quaternion.LookRotation(input.normalized), 0.4f) : rotation;
        //Quaternion.LookRotation(new Vector3(input.x, 0.0f, input.z).normalized);
    }
}
