using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour {


    public float Health { get; set; }
    private float timer;
    protected abstract void Kill();

    protected abstract void Construct();

    protected virtual void Start() {
        timer = 0.0f;
        Health = 1.0f;
        //Debug.Log("Health added");

        Construct();
    }

    private void UpdateHealth(int ind) {




        if (Health > 0.0f) {
            
        }
    }



}
