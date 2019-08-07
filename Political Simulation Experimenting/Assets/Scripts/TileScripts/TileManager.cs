using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class createTileData {
    public createTileData(string name, Vector3Int intV3, float population)
    {
        Name = name;
        position = intV3;
        people = population;

        tilePOPs = new List<createPOP>();
    }

    public string Name { get; private set; }
    public Vector3Int position { get; private set; }
    public float people { get; private set; }
    public List<createPOP> tilePOPs { get; private set; }
}

public class TileManager : MonoBehaviour {

    public Tilemap tilemap;

    public Dictionary<int, createTileData> tileDataStore;

    POPSystem createPOPs;

    int highestAmount;

    int roundVector3Int(Vector3Int v3Int)
    {
        int x = Mathf.RoundToInt(v3Int.x);
        int y = Mathf.RoundToInt(v3Int.y);
        int sum = x * 1000 + y * 1000000;

        return sum;
    }

    void Awake()
    {
        createPOPs = FindObjectOfType<POPSystem>();
    }

    public int key;

    public Tile neutralSeat;
    public Tile fasSeat;
    public Tile commSeat;
    public Tile libSeat;
    public Tile centSeat;
    public Tilemap localTileMap;

    void Start () {
        tileDataStore = new Dictionary<int, createTileData>();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin) {
            int i = 0;
            Vector3Int posTilePlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 v3PosTilePlace = tilemap.CellToWorld(posTilePlace);
            Vector3Int checkPosTilePlace = new Vector3Int(posTilePlace.x, posTilePlace.y, posTilePlace.z);

            if (tilemap.HasTile(posTilePlace)) {
                createTileData tile = new createTileData("Mandate" + i, checkPosTilePlace, 10f);
                key = roundVector3Int(checkPosTilePlace);

                tileDataStore.Add(key, tile);
                createPOPs.CreatePOPsForMandate(key);

                int amountOfFasc = 0;
                int amountOfComm = 0;
                int amountOfLib = 0;
                int amountOfCent = 0;

                foreach (var pop in tileDataStore[key].tilePOPs)
                {
                    if (pop.ideology == "Fascism")
                    {
                        amountOfFasc++;
                    } else if (pop.ideology == "Communism")
                    {
                        amountOfComm++;
                    } else if(pop.ideology == "Neo-Liberalism")
                    {
                        amountOfLib++;
                    } else if(pop.ideology == "Centrism")
                    {
                        amountOfCent++;
                    }
                }

                highestAmount = Mathf.Max(amountOfFasc, amountOfComm, amountOfLib, amountOfCent);

                if (amountOfFasc == highestAmount)
                {
                    localTileMap.SetTile(checkPosTilePlace, fasSeat);
                    Debug.Log("FASC SEAT!");
                } else if (amountOfComm == highestAmount)
                {
                    localTileMap.SetTile(checkPosTilePlace, commSeat);
                } else if (amountOfLib == highestAmount)
                {
                    Debug.Log("Liberal seat made!");
                    localTileMap.SetTile(checkPosTilePlace, libSeat);
                } else if (amountOfCent == highestAmount)
                {
                    localTileMap.SetTile(checkPosTilePlace, centSeat);
                } else
                {
                    localTileMap.SetTile(checkPosTilePlace, neutralSeat);
                }

                Debug.Log("This is the newest data: "+ key);
                Debug.Log("LENGTH IS: " + tileDataStore.Count);

                i++;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
