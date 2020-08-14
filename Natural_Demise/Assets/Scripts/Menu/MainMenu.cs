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
   private int _currentLevel;
   public static GameObject currentLevelIsland;
   
   public static int roundAmount;
   public int[] roundHighscores;

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
      _currentLevel = 0;
      roundAmount = 10;

      _levels = new[] {level0, level1, level2};
      
      _leftButton = GameObject.Find("LeftButton");
      _rightButton = GameObject.Find("RightButton");

      _levelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
      _roundText = GameObject.Find("RoundText").GetComponent<TextMeshProUGUI>();
      
      
      _updateUI();
      _instantiateLevels();
      _activateCurrentIsland();
   }


   private void _updateUI() {
      _checkArrowStatus();

      _levelText.SetText($"Level: {_currentLevel + 1} / {_levelsAmount}");
      _roundText.SetText($"Round: {_getRoundHighscore()}");
   }

   private string _getRoundHighscore() {
   

      return "foo";
   }

   private void _instantiateLevels() {
      _levelClones = new GameObject[3];
     
      
      for (var i = 0; i < _levelsAmount; i++) {
         _levelClones[i] = Instantiate(_levels[i], Vector3.zero, _levels[i].transform.rotation);
         _levelClones[i].SetActive(false);
      }
      
      //LoadLevels();
      
   }

   /*
   public void LoadLevels() {
      roundHighscores = new int[_levelsAmount];

      LevelData data = SaveSystem.LoadLevels();

      roundHighscores = data.roundHighscores;
   }
   
   public void SaveLevels() {
      SaveSystem.SaveLevels(this);
   }
   */
   

   private void _activateCurrentIsland() {
      for (var i = 0; i < _levelsAmount; i++) {
         if (i == _currentLevel) _levelClones[i].SetActive(true);
         else _levelClones[i].SetActive(false);
      }
   }
   
   public void PlayGame() {
      currentLevelIsland = _levelClones[_currentLevel];
      currentLevelIsland.tag = "DestroyPostLoad";
      DontDestroyOnLoad(currentLevelIsland);
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void LeftArrow() {
      if (_currentLevel > 0) {
         _currentLevel--;
         _activateCurrentIsland();
      }

      _updateUI();
   }

   public void RightArrow() {
      if (_currentLevel < _levelsAmount - 1) {
         _currentLevel++;
         _activateCurrentIsland();
      }

      _updateUI();
   } 

   private void _checkArrowStatus() {
      _leftArrowState = true;
      _rightArrowState = true;
      if (_currentLevel >= _levelsAmount - 1) _rightArrowState = false;
      else if (_currentLevel <= 0) _leftArrowState = false;

      _leftButton.SetActive(_leftArrowState);
      _rightButton.SetActive(_rightArrowState);
   }
   

   public void QuitGame() {
      print("Quitting game..");
      Application.Quit();
   }
    
}
