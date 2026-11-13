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

    /*siema tu bartek, mam takie info. Jeśli tu jesteś to pewnie szukasz jakiś zmiennych. Ogólnie niektóre zmienne są 
    zarówno dal przeciwników i graczy więc zrobiłem dla nich wspólną klasę, turnObject(bo oboje bazują na turach).
    Sprawdź tąk klasę jeśli nie jesteś w stanie znaleźć ważnej zmiennej. 
    */



    public IEnumerator FindPlayerPath()
    {
        //argument w WaitForSeconds, może nie dasz wiary, ale to liczba sekund ile czeka przed kontunuowaniem pracy
        yield return new WaitForSeconds(0.6f);
        currentPath = Pathfinding.FindPath(
            currentPole,
            targetTile
        );
        //jeśli droga istnieje to idziemy
        if(currentPath != null && currentPath.Count >= 2)
        {
            if (stamina <= 0)
            {
                //koniec tury
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
            //stoje przy celu, koniec na razie, potem zmienimy
            Debug.Log("Stoję przy celu");
            endYourTurn();
        }
        
            
        
        
    }


    //inicjakizacja ważnych danych przez generator
    public void inicialiate(Vector2Int cords)
    {
        currentPole = generator.pola[cords].GetComponent<HexPoleScript>();
        transform.position = generator.pola[cords].transform.position;
        currentPole.dostepneAkcje["Occupated"] = true;
        targetTile = generator.pola[new Vector2Int(0,10)].GetComponent<HexPoleScript>();
        
    }
    //zmiana pozycji
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
    //początek tury
    public override void startYourTurn()
    {
        stamina = stats.stamina;
        targetTile = generator.player.currentPole;
        StartCoroutine(FindPlayerPath());
    }

    //koniec tury
    public override void endYourTurn()
    {
        generator.changeNextTurn();
    }

}
