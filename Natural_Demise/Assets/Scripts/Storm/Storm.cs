using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour {
    private bool _isStormActive; 
    
    private WindDirection _direction;
    private WindForce _force;

    private GameObject _spawnPlane;
    private readonly float _spawnPlaneWidth = 40.0f;

    //Declaration for wind particles
    private int _particleCounter;
    private float _particleSpawnTimer;
    private Vector3 _particleSpawnOffset;
    public GameObject particle;
    private GameObject _particleClone;

    //Declaration for debris (stones and such)
    //private int debrisCounter;
    private float _debrisSpawnTimer;
    private Vector3 _debrisSpawnOffset;
    public GameObject debris;
    private GameObject _debrisClone;

    //Declaration for whirlwinds
    private float _whirlwindSpawnTimer;
    private Vector3 _whirlwindSpawnOffset;
    public GameObject whirlwind;
    private GameObject _whirlwindClone;
    public StormCloudController stormCloud;
    
    private void Awake() {
        _direction = gameObject.AddComponent<WindDirection>();
        _force = gameObject.AddComponent<WindForce>();
    }

    private void Start() {
        stormCloud = GetComponent<StormCloudController>();
        _isStormActive = false;

        _particleSpawnTimer = 2.0f;
        _particleSpawnOffset = new Vector3(0.0f,1.0f,0.0f);

        _debrisSpawnTimer = 2.0f;
        _debrisSpawnOffset = new Vector3(0.0f, 1.0f, 0.0f);
        
        _whirlwindSpawnTimer = 2.0f;
        _whirlwindSpawnOffset = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void ActivateStorm() {
        _isStormActive = true;
        
        _invokeStorm();
    }

    public void DeactivateStorm() {
        _isStormActive = false;
    }
    
    private void _invokeStorm() {
        Invoke(nameof(_addDebris), _debrisSpawnTimer);
        Invoke(nameof(_addWhirlWinds), _whirlwindSpawnTimer);
        Invoke(nameof(_addParticles), _particleSpawnTimer);
    }

    private void _addDebris() {
        _debrisSpawnTimer = Random.Range(1f, 2f);

        _debrisSpawnOffset.x = Random.Range(-_spawnPlaneWidth / 2, _spawnPlaneWidth / 2);
        _debrisSpawnOffset.z = Random.Range(-_spawnPlaneWidth / 2, _spawnPlaneWidth / 2);

        _debrisClone = (GameObject)Instantiate(debris, _direction.GetWindStartingPoint() + _debrisSpawnOffset + new Vector3(3.0f + Random.Range(0.0f, 4.0f), 0.0f, 3.0f + Random.Range(0.0f, 4.0f)), debris.transform.rotation, transform);
        Destroy(_debrisClone, 20);

        if (_isStormActive) Invoke(nameof(_addDebris), _debrisSpawnTimer);
    }
    
    private void _addWhirlWinds() {
        _whirlwindSpawnTimer = Random.Range(1f, 2f);

        _whirlwindSpawnOffset.x = Random.Range(-_spawnPlaneWidth / 2, _spawnPlaneWidth / 2);
        _whirlwindSpawnOffset.z = Random.Range(-_spawnPlaneWidth / 2, _spawnPlaneWidth / 2);
        
        
        _whirlwindClone = (GameObject) Instantiate(whirlwind, _direction.GetWindStartingPoint() + _whirlwindSpawnOffset + new Vector3(3.0f + Random.Range(0.0f, 4.0f), 0.0f, 3.0f + Random.Range(0.0f, 4.0f)), whirlwind.transform.rotation, transform);
        Destroy(_whirlwindClone, 20);

        if (_isStormActive) Invoke(nameof(_addWhirlWinds), _whirlwindSpawnTimer);
    }

    private void _addParticles() {
        _particleSpawnTimer = Random.Range(0.2f, 0.3f);

        _particleSpawnOffset.x = Random.Range(-_spawnPlaneWidth / 2, _spawnPlaneWidth / 2);
        _particleSpawnOffset.z = Random.Range(-_spawnPlaneWidth / 2, _spawnPlaneWidth / 2);

        _particleCounter = (int)Random.Range(1.0f, 4.0f);

        for(var i = 0; i < _particleCounter; ++i) {
            _particleClone = (GameObject)Instantiate(particle, _direction.GetWindStartingPoint() + _particleSpawnOffset + new Vector3(3.0f + _particleCounter + Random.Range(0.0f,4.0f), 0.0f, 3.0f + _particleCounter + Random.Range(0.0f, 4.0f)), particle.transform.rotation, transform);
            Destroy(_particleClone, 3);
        }

        if (_isStormActive) Invoke(nameof(_addParticles), _particleSpawnTimer);
    }

    public void SetStormCenterPosition(Vector3 pos) {
        _direction.StormCenterPosition = pos;
    }
  
}
