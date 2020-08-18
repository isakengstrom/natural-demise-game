using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//Following the tutorial provided by Brackeys on Youtube. 
//Source: https://www.youtube.com/watch?v=zc8ac_qUXQY
public class MainMenu : MonoBehaviour {
   private int _levelsAmount;
   public static int currentLevel;
   public static GameObject currentLevelIsland;
   
   public static int roundAmount;
   public static int currentRound;
   public int[] roundHighscores;
   private static int _saveCounter;

   [SerializeField] public GameObject level0;
   [SerializeField] public GameObject level1;
   [SerializeField] public GameObject level2;

   private GameObject[] _levels;
   private GameObject[] _levelClones;

   private GameObject _leftButton;
   private GameObject _rightButton;

   private bool _leftArrowState;
   private bool _rightArrowState;

   private TextMeshProUGUI _levelText;
   private TextMeshProUGUI _roundText;
   
   private void Start() {
      _levelsAmount = 3;
      currentLevel = 0;
      currentRound = 0;
      roundAmount = 10;

      _levels = new[] {level0, level1, level2};
      
      _leftButton = GameObject.Find("LeftButton");
      _rightButton = GameObject.Find("RightButton");

      _levelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
      _roundText = GameObject.Find("RoundText").GetComponent<TextMeshProUGUI>();
      
      roundHighscores = new int[_levelsAmount];
      
      if(_isFirstTimeOpeningGame())
         SaveLevels();
      
      LoadLevels();
      
      _updateScore();
      _updateUI();
      _instantiateLevels();
      _activateCurrentIsland();
   }

   private void _updateScore() {
      var lastPlayedLevel = PlayerPrefs.GetInt("LASTPLAYEDLEVEL", -1);
      
      if (lastPlayedLevel >= 0 && lastPlayedLevel < _levelsAmount) {
         currentLevel = lastPlayedLevel;
         var lastPlayedLevelRoundCounter = PlayerPrefs.GetInt("ROUNDSCOUNTER", 0);

         if (lastPlayedLevelRoundCounter > roundHighscores[lastPlayedLevel]) {
            roundHighscores[lastPlayedLevel] = lastPlayedLevelRoundCounter;
            SaveLevels();
         }
      }
   }

   private bool _isFirstTimeOpeningGame() {
      if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1) {
         Debug.Log("First Time Opening");
         
         PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);

         return true;
      }
      else {
         Debug.Log("NOT First Time Opening");

         return false;
      }
   }

   public void LoadLevels() {
      
      LevelData data = SaveSystem.LoadLevels();

      roundHighscores = data.roundHighscores;
   }
   
   public void SaveLevels() {
      SaveSystem.SaveLevels(this);
   }
   

   private void _updateUI() {
      _checkArrowStatus();
      print(roundHighscores.Length);
      print(currentLevel);

      _levelText.SetText($"Level: {currentLevel + 1} / {_levelsAmount}");
      _roundText.SetText($"Round: {roundHighscores[currentLevel] + 1} / {roundAmount}");
   }
   
   private void _instantiateLevels() {
      _levelClones = new GameObject[3];
      
      for (var i = 0; i < _levelsAmount; i++) {
         _levelClones[i] = Instantiate(_levels[i], Vector3.zero, _levels[i].transform.rotation);
         _levelClones[i].SetActive(false);
      }
   }

   private void _activateCurrentIsland() {
      for (var i = 0; i < _levelsAmount; i++) {
         if (i == currentLevel) _levelClones[i].SetActive(true);
         else _levelClones[i].SetActive(false);
      }
   }
   
   public void PlayGame() {
      PlayerPrefs.SetInt("LASTPLAYEDLEVEL", currentLevel);
      
      
      currentLevelIsland = _levelClones[currentLevel];
      currentLevelIsland.tag = "DestroyPostLoad";
      DontDestroyOnLoad(currentLevelIsland);
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void LeftArrow() {
      if (currentLevel > 0) {
         currentLevel--;
         _activateCurrentIsland();
      }

      _updateUI();
   }

   public void RightArrow() {
      if (currentLevel < _levelsAmount - 1) {
         currentLevel++;
         _activateCurrentIsland();
      }

      _updateUI();
   } 

   private void _checkArrowStatus() {
      _leftArrowState = true;
      _rightArrowState = true;
      if (currentLevel >= _levelsAmount - 1) _rightArrowState = false;
      else if (currentLevel <= 0) _leftArrowState = false;

      _leftButton.SetActive(_leftArrowState);
      _rightButton.SetActive(_rightArrowState);
   }
   

   public void QuitGame() {
      print("Quitting game..");
      Application.Quit();
   }
    
}
