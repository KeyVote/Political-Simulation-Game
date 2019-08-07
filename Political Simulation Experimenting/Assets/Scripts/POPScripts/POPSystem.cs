using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createPOP
{
    public createPOP(string name, int amountOfType, string ideologyType) // Add growth
    {
        Name = name + amountOfType;
        ideology = ideologyType;
    }

    public string Name { get; private set; }
    public string ideology { get; private set; }
}

public class POPSystem : MonoBehaviour
{
    int fascistAmount;
    int communistAmount;
    int liberalAmount;
    int centristAmount;

    TileManager tileManager;
    // Start is called before the first frame update

    void Awake()
    {
        tileManager = FindObjectOfType<TileManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreatePOPsForMandate(int key)
    {
        var rnd = new System.Random();
        int communistRandom = rnd.Next(15);
        int fascistRandom = rnd.Next(15);
        int liberalRandom = rnd.Next(15);
        int centRandom = rnd.Next(15);

        for (int i = 0; i < fascistRandom; i++)
        {
            createPOP POP = new createPOP("FascistPOP", fascistAmount, "Fascism");
            Debug.Log("THE CREATE KEY IS: " + key);
            fascistAmount++;

            tileManager.tileDataStore[key].tilePOPs.Add(POP);
        }

        for (int i = 0; i < communistRandom; i++)
        {
            createPOP POP = new createPOP("CommunistPOP", communistAmount, "Communism");
            communistAmount++;

            tileManager.tileDataStore[key].tilePOPs.Add(POP);
        }

        for (int i = 0; i < liberalRandom; i++)
        {
            createPOP POP = new createPOP("LiberalPOP", liberalAmount, "Neo-Liberalism");
            liberalAmount++;

            tileManager.tileDataStore[key].tilePOPs.Add(POP);
        }

        for (int i = 0; i < centRandom; i++)
        {
            createPOP POP = new createPOP("CentristPOP", centristAmount, "Centrism");
            centristAmount++;

            tileManager.tileDataStore[key].tilePOPs.Add(POP);
        }
    }
}