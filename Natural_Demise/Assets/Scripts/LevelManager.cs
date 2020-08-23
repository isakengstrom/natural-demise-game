using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    // Round parameters
    private readonly float countdownTilRound = 3f;
    private readonly float countdownTilRoundEnds = 4f;
    private readonly float countdownTilPortalActive = 3f;
    
    // Island Parameters
    private Vector3[] _islandGlobalOrigins;
    public static int islandAmount;
    private int _nextIslandIndex;
    public static int currentIslandIndex;

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
    private WindForce _windForce;
    private StormCloudController _stormCloud;

    // Player parameters 
    private GameObject _player;
    private BaseMotor _baseMotor;
    
    // UI parameters
    private InGameMenu _inGameMenu;
    

    private void Start() {
        _windCenter = GameObject.FindGameObjectWithTag("WindCenter");
        _storm = _windCenter.GetComponent<Storm>();
        _windDirection = _storm.GetComponent<WindDirection>();
        _windForce = _storm.GetComponent<WindForce>();
        _stormCloud = _storm.stormCloud;
        
        _player = GameObject.FindGameObjectWithTag("Player");
        _baseMotor = _player.GetComponent<BaseMotor>();

        _inGameMenu = GameObject.FindGameObjectWithTag("GameUI").GetComponent<InGameMenu>();

        _findIslandOrigins();
        _setUpPortals();
        _inGameMenu.UpdateText();

        Invoke(nameof(_activateCurrentPortal), countdownTilPortalActive);
    }
    
    //Invoke a full round
    private IEnumerator _fullRound() {
        
        yield return new WaitForSeconds(countdownTilRound);
        _startRound();
        
        yield return new WaitForSeconds(countdownTilRoundEnds);
        _endRound();
    }

    //Start the round
    private void _startRound() {
        _stormCloud.ShrinkStormCloud();
        _storm.ActivateStorm();
    }

    //End the round
    private void _endRound() {
        _stormCloud.ExpandStormCloud();
        _storm.DeactivateStorm();
        Invoke(nameof(_activateCurrentPortal), countdownTilPortalActive);
    }

    //Set up the portals for all the islands
    private void _setUpPortals() {
        _portalClones = new GameObject[islandAmount];
        _portalClonesScripts = new Portal[islandAmount];
        
        currentIslandIndex = 0;
        _nextIslandIndex = 1;
        for (var i = 0; i < islandAmount; i++) {
            if (i == 0) _portalChoice = portalBegin;
            else if (i == islandAmount - 1) _portalChoice = portalEnd;
            else _portalChoice = portalNormal;
            
            _portalClones[i] = (GameObject) Instantiate(_portalChoice, transform.GetChild(i));
            _portalClones[i].name = "portal" + i;

            _portalClonesScripts[i] = _portalClones[i].transform.GetChild(0).GetChild(0).GetComponent<Portal>();
        }
    }
    
    //Perform this method if the player uses a portal.
    public void SignalPlayerTeleportation(string portalName) {
        _checkClearedChapter(portalName);
        
        _setNextPortalIndex(portalName);
        _teleportPlayer();
        _updateWind();
        _inGameMenu.UpdateText();
        
        StartCoroutine(_fullRound());
        
        currentIslandIndex = _nextIslandIndex;
    }
    
    //Check if the player has cleared the chapter
    private void _checkClearedChapter(string portalName) {
        if (_portalClones[islandAmount - 1].name == portalName) {
            _saveRounds();
            _inGameMenu.ActivateWonMenu();
        }
    }
    

    //Perform this method if the player dies
    public void signalPlayerDeath() {
        _saveRounds();
        
        _inGameMenu.ActivateDeadMenu();
    }

    //Save the rounds as a PlayerPref
    private void _saveRounds() { 
        PlayerPrefs.SetInt("ROUNDSCOUNTER", currentIslandIndex);
    }

    //Active the portal to the next island
    private void _activateCurrentPortal() {
        _portalClonesScripts[currentIslandIndex].ActivatePortal();
    }
    
    //Set the next portal index
    private void _setNextPortalIndex(string portalName) {
        var isPortalIdFound = false;
        for (var i = 0; i < islandAmount; i++) {
            if (_portalClones[i].name != portalName) continue;
            _nextIslandIndex = (i+1) % islandAmount;
            isPortalIdFound = true;
        }

        if (!isPortalIdFound) _nextIslandIndex = 0;
    }

    //Teleport the player to the next island. 
    private void _teleportPlayer() {
        //Temporarily disable the characterController to not conflict with the change of position (teleport).
        _baseMotor.DisableController();
        _player.transform.position = _islandGlobalOrigins[_nextIslandIndex] + _teleportOffset;
        _baseMotor.EnableController();
    }

    //Teleport other objects that collides with the active portal
    public void TeleportOther(Collider other) {
        other.transform.position = _islandGlobalOrigins[_nextIslandIndex];
    }
    
    //Get the origins of all the islands
    private void _findIslandOrigins() {
        islandAmount = transform.childCount;
        _islandGlobalOrigins = new Vector3[islandAmount];
        
        for (var i = 0; i < islandAmount; i++) {
            _islandGlobalOrigins[i] = transform.GetChild(i).transform.position;
            transform.GetChild(i).name = "island" + i;
        }
    }

    //Set the center for the storm/wind
    private void _updateWind() {
        var correctedPos = new Vector3(_islandGlobalOrigins[_nextIslandIndex].x, _windCenter.transform.position.y, _islandGlobalOrigins[_nextIslandIndex].z);
        _windCenter.transform.position = correctedPos;
        _storm.SetStormCenterPosition(correctedPos);
        _windForce.IncreaseWindForce();
    }
}
