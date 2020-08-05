using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    private PortalManager _portalManager;
    
    private Collider _collider;
    private MeshRenderer _meshRenderer;
    private readonly Vector3 _teleHeight = new Vector3(0f,20f,0f); 

    //private const float PortalLockTimer = 10f;
    //private bool _isPortalLocked;
    
    
    private void Start() {
        _portalManager = FindObjectOfType<PortalManager>();
        
        
        //_isPortalLocked=true;
        //StartCoroutine(WaitAndUnlock(PortalLockTimer));

        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();    
        
        
        _deactivatePortal();
    }

    private void Update() {
        
        if (Input.GetButtonDown("Jump")) _activatePortal();
        if (Input.GetButtonDown("Fire2")) _deactivatePortal();
    }

    private void _activatePortal() {
        _collider.enabled = true;
        _meshRenderer.enabled = true;
        
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
    }

    private void _deactivatePortal() {
        _collider.enabled = false;
        _meshRenderer.enabled = false;
        
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }

    //Lock the portal for waitTime seconds to give everything time to be instantiated
    /*
    private IEnumerator WaitAndUnlock(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        _isPortalLocked=false;
    }
    */  
    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.position = _portalManager.GetNextTelePosition() + _teleHeight;

        /*if (!_isPortalLocked) {
            col.transform.position = _portalManager.GetNextTelePosition() + new Vector3(0f,10f,0f);    
        }*/
    }
    
    
}
