using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class LevelManager : MonoBehaviour {
    public Vector3[] IslandGlobalOrigins => _islandGlobalOrigins; 
  
    public int IslandAmount => _islandAmount;
    public int NextIslandIndex => _nextIslandIndex; 
    
    private Vector3[] _islandGlobalOrigins;
    private int _islandAmount;
    private int _nextIslandIndex;
    private int _currentIslandIndex;

    private GameObject _windCenter;
    private Storm _storm;
    private WindDirection _windDirection;

    [SerializeField] public GameObject portalBegin;
    [SerializeField] public GameObject portalNormal;
    [SerializeField] public GameObject portalEnd;
    private GameObject _portalChoice; 
    private GameObject[] _portalClones;
    private Portal[] _portalClonesScripts;

    private GameObject _player;
    private readonly Vector3 _pLayerTeleportOffset = new Vector3(0f,15f,0f);

    private void Start() {
        _windCenter = GameObject.FindGameObjectWithTag("WindCenter");
        _storm = _windCenter.GetComponent<Storm>();
        _windDirection = _storm.GetComponent<WindDirection>();
        _player = GameObject.FindGameObjectWithTag("Player");

        _findIslandOrigins();
        _setUpPortals();
        
        Invoke(nameof(_activateCurrentPortal),3f);
    }

    private void _setUpPortals() {
        _portalClones = new GameObject[IslandAmount];
        _portalClonesScripts = new Portal[IslandAmount];
        
        _currentIslandIndex = 0;
        _nextIslandIndex = 1;
        for (var i = 0; i < IslandAmount; i++) {
            if (i == 0) _portalChoice = portalBegin;
            else if (i == IslandAmount - 1) _portalChoice = portalEnd;
            else _portalChoice = portalNormal;
            
            _portalClones[i] = (GameObject) Instantiate(_portalChoice, transform.GetChild(i));
            _portalClones[i].name = "portal" + i;

            _portalClonesScripts[i] = _portalClones[i].transform.GetChild(0).GetChild(0).GetComponent<Portal>();
        }
    }
    
    public void SignalPlayerTeleportation(string portalName) {
        _setNextPortalIndex(portalName);
        _teleportPlayer();
        _setWindCenter();

        _currentIslandIndex = _nextIslandIndex;
    }

    private void _activateCurrentPortal() {
        _portalClonesScripts[_currentIslandIndex].ActivatePortal();
    }
    private void _setNextPortalIndex(string portalName) {
        var isPortalIdFound = false;
        for (var i = 0; i < IslandAmount; i++) {
            if (_portalClones[i].name != portalName) continue;
            _nextIslandIndex = (i+1) % IslandAmount;
            isPortalIdFound = true;
        }

        if (!isPortalIdFound) _nextIslandIndex = 0;
    }

    private void _teleportPlayer() {
        _player.transform.position = _islandGlobalOrigins[_nextIslandIndex] + _pLayerTeleportOffset;
    }

    public void TeleportOther(Collider other) {
        other.transform.position = _islandGlobalOrigins[_nextIslandIndex];
    }
    
    private void _findIslandOrigins() {
        _islandAmount = transform.childCount;
        _islandGlobalOrigins = new Vector3[_islandAmount];
        
        for (var i = 0; i < _islandAmount; i++) {
            _islandGlobalOrigins[i] = transform.GetChild(i).transform.position;
        }
    }

    private void _setWindCenter() {
        var correctedPos = new Vector3(_islandGlobalOrigins[_nextIslandIndex].x, _windCenter.transform.position.y, _islandGlobalOrigins[_nextIslandIndex].z);
        _windCenter.transform.position = correctedPos;
        _storm.SetStormCenterPosition(correctedPos);
    }
    
    
    public Vector3 GetNextIslandOrigin(int i) {
        
        _setWindCenter();
        return IslandGlobalOrigins[_nextIslandIndex];
    }

  
}
