using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour {

    private GameObject[] _islands;
    
    private void Start() {
        
        _setUpIslands();

        _movePostLoad();
        //_destroyPostLoad();
    }

    //Move certain objects that are saved in-between scenes away from view.  
    private void _movePostLoad() {
        GameObject.FindGameObjectWithTag("MovePostLoad").transform.position = new Vector3(1000f,1000f,1000f);
    }
    
    //Destroy certain objects that are saved in-between scenes after information has been extracted from them. 
    private void _destroyPostLoad() {
        Destroy(GameObject.FindGameObjectWithTag("DestroyPostLoad"));
    }

    //Set up the islands depending on which chapter the user chose to play. 
    private void _setUpIslands() {
        
        _islands = new GameObject[MainMenu.roundAmount];
        var pos = Vector3.zero;
        pos.y = -6.5f * 2.25f;
        for (int i = 0; i < MainMenu.roundAmount; i++) {
            
            _islands[i] = (GameObject) Instantiate(MainMenu.currentChapterIsland, transform.InverseTransformPoint(pos), MainMenu.currentChapterIsland.transform.rotation, transform);
            pos.z += 50f * 2.25f;
        }
    }
}
