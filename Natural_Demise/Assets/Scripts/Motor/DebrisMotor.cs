using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisMotor : BaseMotor {

    protected override void Construct() {
        Mass = Random.Range(0.125f, 0.135f);
        gameObject.transform.localScale *= Mass * 5f;

        //gameObject.tag = "Debris";   
    }

    protected override void SetState() {
        state = gameObject.AddComponent<RollingState>();
        controller.detectCollisions = false; //Collisions are detected through a collider instead
    }

    protected override void UpdateMotor() {

        //Get input
        MoveVector = WindDirection;

        //Send input to a filter 
        MoveVector = state.ProcessMotion(MoveVector);

        //MoveVector += Quaternion.AngleAxis(90, Vector3.up) * MoveVector * Mathf.Sin(timer * Random.Range(0.7f, 1.3f)) / 3 * initialDirection;

        RotationQuaternion = state.ProcessRotation(MoveVector);

        //RotationQuaternion = transform.Rotate(Vector3.left, 45 * Time.deltaTime * Speed);

        //Check if we should change current state
        state.Transition();

        //Move 
        Move();

        //Rotate
        Rotate();


        //Check if an object is grounded, used together with rays to debug
        //Grounded();
    }

    //Override the move function so that the wind particles aren't effected by other objects' colliders
    protected override void Move() {
        transform.position += MoveVector * Time.deltaTime;
    }

    public override bool Grounded() { return true; }

}
