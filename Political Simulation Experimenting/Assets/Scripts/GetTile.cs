using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class GetTile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    public Tilemap tilemap;
    public Tile socParty;
    public Tile fasParty;
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("CLIKED PRIMARY");
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int coordinate = tilemap.WorldToCell(pos);
            Debug.Log(string.Format("Coordinates: {0} & {1}", coordinate.x, coordinate.y));
            if (tilemap.GetTile(coordinate) != null) {
                Debug.Log(string.Format("TILES COLOR IS: {0}", tilemap.color));
            } else {
                tilemap.SetTile(coordinate, socParty);
            }
            
            
        }
    }

}
