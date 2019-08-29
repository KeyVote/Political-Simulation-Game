using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class createParty
{
    public createParty(string name, string ideologyType, int amountOfVotes, Tile seat) // Add party leader name
    {
        Name = name;
        ideology = ideologyType;
        votes = amountOfVotes;
        mandate = seat;
    }

    public string Name { get; private set; }
    public string ideology { get; private set; }
    public int votes { get; set; }
    public Tile mandate { get; private set; }
}

public class VotingSystem : MonoBehaviour
{
    TileManager tileManager;


    int totalVotes;
    int amountOfFasc;
    int amountOfComm;
    int amountOfLib;
    int amountOfCent;

    public List<createParty> partyList;

    public Tile fascSeat;
    public Tile commSeat;
    public Tile libSeat;
    public Tile centSeat;

    createParty winnerParty;
    int winnerVotes;
  
    public Tilemap tilemap;

    Canvas canvas;
    public Text winningPartyText;
    public Text winningPartyVotesText;

    void Awake()
    {
        tileManager = FindObjectOfType<TileManager>();
        canvas = FindObjectOfType<Canvas>();
        partyList = new List<createParty>();
    }

    int roundVector3Int(Vector3Int v3Int)
    {
        int x = Mathf.RoundToInt(v3Int.x);
        int y = Mathf.RoundToInt(v3Int.y);
        int sum = x * 1000 + y * 1000000;

        return sum;
    }

    public void countVotes(int key)
    {
        //foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        
        //Vector3Int posTilePlace = new Vector3Int(pos.x, pos.y, pos.z);
        //if (tilemap.HasTile(posTilePlace))
        
        //Dictkey = roundVector3Int(posTilePlace);
        foreach (createPOP pop in tileManager.tileDataStore[key].tilePOPs)
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
        partyList[0].votes = amountOfFasc;
        partyList[1].votes = amountOfComm;
        partyList[2].votes = amountOfLib;
        partyList[3].votes = amountOfCent;
    }

    // Start is called before the first frame update
    public void partySetup()
    {

        createParty fascistParty = new createParty("The Fascist Party", "Fascism", amountOfFasc, fascSeat);
        createParty communistParty = new createParty("The Communist Party", "Communism", amountOfComm, commSeat);
        createParty liberalParty = new createParty("The Liberal Party", "Neo-Liberalism", amountOfLib, libSeat);
        createParty centristParty = new createParty("The Centrist Party", "Centrism", amountOfCent, centSeat);

        partyList.Add(fascistParty);
        partyList.Add(communistParty);
        partyList.Add(liberalParty);
        partyList.Add(centristParty);
    }

    // Update is called once per frame
 
    // General Vote is the general voting (obviously)
    public void GeneralVote()
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
