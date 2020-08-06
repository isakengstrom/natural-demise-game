using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour {
    private WindDirection _direction;
    private WindForce _force;

    private GameObject _spawnPlane;
    private readonly float _spawnPlaneWidth = 40.0f;


    private int _particleCounter;
    private float _particleSpawnTimer;
    private Vector3 _particleSpawnOffset;
    public GameObject particle;
    private GameObject _particleClone;

    //private int debrisCounter;
    private float _debrisSpawnTimer;
    private Vector3 _debrisSpawnOffset;
    public GameObject debris;
    private GameObject _debrisClone;


    private void Awake() {
        _direction = gameObject.AddComponent<WindDirection>();
        _force = gameObject.AddComponent<WindForce>();
    }

    private void Start() {

        _particleSpawnTimer = 2.0f;
        _particleSpawnOffset = new Vector3(0.0f,1.0f,0.0f);

        _debrisSpawnTimer = 2.0f;
        _debrisSpawnOffset = new Vector3(0.0f, 1.0f, 0.0f);

        /*
        //Initialize a plane for debugging, helps to see how big the spawn area for the wind is.
        _spawnPlane = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _spawnPlane.transform.localScale = new Vector3(_spawnPlaneWidth, 10.0f, 1.0f);
        _spawnPlane.GetComponent<Collider>().enabled = false;
        */

        

        Invoke(nameof(AddParticles), _particleSpawnTimer);
        Invoke(nameof(AddDebris), _particleSpawnTimer);
    }

    private void AddDebris() {
        _debrisSpawnTimer = Random.Range(1f, 2f);

        _debrisSpawnOffset.x = Random.Range(-_spawnPlaneWidth / 2, _spawnPlaneWidth / 2);
        _debrisSpawnOffset.z = Random.Range(-_spawnPlaneWidth / 2, _spawnPlaneWidth / 2);

        /*
        debrisCounter = (int)Random.Range(1.0f, 4.0f);
        
        for (int i = 0; i < debrisCounter; ++i) {
            debrisClone = (GameObject)Instantiate(debris, direction.getWindOrigin() + debrisSpawnOffset + new Vector3(3.0f + debrisCounter + Random.Range(0.0f, 4.0f), 0.0f, 3.0f + debrisCounter + Random.Range(0.0f, 4.0f)), debris.transform.rotation);
            debrisClone.transform.parent = gameObject.transform;
            Destroy(debrisClone, 3);
        }
        */

        _debrisClone = (GameObject)Instantiate(debris, _direction.GetWindStartingPoint() + _debrisSpawnOffset + new Vector3(3.0f + Random.Range(0.0f, 4.0f), 0.0f, 3.0f + Random.Range(0.0f, 4.0f)), debris.transform.rotation);
        _debrisClone.transform.parent = gameObject.transform;
        Destroy(_debrisClone, 20);

        Invoke(nameof(AddDebris), _debrisSpawnTimer);
    }

    private void AddParticles() {
        _particleSpawnTimer = Random.Range(0.2f, 0.3f);

        _particleSpawnOffset.x = Random.Range(-_spawnPlaneWidth / 2, _spawnPlaneWidth / 2);
        _particleSpawnOffset.z = Random.Range(-_spawnPlaneWidth / 2, _spawnPlaneWidth / 2);

        _particleCounter = (int)Random.Range(1.0f, 4.0f);

        for(var i = 0; i < _particleCounter; ++i) {
            _particleClone = (GameObject)Instantiate(particle, _direction.GetWindStartingPoint() + _particleSpawnOffset + new Vector3(3.0f + _particleCounter + Random.Range(0.0f,4.0f), 0.0f, 3.0f + _particleCounter + Random.Range(0.0f, 4.0f)), particle.transform.rotation);
            _particleClone.transform.parent = gameObject.transform;
            Destroy(_particleClone, 3);
        }

        Invoke(nameof(AddParticles), _particleSpawnTimer);
    }

    private void Update() {
        
        
 
        
        /*
        //Update the spawnPlane.
        _spawnPlane.transform.position = _direction.GetWindOrigin();
        _spawnPlane.transform.rotation = Quaternion.LookRotation(_windDirection, Vector3.up);
        */
    }

    public void SetStormCenterPosition(Vector3 pos) {
        _direction.StormCenterPosition = pos;
    }
    /*
    public void windDirectionChange() {
        Debug.Log("Major change in wind direction.");
        CancelInvoke("DestroyParticle");
        Invoke("DestroyParticle", 3.0f);
    }
    */
}
