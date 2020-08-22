using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
   private int _chaptersAmount;
   public static int currentChapter;
   public static GameObject currentChapterIsland;
   
   public static int roundAmount;
   public int[] roundHighscores;
   private static int _saveCounter;

   //Chapter parameters 
   [SerializeField] public GameObject chapter0;
   [SerializeField] public GameObject chapter1;
   [SerializeField] public GameObject chapter2;
   
   private GameObject[] _chapters;
   private GameObject[] _chapterClones;

   //UI parameters
   private GameObject _leftButton;
   private GameObject _rightButton;
   private bool _leftArrowState;
   private bool _rightArrowState;
   private TextMeshProUGUI _chapterText;
   private TextMeshProUGUI _roundText;
   
   private void Start() {
      _chaptersAmount = 3;
      currentChapter = 0;
      roundAmount = 10;

      _chapters = new[] {chapter0, chapter1, chapter2};
      
      _leftButton = GameObject.Find("LeftButton");
      _rightButton = GameObject.Find("RightButton");

      _chapterText = GameObject.Find("ChapterText").GetComponent<TextMeshProUGUI>();
      _roundText = GameObject.Find("RoundText").GetComponent<TextMeshProUGUI>();
      
      roundHighscores = new int[_chaptersAmount];
      
      //Initialize a save if it's the first time the user plays the game.
      if(_isFirstTimeOpeningGame())
         SaveChapters();
      
      //Load the users progress
      LoadChapters();
      
      _updateScore();
      _updateUI();
      _instantiateChapters();
      _activateCurrentIsland();
   }

   private void _updateScore() {
      var lastPlayedLevel = PlayerPrefs.GetInt("LASTPLAYEDLEVEL", -1);
      
      if (lastPlayedLevel >= 0 && lastPlayedLevel < _chaptersAmount) {
         currentChapter = lastPlayedLevel;
         var lastPlayedLevelRoundCounter = PlayerPrefs.GetInt("ROUNDSCOUNTER", 0);

         if (lastPlayedLevelRoundCounter > roundHighscores[lastPlayedLevel]) {
            roundHighscores[lastPlayedLevel] = lastPlayedLevelRoundCounter;
            SaveChapters();
         }
      }
   }

   //Check if it's the first time the user opens the game (device specific)
   private bool _isFirstTimeOpeningGame() {
      if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1) {
         PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);
         return true;
      }
      
      return false;
   }

   //Load progress
   public void LoadChapters() {
      
      LevelData data = SaveSystem.LoadLevels();

      roundHighscores = data.roundHighscores;
   }
   
   //Save progress
   public void SaveChapters() {
      SaveSystem.SaveLevels(this);
   }
   
   
   private void _updateUI() {
      _checkArrowStatus();

      _chapterText.SetText($"CHAPTER: {currentChapter + 1} / {_chaptersAmount}");
      _roundText.SetText($"ROUND: {roundHighscores[currentChapter] + 1} / {roundAmount}");
   }
   
   private void _instantiateChapters() {
      _chapterClones = new GameObject[3];
      
      for (var i = 0; i < _chaptersAmount; i++) {
         _chapterClones[i] = Instantiate(_chapters[i], Vector3.zero, _chapters[i].transform.rotation);
         _chapterClones[i].SetActive(false);
      }
   }

   private void _activateCurrentIsland() {
      for (var i = 0; i < _chaptersAmount; i++) {
         if (i == currentChapter) _chapterClones[i].SetActive(true);
         else _chapterClones[i].SetActive(false);
      }
   }
   
   public void PlayGame() {
      PlayerPrefs.SetInt("LASTPLAYEDLEVEL", currentChapter);

      currentChapterIsland = _chapterClones[currentChapter];
      currentChapterIsland.tag = "MovePostLoad";
      DontDestroyOnLoad(currentChapterIsland);
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void LeftArrow() {
      if (currentChapter > 0) {
         currentChapter--;
         _activateCurrentIsland();
      }

      _updateUI();
   }

   public void RightArrow() {
      if (currentChapter < _chaptersAmount - 1) {
         currentChapter++;
         _activateCurrentIsland();
      }

      _updateUI();
   } 

   private void _checkArrowStatus() {
      _leftArrowState = true;
      _rightArrowState = true;
      if (currentChapter >= _chaptersAmount - 1) _rightArrowState = false;
      else if (currentChapter <= 0) _leftArrowState = false;

      _leftButton.SetActive(_leftArrowState);
      _rightButton.SetActive(_rightArrowState);
   }
   

   public void QuitGame() {
      print("Quitting game..");
      Application.Quit();
   }
    
}
