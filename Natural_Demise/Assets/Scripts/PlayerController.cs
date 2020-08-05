using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour{ 
    public float moveSpeed;
    private Rigidbody _rb;

    //joystick 
    public Transform circle;
    public Transform outerCircle;

    private void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    


    private void Update() {
        
    }

    private void FixedUpdate() {
        
    }





    /*
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        //moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f,  Input.GetAxis("Vertical"));
        //moveVelocity = moveInput * moveSpeed;

        if(Input.GetMouseButtonDown(0)) {
            //pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            pointA = Camera.main.ScreenToWorldPoint(new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f, 10));

            circle.transform.position = pointA * (-1);
            outerCircle.transform.position = pointA * (-1);

            Debug.Log(pointA);

            circle.GetComponent<SpriteRenderer>().enabled = true;
            outerCircle.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetMouseButton(0)) {
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        else {
            touchStart = false;
        }
    }

    private void FixedUpdate() {
        if (touchStart) {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            //moveVelocity = direction * (-1);
            moveVelocity = new Vector3(direction.x * (-1), 0.0f, direction.y * (-1));

            circle.transform.position = new Vector3(pointA.x + direction.x, pointA.y + direction.y, 0.0f) * (-1);
        }


        rb.velocity = moveVelocity;
        //transform.rotation.SetLookRotation(rb.velocity);

        rb.rotation = Quaternion.LookRotation(moveVelocity);
            
    }

    */
}




