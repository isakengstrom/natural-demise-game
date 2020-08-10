using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    private PortalManager _portalManager;
    private Collider _collider;
    private MeshRenderer _meshRenderer;
    
    private readonly Vector3 _teleportOffset = new Vector3(0f,15f,0f);

    private void Start() {
        _portalManager = FindObjectOfType<PortalManager>();
        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();

        _deactivatePortal();
    }

    private void Update() {
        if (Input.GetButtonDown("Jump")) _activatePortal();
        if (Input.GetButtonDown("Fire2")) _deactivatePortal();
    }

    private void _activatePortal() {
        _changePortalState(true);
    }

    private void _deactivatePortal() {
        _changePortalState(false);
    }

    private void _changePortalState(bool state) {
        _collider.isTrigger = state;
        _collider.enabled = state;
        _meshRenderer.enabled = state;
        
        foreach (Transform child in transform)
            child.gameObject.SetActive(state);
    }
    
    private void OnTriggerEnter(Collider other) {
        StartCoroutine(TeleportOther(other));
    }
    
    private IEnumerator TeleportOther(Collider other) {
        if (other.gameObject.CompareTag("Player"))
            other.transform.position = _portalManager.GetNextTelePosition(transform.parent.parent.name) + _teleportOffset;
        
        print("Collision with: " + other);
        
        yield return null;
    }
}
