using UnityEngine;
using System.Collections.Generic;

public class enemyScript : MonoBehaviour
{
    public Vector2Int pozycja = new Vector2Int(0, 0);

    public roomGenerator generator;

    public HexPoleScript currentPole;

    public HexPoleScript targetTile;

    private List<HexPoleScript> currentPath;

    public void FindPlayerPath()
    {
        currentPath = Pathfinding.FindPath(
            currentPole,
            targetTile
        );
        foreach(HexPoleScript x in currentPath)
        {

            Debug.Log(x.coordinates);
        }
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void inicialiate(Vector2Int cords)
    {
        currentPole = generator.pola[cords].GetComponent<HexPoleScript>();
        transform.position = generator.pola[cords].transform.position;
        pozycja = cords;
        currentPole.dostepneAkcje["Occupated"] = true;
        targetTile = generator.pola[new Vector2Int(0,10)].GetComponent<HexPoleScript>();
        FindPlayerPath();

    }


}
