using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndMenu : MonoBehaviour {
    public static bool isGamePaused = false;
    public GameObject pauseMenuUI;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isGamePaused) Resume();
            else Pause();
        }
    }
    
    /*
    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    */

    public void Restart() {
        
    }

    public void Pause() {
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
