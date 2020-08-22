using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMotor : MonoBehaviour {
    
    protected Animator anim;
    private System.Type _t;
    protected CharacterController controller;
    protected BaseState state; //reference state assigned to null

    //Character motor parameters
    protected float currentHealth;
    public float Health => currentHealth;

    //Used for detecting if the objects are grounded
    private RaycastHit _hit;
    private Vector3 _ray;
    private float _yRay;
    private float _centerX; 
    private float _centerZ;
    private float _extentX; 
    private float _extentZ;

    //Base parameters
    private readonly float baseSpeed = 5.0f;
    private readonly float baseGravity = 25.0f;
    private readonly float baseJumpForce = 13.0f;
    
    private readonly float terminalVelocity = 30.0f;
    private readonly float groundRayDistance = 0.5f;
    private readonly float groundRayInnerOffset = 0.1f;
    
    //Motor parameters
    public float Speed { get{ return baseSpeed; } }
    public float Gravity { get { return baseGravity; } }
    public float JumpForce { get { return baseJumpForce; } }
    public float TerminalVelocity { get { return terminalVelocity; } }

    public float Mass { set; get; }
    public float VerticalVelocity { set; get; }
    public Vector3 MoveVector { set; get; }
    public Quaternion RotationQuaternion { set; get; }
    
    //Storm parameters
    protected Storm storm;
    protected WindForce force;
    protected WindDirection direction;
    public float WindForce { set; get; }
    public Vector3 WindDirection { set; get; }

    //Abstract methods 
    protected abstract void UpdateMotor();
    protected abstract void Construct();

    protected virtual void Awake() {
        controller = gameObject.AddComponent<CharacterController>();
    }
    
    protected virtual void Start() {

        Mass = 1.0f;

        //Set the needed variables in the inheriting classes. E.g. set the mass for the player.
        Construct(); 

        //Construct an IdleState
        SetState();
        state.Construct();
        
        anim = GetComponent<Animator>();
        
        //Find the storm objects
        storm = GameObject.FindObjectOfType<Storm>();
        force = GameObject.FindObjectOfType<WindForce>();
        direction = GameObject.FindObjectOfType<WindDirection>();
    }

    private void Update() {
        UpdateMotor();

        WindForce = force.GetWindForce();
        WindDirection = direction.GetWindDirection();
    }

    //Enable the character controller
    public void EnableController() {
        controller.enabled = true;
    }
    
    //Disable the character controller
    public void DisableController() {
        controller.enabled = false;
    }
    
    //Move the object for which the motor is attached to. 
    //A character controller is used for the base class, but this is overwritten for some objects
    protected virtual void Move() {
        controller.Move(MoveVector * Time.deltaTime);
    }

    //Rotate the object for which the motor is attached to 
    protected virtual void Rotate() {
        if(MoveVector != Vector3.zero)
            transform.rotation = RotationQuaternion; 
    }

    //Set start up state
    protected virtual void SetState() {
        state = gameObject.AddComponent<IdleState>();
    }
    
    //Change state for the object that the motor is attached to.
    public virtual void ChangeState(string stateName) {
        _t = System.Type.GetType(stateName);

        ChangeAnimation(stateName); //Change animation depending on the state, for now, this only affects the player object

        state.Destruct(); //Destroy the previous state
        state = gameObject.AddComponent(_t) as BaseState; //Add the new state 
        state.Construct(); //Construct the new state
    }

 
    protected virtual void ChangeAnimation(string stateName) {
        //Could be converted to an abstract method if every child class had objects with animation changes.  
    }

    //Check if an object with a motor script is grounded. I.e. if an object has another object with a collider beneath it. 
    public virtual bool Grounded() {
        var bounds = controller.bounds;
        
        _yRay = (bounds.center.y - bounds.extents.y) + 0.3f; //right in the middle of the object a bit above the objects lowest point. 
        _centerX = bounds.center.x;
        _centerZ = bounds.center.z;
        _extentX = bounds.extents.x - groundRayInnerOffset;
        _extentZ = bounds.extents.z - groundRayInnerOffset;


        //Center groundRay
        _ray = new Vector3(_centerX, _yRay, _centerZ);
        if (Physics.Raycast(_ray, Vector3.down, out _hit, groundRayDistance)) return true;
        Debug.DrawRay(_ray, Vector3.down * groundRayDistance, Color.green);
        
        //outer groundRays
        _ray = new Vector3(_centerX + _extentX, _yRay, _centerZ + _extentZ);
        if (Physics.Raycast(_ray, Vector3.down, out _hit, groundRayDistance)) return true;

        _ray = new Vector3(_centerX - _extentX, _yRay, _centerZ + _extentZ);
        if (Physics.Raycast(_ray, Vector3.down, out _hit, groundRayDistance)) return true;

        _ray = new Vector3(_centerX - _extentX, _yRay, _centerZ - _extentZ);
        if (Physics.Raycast(_ray, Vector3.down, out _hit, groundRayDistance)) return true;

        _ray = new Vector3(_centerX + _extentX, _yRay, _centerZ - _extentZ);
        if (Physics.Raycast(_ray, Vector3.down, out _hit, groundRayDistance)) return true;

        return false;
    }
}
