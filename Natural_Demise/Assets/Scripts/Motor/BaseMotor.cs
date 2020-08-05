using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMotor : MonoBehaviour {

    protected CharacterController controller;
    protected BaseState state; //reference state assigned to null
    //protected Transform thisTransform;

    private RaycastHit _hit;
    private Vector3 _ray;

    protected Storm storm;
    protected WindForce force;
    protected WindDirection direction;

    protected float currentHealth;
    public float Health { get { return currentHealth; } }

    private readonly float baseSpeed = 5.0f;
    private readonly float baseGravity = 25.0f;
    private readonly float baseJumpForce = 13.0f;
    //private float baseMass = 10.0f; //float v = (F/rigidbody.mass)*Time.fixedDeltaTime;

    private readonly float terminalVelocity = 30.0f;
    private readonly float groundRayDistance = 0.5f;
    private readonly float groundRayInnerOffset = 0.1f;

    //Used for detecting if the objects are grounded
    private float _yRay;
    private float _centerX; 
    private float _centerZ;
    private float _extentX; 
    private float _extentZ;

    private System.Type _t;

    public float Speed { get{ return baseSpeed; } }
    public float Gravity { get { return baseGravity; } }
    public float JumpForce { get { return baseJumpForce; } }
    public float TerminalVelocity { get { return terminalVelocity; } }

    public float WindForce { set; get; }
    public Vector3 WindDirection { set; get; }

    public float Mass { set; get; }
    public float VerticalVelocity { set; get; }
    public Vector3 MoveVector { set; get; }
    public Quaternion RotationQuaternion { set; get; }

    protected abstract void UpdateMotor();
    protected abstract void Construct();

    protected Animator anim;

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

        storm = GameObject.FindObjectOfType<Storm>();
        force = GameObject.FindObjectOfType<WindForce>();
        direction = GameObject.FindObjectOfType<WindDirection>();
    }

    protected virtual void SetState() {
        state = gameObject.AddComponent<IdleState>();
    }

    public BaseState GetState() {
        return state;
    }

    private void Update() {
        UpdateMotor();

        WindForce = force.GetWindForce();
        WindDirection = direction.GetWindDirection();

        //Debug.Log(MoveVector);
        //Debug.Log(MoveVector.magnitude);
    }

    protected virtual void Move() {
        //controller.Move(MoveVector * Time.deltaTime);
        //MoveVector += storm.test() / Mass * Time.fixedDeltaTime;
        //controller.Move((MoveVector + WindDirection * WindForce / Mass) * Time.deltaTime);
        //controller.Move(MoveVector);

        controller.Move(MoveVector * Time.deltaTime);
    }

    protected virtual void Rotate() {
        if(MoveVector != Vector3.zero)
            transform.rotation = RotationQuaternion; 
    }

    public virtual void ChangeState(string stateName) {
        _t = System.Type.GetType(stateName);

        ChangeAnimation(stateName);

        state.Destruct();
        state = gameObject.AddComponent(_t) as BaseState;
        state.Construct();
    }

 
    protected virtual void ChangeAnimation(string stateName) {
        //Could be converted to an abstract method if every child class had objects with animation changes.  
    }

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
