using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Following the tutorial provided by Brackeys on Youtube. 
//Source: https://www.youtube.com/watch?v=JivuXdrIHK0 
public class PauseMenu : MonoBehaviour {
    public static bool isGamePaused = false;
    public GameObject pauseMenuUI;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isGamePaused) Resume();
            else _pause();
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    private void _pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void LoadMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame() {
        print("Quitting game..");
        Application.Quit();
    }
}
