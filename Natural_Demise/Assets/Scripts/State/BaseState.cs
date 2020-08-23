using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour {
    
    protected BaseMotor motor;
    protected LevelManager levelManager;
    protected float? fallingTimer; 
    
    #region baseState implementation 

    public virtual void Construct() {
        motor = GetComponent<BaseMotor>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();

        fallingTimer = null;
    }

    public virtual void Destruct() {
        Destroy(this);
    }

    //Overridden to handle all possible combinations of state changes, depending on the current state.
    //Uses the code in the "CheckStates implementation"
    public virtual void Transition() { }

    #endregion
    
    #region CheckStates implementation

    protected float playerInputDirectionMagnitude;
    private const float WalkRunThreshold = 0.7f;
    
    protected void CheckIdleState() {
        if (playerInputDirectionMagnitude < Mathf.Epsilon) 
            motor.ChangeState("IdleState");
    }
    
    protected void CheckWalkingState() {
        if (playerInputDirectionMagnitude > Mathf.Epsilon && playerInputDirectionMagnitude < WalkRunThreshold)
            motor.ChangeState("WalkingState");
    }
    
    protected void CheckRunningState() {
        if (playerInputDirectionMagnitude > WalkRunThreshold)
            motor.ChangeState("RunningState");
    }
    
    protected void CheckJumpingState() {
        if (Input.GetButtonDown("Jump"))
            motor.ChangeState("JumpingState");
    }

    protected void CheckFallingState() {
        if(motor.VerticalVelocity < 0f)
            motor.ChangeState("FallingState");
    }
    
    protected void CheckLandingState() {
        if (motor.Grounded())
            motor.ChangeState("LandingState");
    }
    
    protected void CheckDyingState() {
        if (motor.Health <= 0f)
            motor.ChangeState("DyingState");
    }

    #endregion
    
    public abstract Vector3 ProcessMotion(Vector3 input);
    
    //Process rotations, only the rotation around the y axis is relevant
    public virtual Quaternion ProcessRotation(Vector3 input) {
        var rot = new Vector3(input.x, 0.0f, input.z);
        
        //Before calling the lerp between the two angles, check if there even is a rotation to process. 
        if (rot != Vector3.zero)
            return Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rot.normalized), 0.4f);

        return transform.rotation;
    }

    protected virtual void ApplySpeed(ref Vector3 input, float speed) {
        input *= speed; 
    }

    protected void ApplyGravity(ref Vector3 input, float gravity) {
        motor.VerticalVelocity -= gravity * Time.deltaTime;

        motor.VerticalVelocity = Mathf.Clamp(motor.VerticalVelocity, -motor.TerminalVelocity, motor.TerminalVelocity);

        input.Set(input.x, motor.VerticalVelocity, input.z);
    }

}
