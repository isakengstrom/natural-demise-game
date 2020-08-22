using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameMenu : MonoBehaviour {
    public static bool isGamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject activeUI;
    public GameObject deadMenuUI;
    public GameObject miscUI;
    public GameObject wonMenuUI;
    
    private TextMeshProUGUI _roundText;
    private TextMeshProUGUI _roundTextPaused;

    private void Start() {
        _roundText = GameObject.Find("RoundTextActiveMod").GetComponent<TextMeshProUGUI>();
        _roundTextPaused = GameObject.Find("RoundTextPaused").GetComponent<TextMeshProUGUI>();
        
        Resume();
    }

    public void UpdateText() {
        _roundTextPaused.SetText($"ROUND {LevelManager.currentIslandIndex + 1} / {LevelManager.islandAmount}");
        _roundText.SetText($"{LevelManager.currentIslandIndex + 1} / {LevelManager.islandAmount}");
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        deadMenuUI.SetActive(false);
        miscUI.SetActive(false);
        wonMenuUI.SetActive(false);
        activeUI.SetActive(true);
        
        _startTime();
    }

    public void ActivateDeadMenu() {
        StartCoroutine(_delayOnDeath());
    }

    private IEnumerator _delayOnDeath() {
        yield return new WaitForSeconds(1.5f);
        deadMenuUI.SetActive(true);
        miscUI.SetActive(true);
        activeUI.SetActive(false);
        
        _stopTime();
    }

    public void ActivateWonMenu() {
        wonMenuUI.SetActive(true);
        activeUI.SetActive(false);
        
        _stopTime();
    }

    public void Pause() {
        pauseMenuUI.SetActive(true);
        miscUI.SetActive(true);
        activeUI.SetActive(false);
        
        _stopTime();
    }

    private void _stopTime() {
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    private void _startTime() {
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    
    public void Reset() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadMenu() {
        Destroy(GameObject.FindGameObjectWithTag("MovePostLoad"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame() {
        print("Quitting game..");
        Application.Quit();
    }
}
