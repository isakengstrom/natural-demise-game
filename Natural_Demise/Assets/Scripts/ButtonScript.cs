using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

    private TextMeshPro _textMeshPro;
    

    private void Start() {
        _textMeshPro = GetComponent<TextMeshPro>();
    }

    private void Update() {
        if(hover()) OnMouseOver();
    } 

    private bool hover() {
        return EventSystem.current.IsPointerOverGameObject();
    }
    private void OnMouseOver() {
        print("hi");
    }
}
