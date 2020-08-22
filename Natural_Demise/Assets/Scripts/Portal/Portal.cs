using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attached to each portal 
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
    
    //Detect collisions with the portal, and call methods depending on the collider
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
            _levelManager.SignalPlayerTeleportation(transform.parent.parent.name);
        else 
            _levelManager.TeleportOther(other);

    }
}
