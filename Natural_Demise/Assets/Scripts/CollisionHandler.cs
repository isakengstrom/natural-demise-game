using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {

    private CharacterMotor _cm;

    private void Start() {

        _cm = GameObject.FindObjectOfType<CharacterMotor>();

    }

    private void OnCollisionEnter(Collision collision) {

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (!collision.gameObject.CompareTag("Player")) return;
        
        if (gameObject.CompareTag("Debris")) {
            _cm.UpdateHealth(-5);
        }
        else if (gameObject.CompareTag("HealthPack")) {
            _cm.UpdateHealth(1);
        }

        //If the GameObject has the same tag as specified, output this message in the console
        //Debug.Log("Do something here");
            
        //Destroy the object that collides with the player. 
        Destroy(gameObject);
    }
}
