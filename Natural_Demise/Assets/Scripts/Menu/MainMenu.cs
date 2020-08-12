using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Following the tutorial provided by Brackeys on Youtube. 
//Source: https://www.youtube.com/watch?v=zc8ac_qUXQY
public class MainMenu : MonoBehaviour {
   public void PlayGame() {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void QuitGame() {
      print("Quitting game..");
      Application.Quit();
   }
    
}
