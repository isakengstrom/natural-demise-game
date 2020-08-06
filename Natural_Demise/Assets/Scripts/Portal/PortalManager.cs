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
    
    [SerializeField] public GameObject portal;
    private GameObject[] _portalClones;
    private GameObject _portalClone;

    private void Start() {
        _islandAmount = transform.childCount;

        _setUpPortals();
        _findIslandOrigins();

        _windCenter = GameObject.FindGameObjectWithTag("WindCenter");
        _storm = _windCenter.GetComponent<Storm>();
        _windDirection = _storm.GetComponent<WindDirection>();

        _nextIslandIndex = 1;
    }

    public Vector3 GetNextTelePosition(string portalName) {
        var isPortalIdFound = false;
        for (var i = 0; i < _portalClones.Length; i++) {
            if (portalName == _portalClones[i].name) {
                _nextIslandIndex = (i+1) % _islandAmount;
                isPortalIdFound = true;
            }
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

        for (var i = 0; i < _portalClones.Length; i++) {
            _portalClones[i] = (GameObject) Instantiate(portal, transform.GetChild(i));
            _portalClones[i].name = "portal" + i;
        }
    }
}
