using UnityEngine;
using System.Collections.Generic;

public class ChampionScript : turnObject
{

    public Vector2Int pozycja = new Vector2Int(0, 0);

    public roomGenerator generator;

    public HexPoleScript currentPole;
    ItemManager itemManager;

    
    public int stamina = 0;

    // action będzie przechowywał akcję jaką wykonuję champion.
    public string action = "Move";

    /*siema tu bartek, mam takie info. Jeśli tu jesteś to pewnie szukasz jakiś zmiennych. Ogólnie niektóre zmienne są 
    zarówno dal przeciwników i graczy więc zrobiłem dla nich wspólną klasę, turnObject(bo oboje bazują na turach).
    Sprawdź tąk klasę jeśli nie jesteś w stanie znaleźć ważnej zmiennej. 
    */

    void Start()
    {
        itemManager = FindFirstObjectByType<ItemManager>();

    }

    void Update()
    {

    }

    public void actionToTake(Vector2Int toCordinates)
    {
        //message odpala funkcję o nazwie akcje i przekazuje argument toCordinates
        SendMessage(action, toCordinates);
    }
    //interpretacja kordunaktow na pozycje, mozna troche zmienic jesli chcecie ale ja nie widze bledow 
    public void przeniesNaPozycje(Vector2Int pozycja)
    {
        if (generator.pola.ContainsKey(pozycja))
        {
            transform.position = generator.pola[pozycja].transform.position;
        }
    }

    //no i tutaj wykonuje sie akcja wyslana SendMessage, zmieniajac parametr action bedzie mozna zmienic logike zachowania pamietajac jakie pole zostalo wybrane
    public void Move(Vector2Int cords) {
        generator.resetBoard();
        currentPole.dostepneAkcje["Occupated"] = false;
        przeniesNaPozycje(cords);
        pozycja = cords;
        //po poruszeniu się gracza wykonują się funkcje na polu
        currentPole = generator.pola[cords].GetComponent<HexPoleScript>();
        currentPole.setActiveHex(1);
        currentPole.ZbierzItem();
        currentPole.dostepneAkcje["Occupated"] = true;
        //currentPole.enable();
        //itemManager.SprawdzKolizje(cords);
        stamina--;
        if (stamina <= 0) {
            endYourTurn();
        }

    }
    //akcje na zaczęciu tury
    public override void startYourTurn()
    {
        stamina = stats.stamina;
        currentPole.setActiveHex(1);
        action = "Move";
    }
        
    //akcje na zakończenie tury
    public override void endYourTurn()
    {
        currentPole.turnOffHex(1);
        generator.changeNextTurn();
    }
}

