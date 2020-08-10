using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    private int _islandAmount;
    private int _nextIslandIndex;
    //private Vector3[] _islandLocalOrigins;
    private Vector3[] _islandGlobalOrigins;

    private GameObject _windCenter;
    private Storm _storm;
    private WindDirection _windDirection;
    
    [SerializeField] public GameObject portalBegin;
    [SerializeField] public GameObject portalNormal;
    [SerializeField] public GameObject portalEnd;
    private GameObject _portalChoice; 
    private GameObject[] _portalClones;
    
    
    private void Start() {
        _islandAmount = transform.childCount;

        _setUpPortals();
        _findIslandOrigins();

        _windCenter = GameObject.FindGameObjectWithTag("WindCenter");
        _storm = _windCenter.GetComponent<Storm>();
        _windDirection = _storm.GetComponent<WindDirection>();
    }

    public Vector3 GetNextTelePosition(string portalName) {
        var isPortalIdFound = false;
        for (var i = 0; i < _islandAmount; i++) {
            if (_portalClones[i].name != portalName) continue;
            _nextIslandIndex = (i+1) % _islandAmount;
            isPortalIdFound = true;
        }

        if (!isPortalIdFound) _nextIslandIndex = 0;
        
        _setWindCenter(_islandGlobalOrigins[_nextIslandIndex]); 
        return _islandGlobalOrigins[_nextIslandIndex];
    }

    private void _setWindCenter(Vector3 pos) {
        var correctedPos = new Vector3(pos.x, _windCenter.transform.position.y, pos.z);
        _windCenter.transform.position = correctedPos;
        _storm.SetStormCenterPosition(correctedPos);
    }
    
    private void _findIslandOrigins() {
        //_islandLocalOrigins = new Vector3[_islandAmount];
        _islandGlobalOrigins = new Vector3[_islandAmount];
        
        for (var i = 0; i < _islandAmount; i++) {
            //_islandLocalOrigins[i] = transform.GetChild(i).transform.localPosition;
            _islandGlobalOrigins[i] = transform.GetChild(i).transform.position;
        }
    }

    private void _setUpPortals() {
        _portalClones = new GameObject[_islandAmount];

        for (var i = 0; i < _islandAmount; i++) {
            if (i == 0) _portalChoice = portalBegin;
            else if (i == _islandAmount - 1) _portalChoice = portalEnd;
            else _portalChoice = portalNormal;
            
            _portalClones[i] = (GameObject) Instantiate(_portalChoice, transform.GetChild(i));
            _portalClones[i].name = "portal" + i;
        }
    }
}
