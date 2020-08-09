using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stormCloudController : MonoBehaviour {
   
    
    public GameObject stormCloud;
    private float _stormCloudScaleMultiplierWhenHidden = 300f;
    private Vector3 _stormCloudScaleWhenHidden;
    private Vector3 _stormCloudScaleWhenVisible;

    private void Start() {
        ActivateStormCloud();
    }

    private void ActivateStormCloud() {
        var localScale = stormCloud.transform.localScale;
        
        _stormCloudScaleWhenVisible =  localScale;
        _stormCloudScaleWhenHidden = new Vector3(_stormCloudScaleMultiplierWhenHidden, localScale.y, _stormCloudScaleMultiplierWhenHidden);
        stormCloud.transform.localScale = _stormCloudScaleWhenHidden;

        stormCloud.SetActive(true);
        
        //Debugging calls:
        Invoke(nameof(ShrinkStormCloud), 3f);
        print(_stormCloudScaleWhenHidden);
        print(_stormCloudScaleWhenVisible);
    }

    public void ShrinkStormCloud() {
        //TODO: call Smoothvector3 with good values 
        SmoothVector3(_stormCloudScaleWhenHidden, _stormCloudScaleWhenVisible, 5f, Time.time);
        
    }

    public void EnlargeStormCloud() {
        //TODO: call Smoothvector3 with good values
        SmoothVector3(_stormCloudScaleWhenVisible, _stormCloudScaleWhenHidden, 5f, Time.time);
    }
    
    private Vector3 SmoothVector3(Vector3 startVector, Vector3 endVector, float smoothTime, float sTime) {
        //TODO: FIX SmoothVector3 and/or SmoothFloat
        var vx= SmoothFloat(startVector.x, endVector.x, smoothTime, sTime);
        var vy= SmoothFloat(startVector.y, endVector.y, smoothTime, sTime);
        var vz= SmoothFloat(startVector.z, endVector.z, smoothTime, sTime);
        
        return new Vector3(vx,vy,vz);
    }
    private float SmoothFloat(float startValue, float endValue, float smoothTime, float sTime) {
        var  smoothPercentage = (Time.time - sTime) / smoothTime;
        
        return Mathf.SmoothStep(startValue, endValue, smoothPercentage);
    }
    
}
