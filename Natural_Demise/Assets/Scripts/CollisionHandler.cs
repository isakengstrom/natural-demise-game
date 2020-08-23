using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handle collisions between the player and moving objects. 
public class CollisionHandler : MonoBehaviour {

    private CharacterMotor _cm;
    private void Start() {
        _cm = GameObject.FindObjectOfType<CharacterMotor>();
    }
    
    private void OnCollisionEnter(Collision collision) {
        //Return if the player isn't involved in the collision
        if (!collision.gameObject.CompareTag("Player")) return;
    
        //Change the player health on collision 
        if (gameObject.CompareTag("Debris")) {
            _cm.UpdateHealth(-5f);
        }

        if (gameObject.CompareTag("Whirlwind")) {
            _cm.UpdateHealth(-10f);
        }

        /*
        if (gameObject.CompareTag("HealthPack")) {
            _cm.UpdateHealth(1);
        }
        */

        //Destroy the object that collides with the player. 
        Destroy(gameObject);
        
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
            _cm.UpdateHealth(-1000f);

    }
}
