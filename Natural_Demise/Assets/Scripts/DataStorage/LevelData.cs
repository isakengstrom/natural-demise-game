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
}
