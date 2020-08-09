using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlwindMotor : BaseMotor {
    

    protected override void Construct() {
        Mass = 0.3f;//Random.Range(0.03f, 0.09f);
        //gameObject.tag = "Whirlwind";
       
        //gameObject.transform.rotation = Quaternion.Euler(-Mathf.PI/2, 0,0);
    }
    
    protected override void SetState() {
        state = gameObject.AddComponent<WhirlState>();
        controller.detectCollisions = false; //Collisions are detected through a collider instead
    }
    
    protected override void UpdateMotor() {

        //Get input
        MoveVector = WindDirection;

        //Send input to a filter 
        MoveVector = state.ProcessMotion(MoveVector);

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
