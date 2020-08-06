using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    private int _islandAmount;
    private int _nextIslandIndex;
    private Vector3[] _islandLocalOrigins;
    private Vector3[] _islandGlobalOrigins;
        
    private GameObject _windCenter;
    private Storm _storm;
    private WindDirection _windDirection;
    
    [SerializeField] public GameObject portal;
    private GameObject _portalClone;

    private void Start() {
        _setUpPortals();
        _findIslandOrigins();

        _windCenter = GameObject.FindGameObjectWithTag("WindCenter");
        _storm = _windCenter.GetComponent<Storm>();
        _windDirection = _storm.GetComponent<WindDirection>();

        _nextIslandIndex = 1;
    }

    public Vector3 GetNextTelePosition() {
        _setWindCenter(_islandGlobalOrigins[_nextIslandIndex % 7]); 
        return _islandLocalOrigins[_nextIslandIndex++ % 7];
    }

    private void _setWindCenter(Vector3 pos) {
        var correctedPos = new Vector3(pos.x, _windCenter.transform.position.y, pos.z);
        _windCenter.transform.position = correctedPos;
        _storm.SetStormCenterPosition(correctedPos);
        
        //_windCenter.transform.parent = transform.GetChild(_nextIslandIndex).transform;
    }
    
    private void _findIslandOrigins() {
        _islandAmount = transform.childCount;
        _islandLocalOrigins = new Vector3[_islandAmount];
        _islandGlobalOrigins = _islandLocalOrigins;
        for (var i = 0; i < _islandAmount; i++) {
            _islandLocalOrigins[i] = transform.GetChild(i).transform.localPosition;
            _islandGlobalOrigins[i] = transform.GetChild(i).transform.position;
        }
    }

    private void _setUpPortals() {
        foreach (Transform child in transform) {
            _portalClone = (GameObject) Instantiate(portal,child);
        }
    }
    
}
