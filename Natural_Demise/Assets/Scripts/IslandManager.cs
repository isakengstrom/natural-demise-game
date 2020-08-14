using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour {

    private GameObject[] _islands;
    private GameObject[] _levels;
    
    private void Start() {
        
        _setUpIslands();

        _destroyPostLoad();
    }

    private void _destroyPostLoad() {
        Destroy(GameObject.FindGameObjectWithTag("DestroyPostLoad"));
    }

    private void _setUpIslands() {
        
        _islands = new GameObject[MainMenu.roundAmount];
        var pos = Vector3.zero;
        pos.y = -6.5f * 2.25f;
        for (int i = 0; i < MainMenu.roundAmount; i++) {
            
            _islands[i] = (GameObject) Instantiate(MainMenu.currentLevelIsland, transform.InverseTransformPoint(pos), MainMenu.currentLevelIsland.transform.rotation, transform);
            pos.z += 50f * 2.25f;
        }
    }
}
