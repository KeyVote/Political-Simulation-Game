using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour {

    public class TileData {
        public string Name;
        public Vector3Int x, y;
        public int population;
    }

    public class Location
    {
        public int x;
        public int y;
    }

    public Dictionary<Location, TileData> TileStore = new Dictionary<Location, TileData>(); 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
