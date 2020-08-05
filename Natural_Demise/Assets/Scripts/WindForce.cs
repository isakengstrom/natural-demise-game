using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    private float _windForce;
    private float _oldWindForce;
    private float _newWindForce;
    private float _windForceChange;

    private float _timeSinceStarted;
    private float _smoothPercentage;


    private void Start() {
        _windForce = 3.0f;// Random.Range(3.0f, 5.0f); 
    }

    public void WindDirectionChange() {
        //temporarly decrease windforce
        //Debug.Log("Major change in wind direction.");


    }

    public float GetWindForce() {
        return _windForce;
    }

    public float SmoothFloat(float startValue, float endValue, float smoothTime, float startTime) {
        _timeSinceStarted = Time.time - startTime;
        _smoothPercentage = _timeSinceStarted / smoothTime;

        return Mathf.SmoothStep(startValue, endValue, _smoothPercentage);
    }

}
