using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

class createParty
{
    public createParty(string name, string ideologyType, int amountOfVotes) // Add party leader name
    {
        Name = name;
        ideology = ideologyType;
        votes = amountOfVotes;
    }

    public string Name { get; private set; }
    public string ideology { get; private set; }
    public int votes { get; private set; }
}

public class VotingSystem : MonoBehaviour
{
    TileManager tileManager;

    int totalVotes;
    int amountOfFasc;
    int amountOfComm;
    int amountOfLib;
    int amountOfCent;

    List<createParty> partyList;

    createParty winnerParty;
    int winnerVotes;
  
    public Tilemap tilemap;
    int Dictkey; 

    Canvas canvas;
    public Text winningPartyText;
    public Text winningPartyVotesText;

    void Awake()
    {
        tileManager = FindObjectOfType<TileManager>();
        canvas = FindObjectOfType<Canvas>();
    }
    
    void countVotes()
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int posTilePlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (tilemap.HasTile(posTilePlace))
            {
                Dictkey = roundVector3Int(posTilePlace);
                foreach (var pop in tileManager.tileDataStore[Dictkey].tilePOPs)
                {
                    if (pop.ideology == "Fascism")
                    {
                        amountOfFasc++;
                        totalVotes++;
                    }
                    else if (pop.ideology == "Communism")
                    {
                        amountOfComm++;
                        totalVotes++;
                    } else if (pop.ideology == "Neo-Liberalism")
                    {
                        amountOfLib++;
                        totalVotes++;
                    } else if (pop.ideology == "Centrism")
                    {
                        amountOfCent++;
                        totalVotes++;
                    }
                }
            }
        }
    }

    int roundVector3Int(Vector3Int v3Int)
    {
        int x = Mathf.RoundToInt(v3Int.x);
        int y = Mathf.RoundToInt(v3Int.y);
        int sum = x * 1000 + y * 1000000;

        return sum;
    }

    // Start is called before the first frame update
    void Start()
    {
        countVotes();

        createParty fascistParty = new createParty("The Fascist Party", "Fascism", amountOfFasc);
        createParty communistParty = new createParty("The Communist Party", "Communism", amountOfComm);
        createParty liberalParty = new createParty("The Liberal Party", "Neo-Liberalism", amountOfLib);
        createParty centristParty = new createParty("The Centrist Party", "Centrism", amountOfCent);

        partyList = new List<createParty>();
        partyList.Add(fascistParty);
        partyList.Add(communistParty);
        partyList.Add(liberalParty);
        partyList.Add(centristParty);
    }

    // Update is called once per frame
    void Update()
    {
        GeneralVote();
    }

    // General Vote is the general voting (obviously)
    void GeneralVote()
    {
        winnerVotes = Mathf.Max(partyList[0].votes, partyList[1].votes, partyList[2].votes, partyList[3].votes);

        foreach (createParty party in partyList)
        {
            if (party.votes == winnerVotes)
            {
                winnerParty = party;
            }
        }

        winningPartyText.text = "The winning party in the general election is: " + winnerParty.Name;
        winningPartyVotesText.text = "Total winning party votes in the general election: " + winnerParty.votes;
    }
}
