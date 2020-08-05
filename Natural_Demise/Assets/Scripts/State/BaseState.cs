using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected BaseMotor motor;
    //public WindForce force;
    protected float playerInputDirectionMagnitude;
    private const float WalkRunThreshold = 0.7f;

    #region baseState implementation 

    public virtual void Construct() {
        motor = GetComponent<BaseMotor>();
    }

    public virtual void Destruct() {
        Destroy(this);
    }

    public virtual void Transition() {
        
    }

    #endregion
    
    #region CheckStates implementation

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
    
    private void Start() {
        //force = GameObject.FindObjectOfType<WindForce>();//gameObject.AddComponent<WindForce>();
    }

    public abstract Vector3 ProcessMotion(Vector3 input);
    
    public virtual Quaternion ProcessRotation(Vector3 input) {
        if (new Vector3(input.x, 0.0f, input.z) != Vector3.zero)
            //return Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(input.x, 0.0f, input.z).normalized), 0.3f);
            //Quaternion.LookRotation(new Vector3(input.x, 0.0f, input.z).normalized);
            return Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(input.x, 0.0f, input.z).normalized), 0.4f);

        return transform.rotation;
    }

    protected virtual void ApplySpeed(ref Vector3 input, float speed) {
        input *= speed; // (speed + (force.getWindForce() / motor.Mass * Time.fixedDeltaTime));
    }

    protected void ApplyGravity(ref Vector3 input, float gravity) {
        motor.VerticalVelocity -= gravity * Time.deltaTime;

        motor.VerticalVelocity = Mathf.Clamp(motor.VerticalVelocity, -motor.TerminalVelocity, motor.TerminalVelocity);

        input.Set(input.x, motor.VerticalVelocity, input.z);
    }

}
