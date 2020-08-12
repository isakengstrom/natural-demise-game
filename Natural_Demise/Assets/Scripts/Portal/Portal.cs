using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    private Collider _collider;
    private MeshRenderer _meshRenderer;
    private LevelManager _levelManager;

    private void Start() {
        _levelManager = FindObjectOfType<LevelManager>();
        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();

        DeactivatePortal();
    }
    
    private void Update() {
        //For debugging purposes:
        if (Input.GetButtonDown("Jump")) ActivatePortal();
        if (Input.GetButtonDown("Fire2")) DeactivatePortal();
    }

    public void ActivatePortal() {
        _changePortalState(true);
    }

    private void DeactivatePortal() {
        _changePortalState(false);
    }

    private void _changePortalState(bool state) {
        _collider.enabled = state;
        _meshRenderer.enabled = state;
        
        foreach (Transform child in transform)
            child.gameObject.SetActive(state);
    }
    
    private void OnTriggerEnter(Collider other) {
        StartCoroutine(TeleportObject(other));
    }
    
    private IEnumerator TeleportObject(Collider other) {
        if (other.gameObject.CompareTag("Player"))
            _levelManager.SignalPlayerTeleportation(transform.parent.parent.name);
        else 
            _levelManager.TeleportOther(other);
        
        yield return null;
    }
}
