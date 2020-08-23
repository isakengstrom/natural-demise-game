using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameMenu : MonoBehaviour {
    public static bool isGamePaused = false;
    
    //UI parameters
    public GameObject pauseMenuUI;
    public GameObject activeUI;
    public GameObject deadMenuUI;
    public GameObject miscUI;
    public GameObject wonMenuUI;
    
    private TextMeshProUGUI _roundText;
    private TextMeshProUGUI _roundTextPaused;

    private AudioSource[] _allAudioSources;
    private bool _wasAudioPlaying;
    
    
    private void Start() {
        _roundText = GameObject.Find("RoundTextActiveMod").GetComponent<TextMeshProUGUI>();
        _roundTextPaused = GameObject.Find("RoundTextPaused").GetComponent<TextMeshProUGUI>();
        
        _allAudioSources = FindObjectsOfType<AudioSource>() as AudioSource[];
        _wasAudioPlaying = false;
        
        Resume();
    }

    //Update the text when the player makes progress
    public void UpdateText() {
        _roundTextPaused.SetText($"ROUND {LevelManager.currentIslandIndex + 1} / {LevelManager.islandAmount}");
        _roundText.SetText($"{LevelManager.currentIslandIndex + 1} / {LevelManager.islandAmount}");
    }

    //Active the UI which should be displayed when the player dies
    public void ActivateDeadMenu() {
        StartCoroutine(_delayOnDeath());
    }

    //Delay the UI for a bit to let animations play out
    private IEnumerator _delayOnDeath() {
        yield return new WaitForSeconds(1.5f);
        deadMenuUI.SetActive(true);
        miscUI.SetActive(true);
        activeUI.SetActive(false);
        
        _stopTime();
    }

    //Activate the UI which should be displayed when the players clears a chapter.
    public void ActivateWonMenu() {
        wonMenuUI.SetActive(true);
        activeUI.SetActive(false);
        
        _stopTime();
    }

    //Triggered when the user presses the "play" button from in game
    public void Resume() {
        pauseMenuUI.SetActive(false);
        deadMenuUI.SetActive(false);
        miscUI.SetActive(false);
        wonMenuUI.SetActive(false);
        activeUI.SetActive(true);
        
        _startTime();
    }
    
    //Triggered when the player presses the "pause" button
    public void Pause() {
        pauseMenuUI.SetActive(true);
        miscUI.SetActive(true);
        activeUI.SetActive(false);
        
        _stopTime();
    }

    //Reset the chapter, triggered from the "reset" button
    public void Reset() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    //Load the menu, triggered from the "to menu" button
    public void LoadMenu() {
        Destroy(GameObject.FindGameObjectWithTag("MovePostLoad"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    //Stop the game time
    private void _stopTime() {
        Time.timeScale = 0f;
        isGamePaused = true;
        _pauseAudio();
    }

    //Start the game time
    private void _startTime() {
        Time.timeScale = 1f;
        isGamePaused = false;
        _startAudio();
    }
    
    private void _pauseAudio() {
        foreach(var audioS  in _allAudioSources) {
            if (audioS.isPlaying) {
                audioS.Pause();
                _wasAudioPlaying = true;
            }
        }
    }

    private void _startAudio() {
        foreach(var audioS  in _allAudioSources) {
            if (_wasAudioPlaying) {
                audioS.Play();
                _wasAudioPlaying = false;
            }
                
        }
    }
    

    public void QuitGame() {
        print("Quitting game..");
        Application.Quit();
    }
}
