using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The serializable part of the save system using binary formatting to store game data
//Following the tutorial made by Brackeys: https://www.youtube.com/watch?v=XOjd_qU2Ido
[Serializable]
public class LevelData {
    public int[] roundHighscores;

    public LevelData(MainMenu mm) {
        roundHighscores = mm.roundHighscores;
    }

    public LevelData(int score) {
        LevelData data = SaveSystem.LoadLevels();

        roundHighscores = data.roundHighscores;
        
        if(score > roundHighscores[MainMenu.currentChapter])
            roundHighscores[MainMenu.currentChapter] = score;
    }
}