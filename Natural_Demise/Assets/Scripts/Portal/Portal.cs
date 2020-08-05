using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    private PortalManager _portalManager;
    private GameObject _player;
    
    private Collider _collider;
    private MeshRenderer _meshRenderer;
    private readonly Vector3 _teleHeight = new Vector3(0f,20f,0f); 

    //private const float PortalLockTimer = 10f;
    //private bool _isPortalLocked;
    
    
    private void Start() {
        _portalManager = FindObjectOfType<PortalManager>();
        _player = GameObject.FindWithTag("Player");
        
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
        _collider.isTrigger = true;
        _meshRenderer.enabled = true;
        
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
    }

    private void _deactivatePortal() {
        _collider.isTrigger = false;
        _collider.enabled = false;
        _meshRenderer.enabled = false;
        
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other) {
        StartCoroutine(TeleportOther(other));
        /*
        if (other.gameObject.CompareTag("Player")) {
            _player.transform.position = _portalManager.GetNextTelePosition() + _teleHeight; 
            //print(other.transform.position);'
            
        }
        */
    }


    private IEnumerator TeleportOther(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            other.transform.position = _portalManager.GetNextTelePosition() + _teleHeight;
        } 
        yield return new WaitForSeconds(0.1f);

        print("Coroutine passed");
    }
    
    
}
