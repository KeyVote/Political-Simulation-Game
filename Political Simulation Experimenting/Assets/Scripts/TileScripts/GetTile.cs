using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEngine.UI;

public class GetTile : MonoBehaviour {

    TileManager tileManager;
    public Tilemap tilemap;
    public Tile socParty;
    public Tile fasParty;
    int key;

    public GameObject panel;
    public Text popTextBox;
    public Text majPartyTextBox;
    public Text majPartysVotesTextBox;
    public Text fascistPartysVotesTextBox;
    public Text communistPartysVotesTextBox;
    public Text liberalPartysVotesTextBox;
    public Text centristPartysVotesTextBox;

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

    public void OpenPanel(int population, string majPartysName, int majPartyVotes, int fascistPartyVotes, int communistPartyVotes, int liberalPartyVotes, int centristPartyVotes)
    {
        if (panel != null)
        {
            //bool isActive = panel.activeSelf;
            panel.SetActive(true);

            popTextBox.text = "Population: " + population;
            majPartyTextBox.text = "Majority Party: " + majPartysName;
            majPartysVotesTextBox.text = "Majority Party's Votes: " + majPartyVotes;
            fascistPartysVotesTextBox.text = "Fascist Party's Votes: " + fascistPartyVotes;
            communistPartysVotesTextBox.text = "Communist Party's Votes: " + communistPartyVotes;
            liberalPartysVotesTextBox.text = "Liberal Party's Votes: " + liberalPartyVotes;
            centristPartysVotesTextBox.text = "Centrist Party's Votes: " + centristPartyVotes;
        }
    }

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
                    Debug.Log(tileManager.tileDataStore[key].majorityParty);
                    OpenPanel(tileManager.tileDataStore[key].tilePOPs.Count, tileManager.tileDataStore[key].majorityPartysName, tileManager.tileDataStore[key].majorityPartyVotes, tileManager.tileDataStore[key].fascistPartyVotes, tileManager.tileDataStore[key].communistPartyVotes, tileManager.tileDataStore[key].liberalPartyVotes, tileManager.tileDataStore[key].centristPartyVotes);
                    Debug.Log(string.Format("Tile name is: {0}", tileManager.tileDataStore[key].name));
                    Debug.Log("AMOUNT OF POPS: " + tileManager.tileDataStore[key].tilePOPs.Count);
                }
                else
                {

                }
            }      
        }
    }
}
