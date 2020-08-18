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
        
        if(score > roundHighscores[MainMenu.currentLevel])
            roundHighscores[MainMenu.currentLevel] = score;
    }
    
}
/*
[Serializable]
public class SystemData {
    public int saveCounter;

    public SystemData(MainMenu mm) {
        SystemData data = SaveSystem.LoadSystemData();

        saveCounter = data.saveCounter;
    }
}
*/