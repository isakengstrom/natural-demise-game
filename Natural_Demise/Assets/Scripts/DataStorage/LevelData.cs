using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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