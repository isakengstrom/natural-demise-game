using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMotor : BaseMotor
{
    private Rigidbody _rb;


    protected override void Construct() {
        Mass = Random.Range(0.1f, 0.4f);

        _rb = gameObject.AddComponent<Rigidbody>();
    }
    
    protected override void Start() {
        controller = gameObject.AddComponent<CharacterController>();

        //Construct a WalkingState
        state = gameObject.AddComponent<WalkingState>();
        state.Construct();

        Construct(); //Set the needed variables in the inheriting classes. E.g. set the mass for the player.
        
    }
    

    protected override void UpdateMotor() {
        //Get input
        MoveVector = Vector3.zero;

        //Send input to a filter 
        //MoveVector = state.ProcessMotion(MoveVector);

        //Check if we should change current state
        state.Transition();


        //Move 
        Move();

        //Check if an object is grounded, used together with rays to debug
        //Grounded();
    }
}
