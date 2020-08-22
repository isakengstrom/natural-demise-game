using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class PlayerMotor : CharacterMotor {
    public VirtualJoystick joystick;
    private Vector3 _dir = Vector3.zero;
    //public BaseHealth health;

    //public PlayerHealth ph;
    
    protected override void Construct() {
        startHealth = 100.0f;
        Mass = 5.0f;
        base.Construct();
    }

    protected override void UpdateMotor() {
        //Get input
        MoveVector = InputDirection();

        //Send input to a filter 
        MoveVector = state.ProcessMotion(MoveVector);
        RotationQuaternion = state.ProcessRotation(MoveVector);

        //Add wind input to the player
        MoveVector += WindDirection * WindForce / Mass;

        //Check if we should change current state
        state.Transition();

        //Move 
        Move();

        //Rotate
        Rotate();

        //Check if an object is grounded, used together with rays to debug
        //Grounded();
    }

    private Vector3 InputDirection() {
         _dir = Vector3.zero;

        //dir.x = Input.GetAxis("Horizontal");
        //dir.z = Input.GetAxis("Vertical");

        _dir.x = joystick.Horizontal();
        _dir.z = joystick.Vertical();


        if (_dir.magnitude > 1f)
            _dir.Normalize();

        //Debug.Log(_dir.magnitude);

        return _dir;
    }

}
