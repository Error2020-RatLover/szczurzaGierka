using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyScript : turnObject
{
    public Vector2Int pozycja = new Vector2Int(0, 0);


    public roomGenerator generator;

    public HexPoleScript currentPole;

    public HexPoleScript targetTile;

    private List<HexPoleScript> currentPath;

    public int stamina = 0;

    

    public IEnumerator FindPlayerPath()
    {
        yield return new WaitForSeconds(0.6f);
        currentPath = Pathfinding.FindPath(
            currentPole,
            targetTile
        );

        if(currentPath != null && currentPath.Count >= 2)
        {
            if (stamina <= 0)
            {
             
                endYourTurn();
               
            }
            else
            {
                stamina -= 1;
                changePosition(currentPath[0].coordinates);
            }
            
            
            
        }
        else
        {
            Debug.Log("Stoję przy celu");
            endYourTurn();
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
        currentPole.dostepneAkcje["Occupated"] = true;
        targetTile = generator.pola[new Vector2Int(0,10)].GetComponent<HexPoleScript>();
        


    }

    public void changePosition(Vector2Int cords)
    {
        currentPole.dostepneAkcje["Occupated"] = false;
        transform.position = generator.pola[cords].transform.position;
        currentPole = generator.pola[cords].GetComponent<HexPoleScript>();
        currentPole.dostepneAkcje["Occupated"] = true;
        currentPole.enable();
        StartCoroutine(FindPlayerPath());
        targetTile = generator.player.currentPole;

    }

    public override void startYourTurn()
    {
        stamina = stats.stamina;
        targetTile = generator.player.currentPole;
        StartCoroutine(FindPlayerPath());
    }

    
    public override void endYourTurn()
    {
        generator.changeNextTurn();
    }

}
