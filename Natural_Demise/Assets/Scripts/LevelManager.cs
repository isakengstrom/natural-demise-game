using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    // Round parameters
    private readonly float countdownTilRound = 3f;
    private readonly float countdownTilRoundEnds = 15f;
    private readonly float countdownTilPortalActive = 3f;
    
    // Island Parameters
    private Vector3[] _islandGlobalOrigins;
    private int _islandAmount;
    private int _nextIslandIndex;
    private int _currentIslandIndex;
    public int roundData;
    
    // Portal parameters
    [SerializeField] public GameObject portalBegin;
    [SerializeField] public GameObject portalNormal;
    [SerializeField] public GameObject portalEnd;
    
    private GameObject _portalChoice; 
    private GameObject[] _portalClones;
    private Portal[] _portalClonesScripts;
    private readonly Vector3 _teleportOffset = new Vector3(0f,15f,0f);

    // Storm parameters
    private GameObject _windCenter;
    private Storm _storm;
    private WindDirection _windDirection;
    private StormCloudController _stormCloud;

    // Player parameters 
    private GameObject _player;
    private BaseMotor _baseMotor;
    
    //Misc
    //private GameObject _signalObject;

    private void Start() {
        _windCenter = GameObject.FindGameObjectWithTag("WindCenter");
        _storm = _windCenter.GetComponent<Storm>();
        _windDirection = _storm.GetComponent<WindDirection>();
        _stormCloud = _storm.stormCloud;
        
        _player = GameObject.FindGameObjectWithTag("Player");
        _baseMotor = _player.GetComponent<BaseMotor>();
        
        //_instantiateSignalObject();

        _findIslandOrigins();
        _setUpPortals();

        Invoke(nameof(_activateCurrentPortal), countdownTilPortalActive);
    }

    /*
    private void _instantiateSignalObject() {
        _signalObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _signalObject.GetComponent<Collider>().enabled = false;
        _signalObject.GetComponent<Renderer>().enabled = false;
        _signalObject.name = "SignalObject";
        _signalObject.tag = "DestroyPostLoad";
    }
    */
    
    private IEnumerator _fullRound() {
        
        yield return new WaitForSeconds(countdownTilRound);
        _startRound();
        
        yield return new WaitForSeconds(countdownTilRoundEnds);
        _endRound();
    }

    private void _startRound() {
        _stormCloud.ShrinkStormCloud();
        _storm.ActivateStorm();
        print("roundstart");
    }

    private void _endRound() {
        _stormCloud.ExpandStormCloud();
        _storm.DeactivateStorm();
        Invoke(nameof(_activateCurrentPortal), countdownTilPortalActive);
    }

    private void _setUpPortals() {
        _portalClones = new GameObject[_islandAmount];
        _portalClonesScripts = new Portal[_islandAmount];
        
        _currentIslandIndex = 0;
        _nextIslandIndex = 1;
        for (var i = 0; i < _islandAmount; i++) {
            if (i == 0) _portalChoice = portalBegin;
            else if (i == _islandAmount - 1) _portalChoice = portalEnd;
            else _portalChoice = portalNormal;
            
            _portalClones[i] = (GameObject) Instantiate(_portalChoice, transform.GetChild(i));
            _portalClones[i].name = "portal" + i;

            _portalClonesScripts[i] = _portalClones[i].transform.GetChild(0).GetChild(0).GetComponent<Portal>();
        }
    }
    
    public void SignalPlayerTeleportation(string portalName) {
        _checkLevelWin(portalName);
        
        _setNextPortalIndex(portalName);
        _teleportPlayer();
        _setWindCenter();

        StartCoroutine(_fullRound());
        
        _currentIslandIndex = _nextIslandIndex;
    }

    private void _checkLevelWin(string portalName) {
        if (_portalClones[_islandAmount - 1].name == portalName) {
            _saveRounds();
            //TODO: TRIGGER win GUI
        }
    }
    

    public void signalPlayerDeath() {
        _saveRounds();
        
        //TODO: TRIGGER loose GUI
    }

    private void _saveRounds() { 
        PlayerPrefs.SetInt("ROUNDSCOUNTER", _currentIslandIndex);
    }

    public void ToMainMenu() {
        //DontDestroyOnLoad(_signalObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void _activateCurrentPortal() {
        _portalClonesScripts[_currentIslandIndex].ActivatePortal();
    }
    private void _setNextPortalIndex(string portalName) {
        var isPortalIdFound = false;
        for (var i = 0; i < _islandAmount; i++) {
            if (_portalClones[i].name != portalName) continue;
            _nextIslandIndex = (i+1) % _islandAmount;
            isPortalIdFound = true;
        }

        if (!isPortalIdFound) _nextIslandIndex = 0;
    }

    private void _teleportPlayer() {
        //Temporarily disable the characterController to not conflict with the change of position (teleport).
        _baseMotor.DisableController();
        _player.transform.position = _islandGlobalOrigins[_nextIslandIndex] + _teleportOffset;
        _baseMotor.EnableController();
    }

    public void TeleportOther(Collider other) {
        other.transform.position = _islandGlobalOrigins[_nextIslandIndex];
    }
    
    private void _findIslandOrigins() {
        _islandAmount = transform.childCount;
        _islandGlobalOrigins = new Vector3[_islandAmount];
        
        for (var i = 0; i < _islandAmount; i++) {
            _islandGlobalOrigins[i] = transform.GetChild(i).transform.position;
            transform.GetChild(i).name = "island" + i;
        }
    }

    private void _setWindCenter() {
        var correctedPos = new Vector3(_islandGlobalOrigins[_nextIslandIndex].x, _windCenter.transform.position.y, _islandGlobalOrigins[_nextIslandIndex].z);
        _windCenter.transform.position = correctedPos;
        _storm.SetStormCenterPosition(correctedPos);
    }
}
