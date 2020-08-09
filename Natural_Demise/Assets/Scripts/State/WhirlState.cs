using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlState : BaseState {
    private float _timer;
    private int _initialDirection;
    

    public override void Construct() {
        base.Construct();
    
        _timer = 0.0f;
        _initialDirection = Random.Range(0, 2) * 2 - 1; //Basically, if its initial direction is left or right. (-1 or 1)
        
    }
    
    public override Vector3 ProcessMotion(Vector3 input) {
        ApplySpeed(ref input, motor.WindForce / motor.Mass); //in baseState
        
        _timer += Time.deltaTime;
        
        input += Quaternion.AngleAxis(90, Vector3.up) * input * (Mathf.Sin(_timer * 3f) * _initialDirection);

        
        return input;
    }


    public override Quaternion ProcessRotation(Vector3 input)
    {
        var rotation = transform.rotation;
        return input != Vector3.zero ? Quaternion.LookRotation(input.normalized) : rotation;
        //return input != Vector3.zero ? Quaternion.Lerp(rotation, Quaternion.LookRotation(input.normalized), 0.4f) : rotation;
        //Quaternion.LookRotation(new Vector3(input.x, 0.0f, input.z).normalized);
    }
}
