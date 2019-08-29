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

    void Awake()
    {
        tileManager = FindObjectOfType<TileManager>();
    }

    public void CreatePOPsForMandate(int key)
    {
        int communistRandom = Random.Range(0, 16);
        int fascistRandom = Random.Range(0, 16);
        int liberalRandom = Random.Range(0, 16);
        int centRandom = Random.Range(0, 16);
        Debug.Log("Cent random is: " + centRandom);
        Debug.Log("Fasc random is: " + fascistRandom);
        Debug.Log("Liberal random is: " + liberalRandom);
        Debug.Log("communist random is: " + communistRandom);

        Debug.Log("THE CREATE KEY IS: " + key);

        for (int i = 0; i < fascistRandom; i++)
        {
            createPOP POP = new createPOP("FascistPOP", fascistAmount, "Fascism");
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