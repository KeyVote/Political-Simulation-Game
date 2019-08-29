using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class createTileData {
    public createTileData(string Name, Vector3Int intV3, createParty MajorityParty, string MajorityPartysName, int majorityPartyVotes, int MajorityPartyVotes,int FascistPartyVotes, int CommunistPartyVotes, int LiberalPartyVotes, int CentristPartyVotes)
    {
        name = Name;
        position = intV3;

        tilePOPs = new List<createPOP>();
        majorityParty = MajorityParty;
        majorityPartysName = MajorityPartysName;

        majorityPartyVotes = MajorityPartyVotes;
        fascistPartyVotes = FascistPartyVotes;
        communistPartyVotes = CommunistPartyVotes;
        liberalPartyVotes = LiberalPartyVotes;
        centristPartyVotes = CentristPartyVotes;
    }

    public string name { get; private set; }
    public Vector3Int position { get; private set; }
    public List<createPOP> tilePOPs { get; private set; }
    public createParty majorityParty { get; set; }
    public string majorityPartysName { get; set; }
    public int majorityPartyVotes { get; set; }
    public int fascistPartyVotes { get; set; }
    public int communistPartyVotes { get; set; }
    public int liberalPartyVotes { get; set; }
    public int centristPartyVotes { get; set; }
}

public class TileManager : MonoBehaviour
{

    public Tilemap tilemap;

    public Dictionary<int, createTileData> tileDataStore;

    POPSystem createPOPs;

    public int highestAmount;

    public int key;

    VotingSystem votingSystem;
    public Tile neutralSeat;
    public Tile fasSeat;
    public Tile commSeat;
    public Tile libSeat;
    public Tile centSeat;

    createParty winningParty;

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
        votingSystem = FindObjectOfType<VotingSystem>();
    }

    void Start()
    {
        tileDataStore = new Dictionary<int, createTileData>();
        int i = 0;
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int posTilePlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 v3PosTilePlace = tilemap.CellToWorld(posTilePlace);
            Vector3Int checkPosTilePlace = new Vector3Int(posTilePlace.x, posTilePlace.y, posTilePlace.z);

            if (tilemap.HasTile(posTilePlace))
            {
                createTileData tile = new createTileData("Mandate" + i, checkPosTilePlace, null, null, 0, 0, 0, 0, 0, 0);
                i++;
                key = roundVector3Int(checkPosTilePlace);

                tileDataStore.Add(key, tile);
                createPOPs.CreatePOPsForMandate(key);

                int amountOfFasc = 0;
                int amountOfComm = 0;
                int amountOfLib = 0;
                int amountOfCent = 0;

                votingSystem.partySetup();
                votingSystem.countVotes(key);

                foreach (createPOP pop in tileDataStore[key].tilePOPs)
                {
                    if (pop.ideology == "Fascism")
                    {
                        amountOfFasc++;
                        Debug.Log("POP in " + tileDataStore[key].tilePOPs + " is fascist");
                        tileDataStore[key].fascistPartyVotes++;
                    }
                    else if (pop.ideology == "Communism")
                    {
                        amountOfComm++;
                        Debug.Log("POP in " + tileDataStore[key].tilePOPs + " is communist");
                        tileDataStore[key].communistPartyVotes++;
                    }
                    else if (pop.ideology == "Neo-Liberalism")
                    {
                        amountOfLib++;
                        Debug.Log("POP in " + tileDataStore[key].tilePOPs + " is LIBERAL");
                        tileDataStore[key].liberalPartyVotes++;
                    }
                    else if (pop.ideology == "Centrism")
                    {
                        amountOfCent++;
                        Debug.Log("POP in " + tileDataStore[key].tilePOPs + " is centrist");
                        tileDataStore[key].centristPartyVotes++;
                    }
                }

                int[] votesArray = { amountOfFasc, amountOfComm, amountOfLib, amountOfCent };

                highestAmount = Mathf.Max(amountOfFasc, amountOfComm, amountOfLib, amountOfCent);
                Debug.Log("AMOUNT OF FASCIST: " + amountOfFasc);
                Debug.Log("AMOUNT OF COMMIES: " + amountOfComm);
                Debug.Log("AMOUNT OF LIBERALS: " + amountOfLib);
                Debug.Log("AMOUNT OF CENTRISTS: " + amountOfCent);
                Debug.Log("HIGHEST AMOUNT IS: " + highestAmount);

                foreach (int votes in votesArray)
                {
                    if (votes == highestAmount)
                    {
                        Debug.Log("PARTY VOTES =: " + votes);
                        if (votes == amountOfFasc && votes == amountOfComm && votes == amountOfLib && votes == amountOfCent)
                        {
                            tilemap.SetTile(checkPosTilePlace, neutralSeat);
                            tileDataStore[key].majorityPartysName = "No party";
                            tileDataStore[key].majorityPartyVotes = 0;
                            Debug.Log("NEUTRAL SEAT MADE");
                        }
                        else
                        {
                            if (amountOfFasc == highestAmount)
                            {
                                if (amountOfFasc != amountOfComm || amountOfFasc != amountOfLib || amountOfFasc != amountOfCent)
                                {
                                    tilemap.SetTile(checkPosTilePlace, fasSeat);
                                    tileDataStore[key].majorityParty = votingSystem.partyList[0];
                                    tileDataStore[key].majorityPartysName = "The Fascist Party";
                                    tileDataStore[key].majorityPartyVotes = amountOfFasc;
                                }
                            }
                            else if (amountOfComm == highestAmount)
                            {
                                if (amountOfComm != amountOfFasc || amountOfComm != amountOfLib || amountOfComm != amountOfCent)
                                {
                                    tilemap.SetTile(checkPosTilePlace, commSeat);
                                    tileDataStore[key].majorityParty = votingSystem.partyList[1];
                                    tileDataStore[key].majorityPartysName = "The Communist Party";
                                    tileDataStore[key].majorityPartyVotes = amountOfComm;
                                }
                            }
                            else if (amountOfLib == highestAmount)
                            {
                                if (amountOfLib != amountOfFasc || amountOfLib != amountOfComm || amountOfLib != amountOfCent)
                                {
                                    tilemap.SetTile(checkPosTilePlace, libSeat);
                                    tileDataStore[key].majorityParty = votingSystem.partyList[2];
                                    tileDataStore[key].majorityPartysName = "The Liberal Party";
                                    tileDataStore[key].majorityPartyVotes = amountOfLib;
                                }
                            }
                            else if (amountOfCent == highestAmount)
                            {
                                if (amountOfCent != amountOfFasc || amountOfCent != amountOfComm || amountOfCent != amountOfLib)
                                {
                                    tilemap.SetTile(checkPosTilePlace, centSeat);
                                    tileDataStore[key].majorityParty = votingSystem.partyList[3];
                                    tileDataStore[key].majorityPartysName = "The Centrist Party";
                                    tileDataStore[key].majorityPartyVotes = amountOfCent;
                                }
                            }
                        }
                    }
                    Debug.Log("This is the newest data: " + key);
                    Debug.Log("LENGTH IS: " + tileDataStore.Count);
                }
            }
        }
        votingSystem.GeneralVote();
    }
}
