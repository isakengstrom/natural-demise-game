using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class StormCloudController : MonoBehaviour {
    
    public GameObject stormCloud;
    private readonly float _stormCloudScaleMultiplierWhenHidden = 300f;
    private Vector3 _stormCloudScaleWhenHidden;
    private Vector3 _stormCloudScaleWhenVisible;
    private readonly float _stormCloudScaleTime = 3f;

    private void Start() {
        ActivateStormCloud();
    }

    private void ActivateStormCloud() {
        var localScale = stormCloud.transform.localScale;
        
        _stormCloudScaleWhenVisible =  localScale;
        _stormCloudScaleWhenHidden = new Vector3(_stormCloudScaleMultiplierWhenHidden, localScale.y, _stormCloudScaleMultiplierWhenHidden);
        stormCloud.transform.localScale = _stormCloudScaleWhenHidden;
        stormCloud.SetActive(true);
    }

    public void ShrinkStormCloud() {
        StartCoroutine(ScaleStormCloud(_stormCloudScaleWhenHidden, _stormCloudScaleWhenVisible, _stormCloudScaleTime));
    }
    
    public void ExpandStormCloud() {
        StartCoroutine(ScaleStormCloud(_stormCloudScaleWhenVisible, _stormCloudScaleWhenHidden, _stormCloudScaleTime));
    }

    private IEnumerator ScaleStormCloud(Vector3 originalScale, Vector3 targetScale, float sTime) {
        var currentTime = 0.0f;
        
        while (currentTime <= sTime) {
            stormCloud.transform.localScale = Vector3.Lerp(originalScale, targetScale, currentTime / sTime);
            currentTime += Time.deltaTime;
            yield return null;
            
        } 
    }
}
