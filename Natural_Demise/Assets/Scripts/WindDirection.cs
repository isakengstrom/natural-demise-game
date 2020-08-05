using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindDirection : MonoBehaviour
{
    private WindForce _force;
    private Storm _storm;

    private Vector3 _windDirection;
    private GameObject _windSphere;
    //private GameObject _windSpawnPoint;
    private float _windRadius = 100f;

    private float[] oldWind = new float[3];
    private float[] newWind = new float[3];
    private float[] startTime = new float[3];
    private float[] smoothStepTime = new float[3];
    private float[] windDirChange = new float[3] {0.0f, 0.0f, 0.0f};


    private Vector3 _rayStartingPoint;
    private float _timeSinceStarted;
    private float _smoothPercentage;

    public Vector3 StormCenterPosition { get; set; }


    private void Awake() {
    
        //Debug.Log(force.GetInstanceID());
    }

    private void Start() {
        /*
        _windSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _windSphere.transform.localScale = new Vector3(windRadius, windRadius, windRadius);
        _windSphere.GetComponent<Collider>().enabled = false;
        _windSphere.GetComponent<Renderer>().enabled = false;
        */

        _force = GameObject.FindObjectOfType<WindForce>();
        _storm = GameObject.FindObjectOfType<Storm>();

/*
        _windSpawnPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _windSpawnPoint.GetComponent<Collider>().enabled = false;
        _windSpawnPoint.GetComponent<Renderer>().enabled = false;
        _windSpawnPoint.transform.position = Vector3.zero;
  */      
        _rayStartingPoint = Vector3.zero;

        StormCenterPosition = Vector3.zero;

        Invoke(nameof(_setWindDirectionAndInvokeSmall), 1.0f);
        Invoke(nameof(_setWindDirectionAndInvokeMedium), 1.0f);
        Invoke(nameof(_setWindDirectionAndInvokeBig), 1.0f);
        //Invoke(nameof(_setWindDirectionAndInvokeBig), 20.0f);
    }

    private void Update() {
        _changeTotalWindDirection();
    }
    
    private void _changeTotalWindDirection() {

        windDirChange[0] = SmoothFloat(oldWind[0], newWind[0], smoothStepTime[0], startTime[0]);
        windDirChange[1] = SmoothFloat(oldWind[1], newWind[1], smoothStepTime[1] / 3, startTime[1]);
        windDirChange[2] = SmoothFloat(oldWind[2], newWind[2], 0.5f, startTime[2]);
        
        _rayStartingPoint = StormCenterPosition + new Vector3(Mathf.Cos(windDirChange[0] + windDirChange[1] + windDirChange[2]), 0, Mathf.Sin(windDirChange[0] + windDirChange[1] + windDirChange[2])) * _windRadius;

        //_windSpawnPoint.transform.position = _rayStartingPoint;
        _windDirection = transform.localPosition - _rayStartingPoint;

        Debug.DrawRay(_rayStartingPoint, _windDirection, new Color(1f, 0.32f, 0.39f));
    }

    #region SetWindDirectionSizes implementation
    
    private void _setWindDirectionAndInvokeSmall() {
        smoothStepTime[0] = Random.Range(3.0f, 7.0f);

        oldWind[0] = newWind[0];
        newWind[0] = Random.Range(-1 * (Mathf.PI / 18), (Mathf.PI / 18));
        startTime[0] = Time.time;

        Invoke(nameof(_setWindDirectionAndInvokeSmall), smoothStepTime[0]);
    }

    private void _setWindDirectionAndInvokeMedium() {
        smoothStepTime[1] = Random.Range(10.0f, 15.0f);

        oldWind[1] = newWind[1];
        newWind[1] = Random.Range(oldWind[1] - (Mathf.PI / 5), oldWind[1] + (Mathf.PI / 5));
        startTime[1] = Time.time;

        Invoke(nameof(_setWindDirectionAndInvokeMedium), smoothStepTime[1]);
    }

    private void _setWindDirectionAndInvokeBig() {
        //smoothStepTime[2] = Random.Range(45.0f, 65.0f);
        smoothStepTime[2] = Random.Range(2.0f, 5.0f);

        oldWind[2] = newWind[2];
        newWind[2] = Random.Range(oldWind[2] - (7 * Mathf.PI / 6), oldWind[2] + (7 * Mathf.PI / 6));
        startTime[2] = Time.time;

        _force.WindDirectionChange();
        //storm.windDirectionChange();

        Invoke(nameof(_setWindDirectionAndInvokeBig), smoothStepTime[2]);
    }
    
    #endregion
    
    public Vector3 GetWindDirection() {
        return _windDirection / _windRadius;
    }

    public Vector3 GetWindStartingPoint() {
        return _rayStartingPoint;
    }

    private float SmoothFloat(float startValue, float endValue, float smoothTime, float sTime) {

        _timeSinceStarted = Time.time - sTime;
        _smoothPercentage = _timeSinceStarted / smoothTime;

        return Mathf.SmoothStep(startValue, endValue, _smoothPercentage);
    }
}
