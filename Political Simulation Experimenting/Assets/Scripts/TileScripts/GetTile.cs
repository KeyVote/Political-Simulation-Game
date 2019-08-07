using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class GetTile : MonoBehaviour {

    TileManager tileManager;

    void Awake()
    {
        tileManager = FindObjectOfType<TileManager>();
    }

    int roundVector3Int(Vector3Int v3Int)
    {
        int x = Mathf.RoundToInt(v3Int.x);
        int y = Mathf.RoundToInt(v3Int.y);
        int z = Mathf.RoundToInt(v3Int.z);
        int sum = x * 1000 + z + y * 1000000;

        return sum;
    }

    public Tilemap tilemap;
    public Tile socParty;
    public Tile fasParty;
    int key;

	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int coordinate = tilemap.WorldToCell(pos);

            if (tilemap.GetTile(coordinate) != null) {
                key = roundVector3Int(coordinate);
                Debug.Log("TILEKEY IS: " + key);
                if (tileManager.tileDataStore[key] != null)
                {
                    tilemap.SetTile(coordinate, fasParty);
                    Debug.Log(string.Format("Tile name is: {0}", tileManager.tileDataStore[key]));
                    Debug.Log("AMOUNT OF POPS: " + tileManager.tileDataStore[key].tilePOPs.Count);
                } else
                {
                    Debug.Log("KEY DOES NOT WORK. KEY: " + key);
                }
            }      
        }
    }
}
