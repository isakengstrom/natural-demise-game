using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlwindMotor : BaseMotor {
    private float _timer = 0.0f;
    private float _initialDirection;

    protected override void Construct() {
        Mass = 0.3f;//Random.Range(0.03f, 0.09f);
        //gameObject.tag = "Whirlwind";
        _initialDirection = (Random.Range(0, 2) * 2 - 1); //Basically, if its original direction is left or right. 
        gameObject.transform.rotation = Quaternion.Euler(-Mathf.PI/2, 0,0);
    }
    
    protected override void SetState() {
        state = gameObject.AddComponent<RollingState>();
        controller.detectCollisions = false; //Collisions are detected through a collider instead
    }
    
    protected override void UpdateMotor() {
        _timer += Time.deltaTime;
        
        //Get input
        MoveVector = WindDirection;

        //Send input to a filter 
        MoveVector = state.ProcessMotion(MoveVector);

        MoveVector += Quaternion.AngleAxis(90, Vector3.up) * MoveVector * (Mathf.Sin(_timer * Random.Range(0.7f, 1.3f)) * _initialDirection);

        //RotationQuaternion = state.ProcessRotation(MoveVector);


        //Check if we should change current state
        state.Transition();

        //Move 
        Move();

        //Rotate
        Rotate();
    }

    //Override the move function so that the wind particles aren't effected by other objects' colliders
    protected override void Move() {
        transform.position += MoveVector * Time.deltaTime;
    }

    public override bool Grounded() { return true; }
    
}
