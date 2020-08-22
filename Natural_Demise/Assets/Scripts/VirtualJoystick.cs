using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    private Image _joystickContainer;
    private Image _outerCircle;
    private Image _innerCircle;
    private Vector3 _inputVector;
    private Vector3 _outerCircleStartPos;
 
    private void Start() {
        _joystickContainer = GetComponent<Image>();
        _outerCircle = _joystickContainer.transform.GetChild(0).GetComponent<Image>();
        _innerCircle = _outerCircle.transform.GetChild(0).GetComponent<Image>();

        _outerCircleStartPos = _outerCircle.rectTransform.anchoredPosition;
    }

    public virtual void OnDrag(PointerEventData ped) {
        
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_outerCircle.rectTransform, ped.position,
            ped.pressEventCamera, out var pos)) return;
        
        var sizeDelta = _outerCircle.rectTransform.sizeDelta;
            
        pos.x = (pos.x / sizeDelta.x);
        pos.y = (pos.y / sizeDelta.y);

        _inputVector = new Vector3(pos.x * 2, 0, pos.y * 2);
        _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;

        //Move innerCircle image
        _innerCircle.rectTransform.anchoredPosition = new Vector3(_inputVector.x * (sizeDelta.x / 2.25f), _inputVector.z * (sizeDelta.y / 2.25f));
    }
    
    public virtual void OnPointerDown(PointerEventData ped) {
        _outerCircle.rectTransform.position = ped.position;
        
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped) {
        _inputVector = Vector3.zero;
        _outerCircle.rectTransform.anchoredPosition = _outerCircleStartPos; //Reset outerCircle to original position
        _innerCircle.rectTransform.anchoredPosition = Vector3.zero; //reset innerCircle to the center of the outerCircle
    }

    //Called by the PlayerMotor
    public float Horizontal() {
        return Math.Abs(_inputVector.x) > Mathf.Epsilon ? _inputVector.x : Input.GetAxis("Horizontal");
    }

    //Called by the PlayerMotor
    public float Vertical() {
        return Math.Abs(_inputVector.z) > Mathf.Epsilon ? _inputVector.z : Input.GetAxis("Vertical");
    }
}
