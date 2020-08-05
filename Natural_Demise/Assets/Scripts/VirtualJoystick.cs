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
    /*
    private float joystickRadius;
    private float joystickContainerLowerBound;
    private float joystickContainerUpperBound;
    private float joystickContainerLeftBound;
    private float joystickContainerRightBound;
    */

    private void Start() {
        _joystickContainer = GetComponent<Image>();
        _outerCircle = _joystickContainer.transform.GetChild(0).GetComponent<Image>();
        _innerCircle = _outerCircle.transform.GetChild(0).GetComponent<Image>();

        _outerCircleStartPos = _outerCircle.rectTransform.anchoredPosition;

        //joystickContainer.transform.parent.
        //joystickContainer.rectTransform.sizeDelta = transform.parent.GetComponent().rectTransform.sizeDelta;
        /*
        joystickRadius = outerCircle.rectTransform.sizeDelta.x / 2;
        */

        //innerCircle = transform.GetChild(0).GetComponent<Image>();
        /*
        joystickContainerLowerBound = joystickContainer.transform.localPosition.y;
        joystickContainerUpperBound = joystickContainer.transform.localPosition.y + joystickContainer.rectTransform.sizeDelta.y;
        joystickContainerLeftBound = joystickContainer.transform.localPosition.x;
        joystickContainerRightBound = joystickContainer.transform.localPosition.x + joystickContainer.rectTransform.sizeDelta.x;
        */

        /*
        Debug.Log(joystickContainerLowerBound);
        Debug.Log(joystickContainerUpperBound);
        Debug.Log(joystickContainerLeftBound);
        Debug.Log(joystickContainerRightBound);
        */
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_outerCircle.rectTransform, ped.position,
            ped.pressEventCamera, out var pos)) return;
        var sizeDelta = _outerCircle.rectTransform.sizeDelta;
            
        pos.x = (pos.x / sizeDelta.x);
        pos.y = (pos.y / sizeDelta.y);

        _inputVector = new Vector3(pos.x * 2, 0, pos.y * 2);
        _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;

        //Debug.Log(ped.pressPosition);

        //Move innerCircle image
        _innerCircle.rectTransform.anchoredPosition = new Vector3(_inputVector.x * (sizeDelta.x / 2.25f), _inputVector.z * (sizeDelta.y / 2.25f));
    }
    
    //Transform.TransformPoint frpn local to global 
    public virtual void OnPointerDown(PointerEventData ped) {
        _outerCircle.rectTransform.position = ped.position;

        /*
        //Lower bound
        if (Mathf.Abs(joystickContainerLowerBound - ped.position.y) < joystickRadius) 
            outerCircle.rectTransform.position = new Vector3(outerCircle.rectTransform.position.x, joystickContainerLowerBound + joystickRadius, outerCircle.rectTransform.position.z);
        //Upper bound
        if (Mathf.Abs(joystickContainerUpperBound - ped.position.y) < joystickRadius)
            outerCircle.rectTransform.position = new Vector3(outerCircle.rectTransform.position.x, joystickContainerUpperBound - joystickRadius, outerCircle.rectTransform.position.z);

        //Left bound
        if (Mathf.Abs(joystickContainerLeftBound - ped.position.x) < joystickRadius)
            outerCircle.rectTransform.position = new Vector3(joystickContainerLeftBound + joystickRadius, outerCircle.rectTransform.position.y, outerCircle.rectTransform.position.z);

        //Right bound
        if (Mathf.Abs(joystickContainerRightBound - ped.position.x) < joystickRadius)
            outerCircle.rectTransform.position = new Vector3(joystickContainerRightBound - joystickRadius, outerCircle.rectTransform.position.y, outerCircle.rectTransform.position.z);
        */

        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped) {
        _inputVector = Vector3.zero;
        _outerCircle.rectTransform.anchoredPosition = _outerCircleStartPos; //Reset outerCircle to original position
        _innerCircle.rectTransform.anchoredPosition = Vector3.zero; //reset innerCircle to the center of the outerCircle
    }

    //Called by the PlayerMotor
    public float Horizontal()
    {
        return Math.Abs(_inputVector.x) > Mathf.Epsilon ? _inputVector.x : Input.GetAxis("Horizontal");
    }

    //Called by the PlayerMotor
    public float Vertical()
    {
        return Math.Abs(_inputVector.z) > Mathf.Epsilon ? _inputVector.z : Input.GetAxis("Vertical");
    }
}
